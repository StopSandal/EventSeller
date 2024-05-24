namespace EventSeller.Model
{
    public class TicketSeat : IEntity
    {
        public long ID { get; set; }
        public string PlaceName { get; set; }
        public string PlaceType { get; set; }
        public int? PlaceRow { get; set; }
        public int? PlaceSeat {  get; set; }
        public long HallSectorID { get; set; }
        public virtual HallSector HallSector { get; set; }
    }
}
