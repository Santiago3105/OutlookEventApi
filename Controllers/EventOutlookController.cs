namespace MicrosoftOutlook.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MicrosoftOutlook.Interface;
    using MicrosoftOutlook.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class EventOutlookController : ControllerBase
    {
        private readonly IEventClass EventClass;

        public EventOutlookController(IEventClass eventClass)
        {
            EventClass = eventClass;
        }

        [HttpPost("AddEvent")]
        public Microsoft.Graph.Event AddEvent(AddEventRequest addEventRequest)
        {
            return EventClass.AddEvent(addEventRequest);
        }

        [HttpPost("SeletEvent")]
        public Microsoft.Graph.Event SelectEvent(string? IdEvent)
        {
            return EventClass.SelectEvent(IdEvent);
        }

        [HttpDelete("DeleteEvent")]
        public string DeleteEvent(string IdEvent)
        {
            return EventClass.DeleteEvent(IdEvent);
        }

        [HttpPatch("UpdateEvent")]
        public string UpdateEvent(UpdateEventRequest eventRequest,string IdEvent)
        {
            return EventClass.UpdateEvent(eventRequest,IdEvent);
        }
    }
}

