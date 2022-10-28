using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Management.Models
{
    public class GamesPlayed
    {
        public int GamesPlayedID { get; set; }
        public string GameName { get; set; }
        public string GameType { get; set; }
        public int FKTeamID { get; set; }
    }
}
