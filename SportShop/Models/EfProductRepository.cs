using System.Linq;
using System.Reflection;

namespace SportShop.Models
{
    public class EfProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EfProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Product> Products => _dbContext.Products;
    }
}