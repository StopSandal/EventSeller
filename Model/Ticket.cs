using System.ComponentModel.DataAnnotations;

namespace EventSeller.Model
{
    public class Ticket
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public DateTime? TicketStartDateTime { get; set; }
        public DateTime? TicketEndDateTime { get; set; }
        public TicketSeat Seat { get; set; }
        public Event Event { get; set; } 
    }
}
