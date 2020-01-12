using System.Linq;
using SportShop.Models;

namespace SportShop.Repositories
{
    public interface IManufacturerRepository
    {
        IQueryable<Manufacturer> Manufacturers { get; }
        bool DeleteManufacturer(int id);
        bool SaveManufacturer(Manufacturer entity);
    }
}