using System.Linq;
using SportShop.Models;
using SportShop.Persistence.Entities;

namespace SportShop.Persistence.Repositories
{
    public interface IManufacturerRepository
    {
        IQueryable<Manufacturer> Manufacturers { get; }
        bool DeleteManufacturer(int id);
        bool SaveManufacturer(Manufacturer entity);

        ResultModel<Manufacturer> GetById(int id);
        ResultModel<Manufacturer> Create(Manufacturer entity);
        ResultModel<Manufacturer> Update(Manufacturer entity);
        ResultModel<Manufacturer> Delete(int id);
    }
}