using Domain.Services;
using Domain.Models;

namespace Infrastructure.Repositories;
public class InMemoryGuestResponseRepository : IGuestResponseRepository
{
    private List<GuestResponse> _responses = new();

    public InMemoryGuestResponseRepository()
    {
        PopulateDummyData();
    }

    public IEnumerable<GuestResponse> Responses => _responses;

    public GuestResponse? CreateResponse(GuestResponse response)
    {
        Console.WriteLine(response);

        int nextId = _responses.Max(x => x.Id) + 1;
        response.Id = nextId;

        _responses.Add(response);
        return response;
    }

    public GuestResponse? DeleteResponse(GuestResponse response)
    {
        var responseToDelete = _responses.FirstOrDefault(r => r.Id == response.Id);
        if (responseToDelete != null)
        {
            _responses.Remove(responseToDelete);
        }

        return responseToDelete;
    }

    public GuestResponse? ReadResponse(int id)
    {
        return _responses.FirstOrDefault(r => r.Id == id);
    }

    public GuestResponse? UpdateResponse(GuestResponse response)
    {
        var entityToUpdate = _responses.FirstOrDefault(r => r.Id == response.Id);
        if (entityToUpdate != null)
        {
            entityToUpdate.Email = response.Email;
            entityToUpdate.Phone = response.Phone;
            entityToUpdate.WillAttent = response.WillAttent;
        }



        return entityToUpdate;
    }

    private void PopulateDummyData()
    {
        _responses.AddRange(
            new[]
            {
                new GuestResponse {Id = 1, Name = "Hubert Farnsworth", Email = "bla@da.nl", Phone = "12345", WillAttent = true, },
                new GuestResponse {Id = 2, Name = "Philip J Fry", Email = "bla@da.nl", Phone = "12345", WillAttent = true, },
                new GuestResponse {Id = 3, Name = "Zapp Brannigan", Email = "bla@da.nl", Phone = "12345", WillAttent = true, },
                new GuestResponse {Id = 4, Name = "Amy Wong", Email = "bla@da.nl", Phone = "12345", WillAttent = true, },
                new GuestResponse {Id = 5, Name = "Bender Bending Rodriguez", Email = "bla@da.nl", Phone = "12345", WillAttent = false, },
                new GuestResponse {Id = 6, Name = "Turanga Lela", Email = "bla@da.nl", Phone = "12345", WillAttent = true, },
            }
        );

    }
}
