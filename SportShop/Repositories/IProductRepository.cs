using System.Linq;
using SportShop.Models;

namespace SportShop.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        bool DeleteProduct(int id);
        bool SaveProduct(Product entity);
    }
}