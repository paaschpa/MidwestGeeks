using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidwestGeeks.Lib;
using MidwestGeeks.ServiceModel.Types;
using ServiceStack.ServiceInterface;

namespace MidwestGeeks.ServiceInterface
{
    public class MeetingsService : Service
    {
        public List<Meeting> Get(UpcomingMeetings request)
        {
            var upcomingMeetings = new List<Meeting>();
            var meetups = new MeetUpRepository().ListEvents()
                .Select(x => new Meeting {
                    Name = x.Group.Name,
                    Description = x.Description, 
                    Day = x.Date, 
                    Time = x.Time,
                    Where = x.Venue != null ? x.Venue.Name : "UnKnown",
                    Source = "MeetUp"
                });

            var eventbrites = new EventBriteRepository().ListEvents()
                .Where(x => x.Date >= DateTime.Today.AddDays(-1))
                .Select(x => new Meeting()
                    {
                       Name = x.Title,
                       Description = x.Description,
                       Day = x.Date,
                       Time = x.Date.ToString("t", CultureInfo.CreateSpecificCulture("en-us")),
                       Where = x.Venue != null ? x.Venue.Name : "UnKnown",
                       Source = "EventBrite"
                    });

            upcomingMeetings.AddRange(meetups);
            upcomingMeetings.AddRange(eventbrites);

            return upcomingMeetings.OrderBy(x => x.Day).ToList();
        }
    }
}
