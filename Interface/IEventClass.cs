namespace MicrosoftOutlook.Interface
{
    using MicrosoftOutlook.Models;
    public interface IEventClass
    {
        Microsoft.Graph.Event AddEvent(AddEventRequest addEventRequest);
        Microsoft.Graph.Event SelectEvent(string? IdEvento);
        string DeleteEvent(string IdEvento);
        string UpdateEvent(UpdateEventRequest eventRequest, string IdEvento);
    }
}
