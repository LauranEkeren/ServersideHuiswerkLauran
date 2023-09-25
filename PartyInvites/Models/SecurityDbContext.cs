using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PartyInvites.Models
{
    public class SecurityDbContext : IdentityDbContext
    {
        public SecurityDbContext (DbContextOptions<SecurityDbContext> options) : base(options)
        {

        }

    }
}
