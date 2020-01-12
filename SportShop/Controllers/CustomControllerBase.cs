using System.Web.Http.Results;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;

namespace SportShop.Controllers
{
    /// <summary>
    /// A custom controller base class for an MVC controller with view support.
    /// </summary>
    public class CustomControllerBase : Controller
    {
        /// <summary>
        /// Represents internal server error.
        /// </summary>
        /// <param name="error">Custom error message.</param>
        protected ObjectResult InternalServerError(string error = "An unexpected internal server error has occured.") => StatusCode(500, error);
    }
}