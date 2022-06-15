using Microsoft.AspNetCore.Identity;

namespace SkinetApi.Entities.Identity
{
    public class IdentityUsersSeeding
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager, RoleManager<Role> roleManager)
        {
            var role1 = "Admin";
            var Desc1 = "This is the Admin role";

            var role2 = "User";
            var Desc2 = "This is the User role";

            var role3 = "Manager";
            var Desc3 = "This is the Manager role";

            var password = "Pa$$w0rd";

            
            
            Role[] role = { new Role { Name = role1, Description = Desc1 }, new Role { Name = role2, Description = Desc2 }, new Role { Name = role3, Description = Desc3 } };

            for (var i = 0; i <= role.Length - 1; i++)
            {
                if (!await roleManager.RoleExistsAsync(role[i].Name) )
                {
                    await roleManager.CreateAsync(role[i]);
                }
               
                //await roleManager.SetRoleNameAsync(item, role2);
                    

            }
            

            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Bobbity",
                        Street = "10 The street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "90210"
                    },
                    
                };
                
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, role1);
                //await userManager.AddToRoleAsync(user, role2);
                //await userManager.AddToRoleAsync(user, role3);

            }

            
        }
    }
}
