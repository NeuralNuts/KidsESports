namespace Data_Management.Models
{
    public class ResultView
    {
        #region Data Structure
        public int ResultID { get; set; }
        public string Team { get; set; }
        public string Opposing { get; set; }
        public string GameName { get; set; }
        public string EventName { get; set; }
        public string Result { get; set; }
        #endregion
    }
}
