using System;
using System.Collections.Generic;
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
                    Description = x.Description, 
                    When = x.Date + " " + x.Time, 
                    Where = x.Venue != null ? x.Venue.Name : "UnKnown"
                });

            var eventbrites = new EventBriteRepository().ListEvents()
                .Where(x => x.When2 >= DateTime.Today.AddDays(-1))
                .Select(x => new Meeting()
                    {
                       Description = x.Description,
                       When = x.When.ToString(),
                       Where = x.Venue != null ? x.Venue.Name : "UnKnown"
                    });

            upcomingMeetings.AddRange(meetups);
            upcomingMeetings.AddRange(eventbrites);

            return upcomingMeetings;
        }
    }
}
