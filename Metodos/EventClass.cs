namespace MicrosoftOutlook.Metodos
{
    using Microsoft.Graph;
    using MicrosoftOutlook.Interface;
    using MicrosoftOutlook.Models;
    using RestSharp;
    using System.Text.Json;

    public class EventClass : IEventClass
    {
        private readonly IAuthClass AuthClass;

        public EventClass(IAuthClass authClass)
        {
            AuthClass = authClass;
        }
        public Microsoft.Graph.Event AddEvent(Models.AddEventRequest addEventRequest)
        {
            var client = new RestClient("https://graph.microsoft.com/v1.0/me/events");
            var request = new RestRequest();
            var attendees = new List<Attendee>();
            foreach (var item in addEventRequest.Attendees)
            {
                attendees.Add(new Attendee
                {
                    EmailAddress = new EmailAddress
                    {
                        Address = item.Address,
                        Name = item.Name
                    },
                    Type = AttendeeType.Required
                });
            }

            var Event = new Microsoft.Graph.Event
            {
                Subject = addEventRequest.Subject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = addEventRequest.Content
                },
                Start = new DateTimeTimeZone
                {
                    DateTime = addEventRequest.DateTimeStar,
                    TimeZone = "America/Argentina/Buenos_Aires"
                },
                End = new DateTimeTimeZone
                {
                    DateTime = addEventRequest.DateTimeEnd,
                    TimeZone = "America/Argentina/Buenos_Aires"
                },
                Location = new Location
                {
                    DisplayName = addEventRequest.LocationName
                },
                Attendees = attendees,
                AllowNewTimeProposals = true,
                TransactionId = addEventRequest.TransactionId
            };

            request.AddHeader("Authorization", "Bearer " + AuthClass.GetToken());
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Prefer", "outlook.timezone = \"America/Argentina/Buenos_Aires\"");
            request.AddBody(Event, "application/json");
            var response = client.Execute(request, Method.Post);          
            return JsonSerializer.Deserialize<Microsoft.Graph.Event>(response.Content);
        }

        public Microsoft.Graph.Event SelectEvent(string? IdEvento)
        {
            var client = new RestClient(" https://graph.microsoft.com/v1.0/me/events" + (string.IsNullOrEmpty(IdEvento) == false ? $"/{IdEvento}" : "" ));
            var request = new RestRequest();
            request.AddHeader("Authorization", "Bearer " + AuthClass.GetToken());
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Prefer", "outlook.timezone = \"America/Argentina/Buenos_Aires\"");
            var response = client.Execute(request, Method.Get);
            return JsonSerializer.Deserialize<Microsoft.Graph.Event>(response.Content);
        }

        public string DeleteEvent(string IdEvento)
        {
            var client = new RestClient("https://graph.microsoft.com/v1.0/me/events/" + IdEvento);
            var request = new RestRequest();
            request.AddHeader("Authorization", "Bearer " + AuthClass.GetToken());
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Prefer", "outlook.timezone = \"America/Argentina/Buenos_Aires\"");
            var response = client.Execute(request, Method.Delete);
            return response.Content;
        }

        public string UpdateEvent(Models.UpdateEventRequest eventRequest, string IdEvento)
        {
            var client = new RestClient("https://graph.microsoft.com/v1.0/me/events/" + IdEvento);
            var request = new RestRequest();
            var attendees = new List<Attendee>();

            foreach (var item in eventRequest.Attendees)
            {
                attendees.Add(new Attendee
                {
                    EmailAddress = new EmailAddress
                    {
                        Address = item.Address,
                        Name = item.Name
                    },
                    Type = AttendeeType.Required
                });
            }

            var Event = new Microsoft.Graph.Event
            {
                Subject = eventRequest.Subject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = eventRequest.Content
                },
                Start = new DateTimeTimeZone
                {
                    DateTime = eventRequest.DateTimeStar,
                    TimeZone = "America/Argentina/Buenos_Aires"
                },
                End = new DateTimeTimeZone
                {
                    DateTime = eventRequest.DateTimeEnd,
                    TimeZone = "America/Argentina/Buenos_Aires"
                },
                Location = new Location
                {
                    DisplayName = eventRequest.LocationName
                },
                Attendees = attendees,
            };


            request.AddHeader("Authorization", "Bearer " + AuthClass.GetToken());
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Prefer", "outlook.timezone = \"America/Argentina/Buenos_Aires\"");
            request.AddBody(Event, "application/json");
            var response = client.Execute(request, Method.Patch);
            return response.Content;
        }
    }
}
