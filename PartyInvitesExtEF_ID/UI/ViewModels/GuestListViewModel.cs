
using Domain.Models;

namespace UI.ViewModels;
public class GuestListViewModel
{
    public IEnumerable<GuestResponse>? GuestResponses { get; internal set; }
    public PagingInfo? PagingInfo { get; internal set; }
}
