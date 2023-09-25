namespace PartyInvites.Models
{
    public class Repository : IRepository
    {
        private readonly InviteDbContext _context;

        public Repository( InviteDbContext context)
        {
            _context = context;

        }
        public async Task AddResponse(GuestResponse response)
        {
            _context.GuestResponses.Add(response);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<GuestResponse> GetResponses()
        {
            return _context.GuestResponses;
        }
    }
}
