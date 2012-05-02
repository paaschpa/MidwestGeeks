using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;

namespace MidwestGeeks.Lib
{
    public class MeetUpRepository
    {
        public String ListEvents()
        {
            var appKey = ConfigurationManager.AppSettings["MeetUpApiKey"]; //my meetup app key
            var senchaGroupId = "1787911";
            var phpGroupId = "2963762";
            var chicagoRuby = "288361";
            var mchenryCraftsmanship = "2857802";

            var api_url = "http://api.meetup.com/2/events?key=" + appKey + "&group_id=" + senchaGroupId + "&sign=true&page=20";

            var request = WebRequest.Create(api_url);
            var response = request.GetResponse();

            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream);

            return reader.ReadToEnd();
            return "";
        }

    }
}
