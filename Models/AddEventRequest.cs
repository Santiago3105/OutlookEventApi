namespace MicrosoftOutlook.Models
{
    public class AddEventRequest
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public string LocationName { get; set; }
        public List<Email> Attendees { get; set; }
        public string DateTimeStar { get; set; }
        public string DateTimeEnd{ get; set; }
        public string TransactionId { get; set; }
    }

    public class Email
    {
        public string Address { get; set; }
        public string Name { get; set; }
    }
}
