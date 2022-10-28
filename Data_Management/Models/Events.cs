using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Management.Models
{
    public class Events
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public int FKTeamID { get; set; }
        public string EventDate { get; set; }
    }
}
