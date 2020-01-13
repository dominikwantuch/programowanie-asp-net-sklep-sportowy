using Moq;
using SportShop.Persistence.Repositories;

namespace DominikUnitTests.ProductApiControllerTests
{
    public class ApplicationDbContextMockHelper
    {
        public readonly Mock<ApplicationDbContext> Mock = new Mock<ApplicationDbContext>();

        public ApplicationDbContextMockHelper()
        {
        }
    }
}