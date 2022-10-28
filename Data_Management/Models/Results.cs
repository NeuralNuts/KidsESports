using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Management.Models
{
    public class Results
    {
        public int ResultID { get; set; }
        public int FKTeamID { get; set; }
        public int FKEventsID { get; set; }
        public int FKTeam_Opposing { get; set; }
        public int FKGamesPlayedID { get; set; }
        public string Result { get; set; }

        public Results(int team_id, int event_id, int games_played_id, int team_opposing)
        {
            FKTeamID = team_id;
            FKEventsID = event_id;
            FKGamesPlayedID = games_played_id;
            FKTeam_Opposing = team_opposing;
        }
    }
}