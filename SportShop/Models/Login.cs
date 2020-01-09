using System.ComponentModel.DataAnnotations;

namespace SportShop.Models
{
    public class Login
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}