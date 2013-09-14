using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidwestGeeks.ServiceModel.Types
{
    public class Meeting
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Day { get; set; }
        public string Time { get; set; }
        public string Where { get; set; }
        public string Source { get; set; }
    }
}
