using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;

namespace MidwestGeeks.Lib
{
    public class EventBriteRepository
    {
        //playing with Eventbrite Api
        public String ListEvents()
        { 
            var appKey = ConfigurationManager.AppSettings["EventBriteApiKey"]; //my eventbrite app key
            var angleBracketId = "1647971612";
            var lcnugId = "66532490";
            var altnet = "58725219";
            var api_url = "https://www.eventbrite.com/json/organizer_list_events?app_key=" + appKey + "&id=" + angleBracketId;

            var request = WebRequest.Create(api_url);
            var response = request.GetResponse();

            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}
