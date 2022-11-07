#region Imports
using System.Windows;
#endregion

namespace Data_Management
{
    public partial class App : Application
    {
        #region Var
        public App()
        {
            DBS_Builder builder = new DBS_Builder();
            builder.CreateDatabase();
            if (builder.DoTablesExist() == false)
            {
                builder.BuildDatabaseTables();
                builder.SeedDatabaseTables();
            }
        }
        #endregion
    }
}
