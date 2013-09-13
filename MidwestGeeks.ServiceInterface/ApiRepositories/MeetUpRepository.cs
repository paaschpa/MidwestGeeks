using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using System.Xml.Serialization;
using System.Globalization;
using MidwestGeeks.ServiceInterface.ApiRepositories;

namespace MidwestGeeks.Lib
{
    public class MeetUpRepository : ApiRepositoryBase
    {
        private static DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public IEnumerable<MeetUpEvent> ListEvents()
        {
            var appKey = ConfigurationManager.AppSettings["MeetUpApiKey"]; //my meetup app key
            var senchaGroupId = "1787911";
            var phpGroupId = "2963762";
            var chicagoRuby = "288361";
            var mchenryCraftsmanship = "2857802";

            var groups = new[] { senchaGroupId, phpGroupId, chicagoRuby, mchenryCraftsmanship };
            var api_url = @"http://api.meetup.com/2/events?key=" + appKey;

            return GetEvents<MeetUpEvent>(groups, (group) => api_url + "&group_id=" + group + "&sign=true&page=100&status=upcoming&format=xml");
        }

        protected override IEnumerable<MeetUpEvent> Deserialize<MeetUpEvent>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(MeetupContainer));
            var eventCollection = (MeetupContainer)serializer.Deserialize(stream);
            return eventCollection.EventCollection.Events.ToList() as IEnumerable<MeetUpEvent>;
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
        private static DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("time")]
        public String When { get; set; }

        [XmlElement("utc_offset")]
        public string TimeOffSet { get; set; }

        public string Date
        {
            get
            {
                var dbl = double.Parse(this.When, CultureInfo.InvariantCulture);
                return Epoch.AddMilliseconds(dbl).ToString();
            }
        }
        public string Time
        {
            get
            {
                var dbl = double.Parse(this.When, CultureInfo.InvariantCulture);
                var dt = Epoch.AddMilliseconds(dbl);
                return dt.AddMilliseconds(double.Parse(this.TimeOffSet, CultureInfo.InvariantCulture)).TimeOfDay.ToString();
            }
        }
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
        public string Address { get; set; }

        [XmlElement("address_2")]
        public string Address_2 { get; set; }

        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("state")]
        public string State { get; set; }

        [XmlElement("zip")]
        public string PostalCode { get; set; }
    }

    public class Group
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }

}
