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
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
     
        public static SqlConnection CreateSQLConnection(string name)
        {
            return new SqlConnection(GetConnectionString(name));
        }
        #endregion
    }
}
