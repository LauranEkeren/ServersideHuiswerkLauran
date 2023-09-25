using Domain.Models;
using Domain.Services;
using Infrastructure.Data;

namespace Infrastructure.Repositories;
public class GuestResponseRepository : IGuestResponseRepository
{
    private readonly PartyInvitesDbContext _dbContext;

    public GuestResponseRepository(PartyInvitesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IEnumerable<GuestResponse> Responses => _dbContext.GuestResponses.ToList();

    public GuestResponse? CreateResponse(GuestResponse response)
    {
        _dbContext.GuestResponses.Add(response);
        _dbContext.SaveChanges();

        return response;
    }

    public GuestResponse? ReadResponse(int id)
    {
        return _dbContext.GuestResponses.FirstOrDefault(r => r.Id == id);
    }

    public GuestResponse? UpdateResponse(GuestResponse response)
    {
        var entityToUpdate = _dbContext.GuestResponses.FirstOrDefault(r => r.Id == response.Id);
        if (entityToUpdate != null)
        {
            entityToUpdate.Email = response.Email;
            entityToUpdate.Phone = response.Phone;
            entityToUpdate.WillAttent = response.WillAttent;

            _dbContext.SaveChanges();
        }

        return entityToUpdate;
    }

    public GuestResponse? DeleteResponse(GuestResponse response)
    {
        var entityToRemove = _dbContext.GuestResponses.FirstOrDefault(r => r.Id == response.Id);
        if (entityToRemove != null)
        {
            _dbContext.GuestResponses.Remove(entityToRemove);
            _dbContext.SaveChanges();
        }

        return entityToRemove;
    }
}
