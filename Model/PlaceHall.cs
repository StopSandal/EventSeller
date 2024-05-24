using Microsoft.EntityFrameworkCore;

namespace EventSeller.Model
{
    [Index(nameof(HallName), nameof(PlaceAddressID), IsUnique = true)]
    public class PlaceHall : IEntity
    {
        public long ID { get; set; }
        public string HallName { get; set; }
        public long PlaceAddressID { get; set; }
        public virtual PlaceAddress PlaceAddress { get; set; }
        public virtual ICollection<HallSector> HallSectors { get; set; }
    }
}
