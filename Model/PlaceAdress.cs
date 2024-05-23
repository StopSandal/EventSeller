namespace EventSeller.Model
{
    public class PlaceAddress
    {
        public long ID { get; set; }
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public ICollection<PlaceHall> PlaceHall { get; set; }


    }
}
