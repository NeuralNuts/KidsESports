using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Management.Models
{
    /// <summary>
    /// Data model representing how the product data is to be viewed by the user as opposed to the actual database structure which has a foreign key in place of 
    /// the category name.
    /// </summary>
    public class GamesPlayedView
    {
        public int GamesPlayedID { get; set; }
        public string GameName { get; set; }
        public string GameType { get; set; }
        public string TeamName { get; set; }
    }
}
