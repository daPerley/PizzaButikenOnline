using Microsoft.AspNetCore.Identity;
using PizzaButikenOnline.Models;

namespace PizzaButikenOnline.Data
{
    public class DBInitializer
    {
        public static void Initialize(UserManager<ApplicationUser> userManager)
        {
            var aUser = new ApplicationUser();

            aUser.UserName = "test@user.com";
            aUser.Email = "test@user.com";

            var r = userManager.CreateAsync(aUser, "Abc12#Test");
        }
    }
}
