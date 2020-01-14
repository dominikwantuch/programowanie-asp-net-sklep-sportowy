using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Moq;
using SportShop.Controllers;
using SportShop.Persistence.Entities;
using SportShop.Persistence.Repositories;
using Xunit;
using Xunit.Abstractions;

namespace SzymonDomalikUnitTests
{
    public class AdminControllerTests
    {
        private readonly Mock<IProductRepository> _mock = new Mock<IProductRepository>();
    }
}

