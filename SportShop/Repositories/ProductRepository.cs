using System.Linq;
using Microsoft.AspNetCore.Http;
using SportShop.Models;

namespace SportShop.Repositories
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
        
    }
}