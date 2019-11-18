using System.Collections.Generic;
using System.Linq;
using SportShop.Models;

// ReSharper disable All

namespace SportShop.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product() {Name = "Piłka nożna", Price = 25},
            new Product() {Name = "Deska surfingowa", Price = 179},
            new Product() {Name = "Buty do biegania", Price = 95}
        }.AsQueryable<Product>();

        public bool DeleteProduct(int id)
        {
            return false;
        }

        public bool SaveProduct(Product entity)
        {
            return false;
        }
    }
}