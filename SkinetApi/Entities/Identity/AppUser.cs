using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinetApi.Entities.Identity
{
    public class AppUser: IdentityUser
    {
        public string DisplayName { get; set; }
        
        public Address Address { get; set; }
    }
}
