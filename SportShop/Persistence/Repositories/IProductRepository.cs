using System;
using System.Collections.Generic;
using System.Linq;
using SportShop.Models;
using SportShop.Persistence.Entities;

namespace SportShop.Persistence.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        bool DeleteProduct(int id);
        bool SaveProduct(Product entity);
        ResultModel<Product> GetById(int id);
        ResultModel<Product> Create(Product entity);
        ResultModel<Product> Update(Product entity);
        ResultModel<Product> Delete(int id);
        ResultModel<IEnumerable<Product>> GetAll(string category);
    }
}