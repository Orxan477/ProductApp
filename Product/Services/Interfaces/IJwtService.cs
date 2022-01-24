using Product.Data.Entities;
using System.Collections.Generic;

namespace Product.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(AppUser user, IList<string> roles);
    }
}
