using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidwestGeeks.Lib;
using MidwestGeeks.ServiceModel.Types;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.OrmLite;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;

namespace MidwestGeeks.ServiceInterface
{
    public class MeetingsService : Service
    {
        public object Get(UpcomingMeetings request)
        {
            var cacheKey = "upcoming_meetings";
            var cacheTime = new TimeSpan(0, 4, 0, 0); //cache for four hours

            return RequestContext.ToOptimizedResultUsingCache(Cache, cacheKey, cacheTime, UpcomingMeetings);
        }

        private static List<Meeting> UpcomingMeetings()
        {
            var upcomingMeetings = new List<Meeting>();
            var meetups = new MeetUpRepository().ListEvents()
                                                .Select(x => new Meeting
                                                    {
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
                                                                Time =
                                                                    x.Date.ToString("t",
                                                                                    CultureInfo.CreateSpecificCulture("en-us")),
                                                                Where = x.Venue != null ? x.Venue.Name : "UnKnown",
                                                                Source = "EventBrite"
                                                            });

            upcomingMeetings.AddRange(meetups);
            upcomingMeetings.AddRange(eventbrites);
            return upcomingMeetings;
        }

        public Meeting Post(Meeting meeting)
        {
            Db.Save<Meeting>(meeting);
            return meeting;
        }
    }
}
