namespace PartyInvites.Models
{
    public interface IRepository
    {
        public Task AddResponse(GuestResponse response);
        public IEnumerable<GuestResponse> GetResponses();
    }
}
