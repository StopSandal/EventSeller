using Microsoft.EntityFrameworkCore;

namespace EventSeller.Model
{
    [Index(nameof(SectorName), nameof(PlaceHallID), IsUnique = true)]
    public class HallSector : IEntity
    {
        public long ID { get; set; }
        public string SectorName { get; set; }
        public long PlaceHallID { get; set; }
        public PlaceHall PlaceHall { get; set; }
        public virtual ICollection<TicketSeat> TicketSeats { get; set; }
    }
}
