namespace EventSeller.Model
{
    public class HallSector
    {
        public long ID { get; set; }
        public string SectorName { get; set; }
        public PlaceHall PlaceHall { get; set; }
        public ICollection<TicketSeat> TicketSeats { get; set; }
    }
}
