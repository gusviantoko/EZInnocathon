using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZInnocathon
{
    public class WorkItem
    {
        public string name { get; set; }
        public string type { get; set; }
        public string location { get; set; }
        public int monitor { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public bool runOnStartup { get; set; }
        public string state{ get; set; }
    }
}
