using Microsoft.EntityFrameworkCore;

namespace EventSeller.Model.EF
{
    public class SellerContext : DbContext
    {
        private const string ConnectionString = "Server=DESKTOP-UAUG3OJ;Database=EventSeller;Trusted_Connection=True;TrustServerCertificate=True;";
        DbSet<Ticket> Tickets { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<PlaceAddress> PlaceAddresses { get; set; }
        DbSet<PlaceHall> PlaceHalls { get; set; }
        DbSet<HallSector> HallSectors { get; set; }
        DbSet<TicketSeat> Seats { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

    }
}
