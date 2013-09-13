using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidwestGeeks.ServiceModel.Types
{
    public class Meeting
    {
        public string Description { get; set; }
        public DateTime When { get; set; }
        public string Where { get; set; }
    }
}
