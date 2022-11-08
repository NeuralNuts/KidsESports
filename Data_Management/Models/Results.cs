namespace Data_Management.Models
{
    public class Results
    {
        #region Data Structure
        public int ResultsID { get; set; }
        public int FKTeamID { get; set; }
        public int FKEventsID { get; set; }
        public int FKTeamID_Opposing { get; set; }
        public int FKGamesPlayedID { get; set; }
        public string Result { get; set; }

        public Results()
        {

        }

        public Results(int team_id, int event_id, int games_played_id, int team_opposing)
        {
            FKTeamID = team_id;
            FKEventsID = event_id;
            FKGamesPlayedID = games_played_id;
            FKTeamID_Opposing = team_opposing;
        }
        #endregion
    }
}