using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportShop.Models;

namespace SportShop.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
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
                _dbContext.Manufacturers.Add(entity);
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
    }
}