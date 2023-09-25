using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class PartyInvitesDbContext : DbContext
{
    public PartyInvitesDbContext(DbContextOptions<PartyInvitesDbContext> options) : base(options)
    {

    }


    public DbSet<GuestResponse> GuestResponses { get; set; }

}
