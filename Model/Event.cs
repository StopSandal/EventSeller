namespace EventSeller.Model
{
    public class Event
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartEventDateTime { get; set; }
        public DateTime EndEventDateTime { get; set; }
    }
}
