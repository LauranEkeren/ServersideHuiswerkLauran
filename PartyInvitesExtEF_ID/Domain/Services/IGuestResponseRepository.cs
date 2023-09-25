using Domain.Models;

namespace Domain.Services;

public interface IGuestResponseRepository
{
    IEnumerable<GuestResponse> Responses { get; }

    GuestResponse? CreateResponse(GuestResponse response);
    GuestResponse? DeleteResponse(GuestResponse response);
    GuestResponse? ReadResponse(int id);
    GuestResponse? UpdateResponse(GuestResponse response);
}