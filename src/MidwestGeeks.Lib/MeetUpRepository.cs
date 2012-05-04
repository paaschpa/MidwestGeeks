using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using System.Xml.Serialization;

namespace MidwestGeeks.Lib
{
    public class MeetUpRepository
    {
        public IEnumerable<MeetUpEvent> ListEvents()
        {
            var appKey = ConfigurationManager.AppSettings["MeetUpApiKey"]; //my meetup app key
            var senchaGroupId = "1787911";
            var phpGroupId = "2963762";
            var chicagoRuby = "288361";
            var mchenryCraftsmanship = "2857802";

            var groups = new[] { senchaGroupId, phpGroupId, chicagoRuby, mchenryCraftsmanship };
            var api_url = @"http://api.meetup.com/2/events?key=" + appKey + "&group_id=" + 
                String.Join(", ", groups) + 
                "&sign=true&page=20&status=upcoming,past&format=xml";

            var request = WebRequest.Create(api_url);
            var response = request.GetResponse();

            var events = new List<MeetUpEvent>();
            using (var stream = response.GetResponseStream())
            {
                var serializer = new XmlSerializer(typeof(MeetupContainer));
                var eventCollection = (MeetupContainer)serializer.Deserialize(stream);
                events.AddRange(eventCollection.EventCollection.Events);
            }

            return events;
        }
    }

    [Serializable, XmlRoot("results")]
    public class MeetupContainer
    {
        public string Head { get; set; }

        [XmlElement("items")]
        public EventCollection EventCollection { get; set; }
    }

    public class EventCollection
    {
        [XmlElement("item")]
        public MeetUpEvent[] Events { get; set; }
    }

    public class MeetUpEvent 
    {
        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("time")]
        public String When { get; set; }
       
        [XmlElement("venue")]
        public MeetupVenue Venue { get; set; }

        [XmlElement("group")]
        public Group Group { get; set; }
    }

    public class MeetupVenue
    {
        [XmlElement("id")]
        public string ID { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("address_1")]
        public string Address {get; set;}

        [XmlElement("address_2")]
        public string Address_2 {get; set;}

        [XmlElement("city")]
        public string City {get; set;}

        [XmlElement("state")]
        public string State {get; set;}

        [XmlElement("zip")]
        public string PostalCode { get; set; }
    }

    public class Group
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }
    
}
