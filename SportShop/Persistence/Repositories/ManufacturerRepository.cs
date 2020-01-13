using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SportShop.Models;
using SportShop.Persistence.Entities;

namespace SportShop.Persistence.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public ManufacturerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Manufacturer> Manufacturers => _dbContext.Manufacturers.Include(x => x.Products);

        /// <summary>
        /// Deleted manufacturer with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteManufacturer(int id)
        {
            var entity = _dbContext.Manufacturers.FirstOrDefault(x => x.Id == id);

            if (entity == null)
                return false;

            _dbContext.Manufacturers.Remove(entity);
            _dbContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool SaveManufacturer(Manufacturer entity)
        {
            if (entity.Id == 0)
            {
                var res = _dbContext.Manufacturers.Add(entity);
                _dbContext.SaveChanges();
                return true;
            }

            else if (entity.Id > 0)
            {
                var entityToUpdate = _dbContext.Manufacturers.SingleOrDefault(x => x.Id == entity.Id);

                if (entityToUpdate == null)
                    return false;

                entityToUpdate.Name = entity.Name;
                entityToUpdate.Country = entity.Country;

                _dbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public ResultModel<Manufacturer> GetById(int id)
        {
            try
            {
                var entity = _dbContext.Manufacturers.FirstOrDefault(x => x.Id == id);
                
                if(entity == null)
                    return new ResultModel<Manufacturer>(null, 404);
                
                return new ResultModel<Manufacturer>(entity, 200);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Manufacturer>(null, 500);
            }
        }
        public ResultModel<Manufacturer> Create(Manufacturer entity)
        {
            try
            {
                if (entity.Id != 0)
                {
                    var getResponse = _dbContext.Manufacturers.FirstOrDefault(x => x.Id == entity.Id);
                    if (getResponse != null)
                        return new ResultModel<Manufacturer>(entity, 409);
                }

                var createdResult = _dbContext.Manufacturers.Add(entity);
                _dbContext.SaveChanges();
                return new ResultModel<Manufacturer>(createdResult.Entity, 201);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Manufacturer>(null, 500);
            }
        }

        public ResultModel<Manufacturer> Update(Manufacturer entity)
        {
            try
            {
                var getResponse = _dbContext.Manufacturers.FirstOrDefault(x => x.Id == entity.Id);

                if (getResponse == null)
                    return new ResultModel<Manufacturer>(null, 404);

                var updateResponse = _dbContext.Update(entity);
                _dbContext.SaveChanges();
                return new ResultModel<Manufacturer>(updateResponse.Entity, 200);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Manufacturer>(null, 500);
            }
        }

        public ResultModel<Manufacturer> Delete(int id)
        {
            try
            {
                var entity = _dbContext.Manufacturers.FirstOrDefault(x => x.Id == id);

                if (entity == null)
                    return new ResultModel<Manufacturer>(null, 404);

                _dbContext.Remove(entity);
                _dbContext.SaveChanges();

                return new ResultModel<Manufacturer>(null, 204);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Manufacturer>(null, 500);
            }
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}