using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Data_Management
{
    public static class Helper
    {
        /// <summary>
        /// Retrieves the specified connection string from the app.config file
        /// </summary>
        /// <param name="name">The reference name of the required conneciton string</param>
        /// <returns>The connection string details asd a string</returns>
        private static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        /// <summary>
        /// Creates a SQL Server connection object to connect to the database.
        /// </summary>
        /// <param name="name">The name of the connection string to be used during creation</param>
        /// <returns>A configured SQL Connection object</returns>
        public static SqlConnection CreateSQLConnection(string name)
        {
            return new SqlConnection(GetConnectionString(name));
        }

    }
}
