using System.Collections.Generic;
using System.Linq;
// ReSharper disable All

namespace SportShop.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product() {Name = "Piłka nożna", Price = 25},
            new Product() {Name = "Deska surfingowa", Price = 179},
            new Product() {Name = "Buty do biegania", Price = 95}
        }.AsQueryable<Product>();
    }
}