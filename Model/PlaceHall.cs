namespace EventSeller.Model
{
    public class PlaceHall
    {
        public long ID { get; set; }
        public string HallName { get; set; }
        public PlaceAddress EventAddress { get; set; } 
        public ICollection<HallSector> HallSectors { get; set; }
    }
}
