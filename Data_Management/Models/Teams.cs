using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Management.Models
{
    public class Teams: IDisplay
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string PrimaryContact { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public int CompetitionPoints { get; set; }
    }
}
