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
    }
}