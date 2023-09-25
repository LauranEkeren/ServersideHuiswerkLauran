using Microsoft.EntityFrameworkCore;

namespace PartyInvites.Models
{
    public class InviteDbContext : DbContext
    {
        public DbSet<GuestResponse> GuestResponses { get; set; }
        public InviteDbContext(DbContextOptions<InviteDbContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GuestResponse>().HasData(
                new GuestResponse() { Id = 1, Name = "Yian-Garuga", Email = "Yian@gmail.com", Phone = "0611202273", WillAttend = true},
                new GuestResponse() { Id = 2, Name = "Tzitzi-ya-ku", Email = "Yian@gmail.com", Phone = "0611202273", WillAttend = true }
            );
        }
    }
}
