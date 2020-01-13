using Moq;
using SportShop.Controllers;
using SportShop.Persistence.Repositories;

namespace UnitTests
{
    public class ManufacturersApiControllerTests
    {
        private readonly Mock<IManufacturerRepository> _mock = new Mock<IManufacturerRepository>();
        private ManufacturersApiController _controller;
    }
}