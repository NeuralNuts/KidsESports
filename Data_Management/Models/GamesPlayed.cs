namespace Data_Management.Models
{
    public class GamesPlayed
    {
        #region Data Structure
        public int GamesPlayedID { get; set; }
        public string GameName { get; set; }
        public string GameType { get; set; }
        public int FKTeamID { get; set; }
        #endregion
    }
}
