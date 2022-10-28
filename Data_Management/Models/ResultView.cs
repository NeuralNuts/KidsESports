using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Management.Models
{
    public class ResultView
    {
        public int ResultID { get; set; }
        public string TeamName { get; set; }
        public string OpposingTeam { get; set; }
        public string GameName { get; set; }
        public string EventName { get; set; }
        public string Result { get; set; }
    }
}
