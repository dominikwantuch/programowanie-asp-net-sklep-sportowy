using System;
using System.Collections.Generic;
using System.Linq;
using SportShop.Models;
using SportShop.Persistence.Entities;

namespace SportShop.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Product> Products => _dbContext.Products;

        public bool DeleteProduct(int id)
        {
            var entity = _dbContext.Products.FirstOrDefault(x => x.ProductId == id);

            if (entity == null)
                return false;

            _dbContext.Products.Remove(entity);
            _dbContext.SaveChanges();

            return true;
        }

        public bool SaveProduct(Product entity)
        {
            if (entity.ProductId == 0)
            {
                _dbContext.Products.Add(entity);
                _dbContext.SaveChanges();
                return true;
            }

            else if (entity.ProductId > 0)
            {
                var entityToUpdate = _dbContext.Products.SingleOrDefault(x => x.ProductId == entity.ProductId);

                if (entityToUpdate == null)
                    return false;

                entityToUpdate.Category = entity.Category;
                entityToUpdate.Description = entity.Description;
                entityToUpdate.Manufacturer = entity.Manufacturer;
                entityToUpdate.Name = entity.Name;
                entityToUpdate.Price = entity.Price;
                entityToUpdate.ManufacturerId = entity.ManufacturerId;

                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }


        public ResultModel<Product> GetById(int id)
        {
            try
            {
                var entity = _dbContext.Products.FirstOrDefault(x => x.ProductId == id);

                if (entity == null)
                    return new ResultModel<Product>(null, 404);

                return new ResultModel<Product>(entity, 200);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Product>(null, 500);
            }
        }

        public ResultModel<IEnumerable<Product>> GetAll(string category = null)
        {
            try
            {
                IEnumerable<Product> products;
                if (string.IsNullOrWhiteSpace(category))
                    products = _dbContext.Products.ToList();
                else
                    products = _dbContext.Products.Where(x => x.Category == category).ToList();

                return new ResultModel<IEnumerable<Product>>(products, 200);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<IEnumerable<Product>>(null, 500);
            }
        }

        public ResultModel<Product> Create(Product entity)
        {
            try
            {
                if (entity.ProductId != 0)
                {
                    var getResponse = _dbContext.Products.FirstOrDefault(x => x.ProductId == entity.ProductId);
                    if (getResponse != null)
                        return new ResultModel<Product>(entity, 409);
                }

                var createdResult = _dbContext.Products.Add(entity);
                _dbContext.SaveChanges();
                return new ResultModel<Product>(createdResult.Entity, 201);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Product>(null, 500);
            }
        }

        public ResultModel<Product> Update(Product entity)
        {
            try
            {
                var getResponse = _dbContext.Products.FirstOrDefault(x => x.ProductId == entity.ProductId);

                if (getResponse == null)
                    return new ResultModel<Product>(null, 404);

                var updateResponse = _dbContext.Update(entity);
                _dbContext.SaveChanges();
                return new ResultModel<Product>(entity, 200);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Product>(null, 500);
            }
        }

        public ResultModel<Product> Delete(int id)
        {
            try
            {
                var entity = _dbContext.Products.FirstOrDefault(x => x.ProductId == id);

                if (entity == null)
                    return new ResultModel<Product>(null, 404);

                _dbContext.Remove(entity);
                _dbContext.SaveChanges();

                return new ResultModel<Product>(null, 204);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultModel<Product>(null, 500);
            }
        }
    }
}