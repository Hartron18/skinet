using Microsoft.AspNetCore.Identity;
using SkinetApi.Entities.Identity;

namespace SkinetApi.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services)
        {
            var builder = Services.AddIdentity<AppUser, Role>(options => options.Stores.MaxLengthForKeys = 128);

            //builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<StoreDbContext>();
            builder.AddDefaultTokenProviders();
            //builder.AddSignInManager<SignInManager<AppUser>>();
            //builder.AddRoles<RoleManager<Role>>();

            return Services;
        }

        
    }
}
