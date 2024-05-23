namespace EventSeller.Model
{
    public class TicketSeat
    {
        public Guid ID { get; set; }
        public string PlaceName { get; set; }
        public string PlaceType { get; set; }
        public int? PlaceRow { get; set; }
        public int? PlaceSeat {  get; set; }
        public HallSector HallSector { get; set; }
    }
}
