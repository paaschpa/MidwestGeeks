using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;

namespace MidwestGeeks.ServiceModel.Types
{
    [Route("/UpcomingMeetings")]
    public class UpcomingMeetings : IReturn<List<Meeting>>
    {
        public DateTime Today { get; set; }
    }
}
