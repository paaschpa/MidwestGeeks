﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using System.Xml.Serialization;
using MidwestGeeks.ServiceInterface.ApiRepositories;

namespace MidwestGeeks.Lib
{
    public class EventBriteRepository : ApiRepositoryBase
    {
        //playing with Eventbrite Api
        public IEnumerable<EventBriteEvent> ListEvents()
        {
            var appKey = ConfigurationManager.AppSettings["EventBriteApiKey"]; //my eventbrite app key
            var angleBracketId = "1647971612";
            var lcnugId = "2353411364"; //66532490 also
            var altnet = "58725219";
            var api_url = "https://www.eventbrite.com/xml/organizer_list_events?app_key=" + appKey; 

            var groupIds = new[] { angleBracketId, lcnugId, altnet };
            return GetEvents<EventBriteEvent>(groupIds, (groupId) => { return api_url + "&id=" + groupId; });
        }

        protected override IEnumerable<EventBriteEvent> Deserialize<EventBriteEvent>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(EventBriteContainer));
            var eventCollection = (EventBriteContainer)serializer.Deserialize(stream);
            return eventCollection.Events.ToList() as IEnumerable<EventBriteEvent>;
        }
    }

    [Serializable, XmlRoot("events")]
    public class EventBriteContainer
    {
        [XmlElement("event")]
        public EventBriteEvent[] Events { get; set; }
    }

    [Serializable]
    public class EventBriteEvent
    {
        [XmlElement("id")]
        public long ID { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("start_date")]
        public string When { get; set; }

        [XmlElement("timezone")]
        public String TimeZone { get; set; }

        [XmlElement("venue")]
        public EventBriteVenue Venue { get; set; }

        [XmlElement("organizer")]
        public Organizer Organizer { get; set; }

        public DateTime Date { get
        {
            DateTime when;
            //DateTime.TryParse(When.Substring(0, When.IndexOf(" ")), out when);
            DateTime.TryParse(When.Replace(" ", " T"), out when);
            return when;
        } }

        public string Time { get { return Date.Hour + ":" + Date.Minute + " " + TimeZone; } }
 
    }

    [Serializable]
    public class EventBriteVenue
    {
        [XmlElement("id")]
        public long ID { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("address")]
        public string Address { get; set; }

        [XmlElement("address_2")]
        public string Address_2 { get; set; }

        [XmlElement("postal_code")]
        public string PostalCode { get; set; }
    }

    [Serializable]
    public class Organizer
    {
        [XmlElement("id")]
        public long ID { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
    }
}

