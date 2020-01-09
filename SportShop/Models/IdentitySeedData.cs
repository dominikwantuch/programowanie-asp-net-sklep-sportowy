using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SportShop.Models
{
    /// <summary>
    /// Class for seeding users
    /// </summary>
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "test123";

        /// <summary>
        /// Makes sure that database is populated.
        /// </summary>
        /// <param name="userManager"></param>
        /// <returns></returns>
        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {

            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser(adminUser);
                await userManager.CreateAsync(user, adminPassword);
            }
        }

    }
}