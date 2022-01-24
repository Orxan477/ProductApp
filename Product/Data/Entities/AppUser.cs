using Microsoft.AspNetCore.Identity;

namespace Product.Data.Entities
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
