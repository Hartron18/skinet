using Microsoft.AspNetCore.Identity;

namespace SkinetApi.Entities.Identity
{
    public class Role : IdentityRole
    {
        //public Role(string roleName) : base(roleName)
        //{
        //}
        public string Description { get; set; }
    }
}
