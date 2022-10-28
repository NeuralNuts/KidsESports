using Data_Management;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Tekkies_Custom_PCs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
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
    }
}
