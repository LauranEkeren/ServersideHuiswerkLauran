using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public interface IPartyInvitesDbContext
{
    DbSet<GuestResponse> GuestResponses { get; set; }
}