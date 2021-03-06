﻿using System.Collections.Generic;
using System.Linq;
using SportShop.Models;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;

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

        public ResultModel<Product> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public ResultModel<Product> Create(Product entity)
        {
            throw new System.NotImplementedException();
        }

        public ResultModel<Product> Update(Product entity)
        {
            throw new System.NotImplementedException();
        }

        public ResultModel<Product> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public ResultModel<IEnumerable<Product>> GetAll(string category)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}