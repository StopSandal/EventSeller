using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventSeller.Model
{
    public class Ticket : IEntity
    {
        public long ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public bool isSold { get; set; } = false;
        public DateTime? TicketStartDateTime { get; set; }
        public DateTime? TicketEndDateTime { get; set; }
        public long SeatID { get; set; }
        public virtual TicketSeat Seat { get; set; }
        public long EventID { get; set; }
        public virtual Event Event { get; set; } 
    }
}
