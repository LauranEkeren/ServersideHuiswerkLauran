using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;
public class PartyInvitesSeedData : ISeedData
{
    private PartyInvitesDbContext _context;
    private ILogger<PartyInvitesSeedData> _logger;

    public PartyInvitesSeedData(PartyInvitesDbContext context, ILogger<PartyInvitesSeedData> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task EnsurePopulated(bool dropExisting = false)
    {
        if (dropExisting)
        {
            _context.Database.EnsureDeleted();
        }
        _context.Database.Migrate();
        // optie: context.Database.EnsureCreated();
        if (_context.GuestResponses?.Count() == 0)
        {
            _logger.LogInformation("Preparing to seed database");
            _context.GuestResponses.AddRange(new[]
            {
                    new GuestResponse { Name = "Hubert Farnsworth", Email = "bla@da.nl", Phone = "12345", WillAttent = true, },
                    new GuestResponse { Name = "Philip J Fry", Email = "bla@da.nl", Phone = "12345", WillAttent = true, },
                    new GuestResponse { Name = "Zapp Brannigan", Email = "bla@da.nl", Phone = "12345", WillAttent = true, },
                    new GuestResponse { Name = "Amy Wong", Email = "bla@da.nl", Phone = "12345", WillAttent = true, },
                    new GuestResponse { Name = "Bender Bending Rodriguez", Email = "bla@da.nl", Phone = "12345", WillAttent = false, },
                    new GuestResponse { Name = "Turanga Lela", Email = "bla@da.nl", Phone = "12345", WillAttent = true, },
                });
            _context.SaveChanges();
            _logger.LogInformation("Database seeded");
        }
        else
        {
            _logger.LogInformation("Database not seeded");
        }
    }
}
