namespace MicrosoftOutlook.Models
{
    public class UpdateEventRequest
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public string LocationName { get; set; }
        public List<Email> Attendees { get; set; }
        public string DateTimeStar { get; set; }
        public string DateTimeEnd { get; set; }
    }
}
