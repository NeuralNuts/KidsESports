#region Imports
using System.Configuration;
using System.Data.SqlClient;
#endregion

namespace Data_Management
{
    public static class Helper
    {
        #region Functions
        private static string GetConnectionString(string name)
        {
            //Gets the connection of the database//
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
     
        public static SqlConnection CreateSQLConnection(string name)
        {
            //Builds the SQL connection//
            return new SqlConnection(GetConnectionString(name));
        }
        #endregion
    }
}
