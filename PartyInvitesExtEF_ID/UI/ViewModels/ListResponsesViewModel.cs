using Domain.Models;
using UI.ViewHelpers;

namespace UI.ViewModels;
public class ListResponsesViewModel
{
    public IEnumerable<GuestResponse> GuestResponses { get; set; } = Enumerable.Empty<GuestResponse>();

    public PagingInfo PagingInfo { get; set; } = new();

    public GuestResponseFilterInfo FilterInfo { get; set; } = new();

}
