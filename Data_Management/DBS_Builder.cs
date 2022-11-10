#region Imports
using Dapper;
using Data_Management.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Data_Management
{
    public class DBS_Builder: DataAccess
    {
        public void CreateDatabase()
        {
            SqlConnection connection = Helper.CreateSQLConnection("Default");
           
            try
            {
                string connectionString = $"Data Source={connection.DataSource}; Integrated Security = True";
                string query = $"IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name ='{connection.Database}') " +
                               $" CREATE DATABASE {connection.Database}";

                using (connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query,connection))
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }

            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Runs a query against the database to get a count of how many  base tables exist in the database.
        /// </summary>
        /// <returns>A confirmation of whther there are tables (TRUE) or not (FALSE)</returns>
        public bool DoTablesExist()
        {
            //Our using statemtnqwhich builds our connection and disposes of it once finished.
            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                //Quey to request the count of how many base tables are in the database structure. Base tables refers to user
                //built tables and ignores inbuild tables such as index tables and reference/settings tables.
                string query = $"SELECT COUNT(*) FROM {connection.Database}.INFORMATION_SCHEMA.TABLES " +
                               $"WHERE TABLE_TYPE = 'BASE TABLE'";
                //Sends the query to the databse and stores the returned table count.
                int count = connection.QuerySingle<int>(query);

                //If the count is above 0 return true, otherwise return false to indicate whether the databse has tabes or not.
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Method to send a request to the databse to create a new databse table. This method requires the table name and column/attributes details to be pre-populated and 
        /// passed to the method for it to work.
        /// </summary>
        /// <param name="name">The name to be given to the table when created.</param>
        /// <param name="structure">A string oulining all the table column/attributes and their names, types and any other special rules for each of them such as PK 
        ///                         identification, nullability rules and foreighn key connections.</param>
        private void CreateTable(string name, string structure)
        {
            try
            {
                //Partial query to build table in database. Parameters passe to method will be inserted to complete the query string.
                string query = $"CREATE TABLE {name} ({structure})";
                //Our using statemtnqwhich builds our connection and disposes of it once finished.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Passes the query to the databse to be perfomed.
                    connection.Execute(query);
                }
            } 
            catch (Exception e)
            {
                //Log error on failure
            }
           
        }

        /// <summary>
        /// Runs all the separate methods to create all the database tables, ensuring to run them in the correct sequence to ensure tables with foreign key declarations are 
        /// not made until after the tables they are referencing are already created.
        /// </summary>
        public void BuildDatabaseTables()
        {
            BuildTeamsTable();
            BuildEventsTable();
            BuildGamesPlayedTable();
            BuildResultsTable();
        }

        /// <summary>
        /// Runs all the separate methods to populate test data into all the tables, ensuring to run them in the correct sequence to ensure tables with foreign key declarations are 
        /// not seeded until after the tables they are referencing are already completed and contain data.
        /// </summary>
        public void SeedDatabaseTables()
        {
            SeedTeamTable();
            SeedEventsTable();
            SeedGamesPlayedTable();
            SeedResultsTable();
        }

        /// <summary>
        /// Outlines the table structure of the Customer table and passes it to the CreateTable() method to be built.
        /// Each column/attribute is defined using the following format: 
        ///     <Name> <DataType> <Rules>
        /// 
        /// The rules for each attribute use the following options:
        ///     IDENTITY (1,1) - Sets auto-incrementation for the column with a start number and increment matching the provided numbers
        ///     PRIMARY KEY - Marks the clumn as the primary key for the table
        ///     NULL or NOT NULL sets the nullability of the column accordingly. If not defined the NULL 
        /// 
        /// </summary>
        private void BuildTeamsTable()
        {
            //Stores desired table name
            string tableName = "Teams";
            //Outlines structure of table
            string tableStructure = "TeamID BIGINT IDENTITY (1,1) PRIMARY KEY, " +
                                    "TeamName VARCHAR(50) NOT NULL, " +
                                    "PrimaryContact VARCHAR(50) NOT NULL, " +
                                    "ContactPhone VARCHAR(20) NOT NULL, " +
                                    "ContactEmail VARCHAR(50) NOT NULL, " +
                                    "CompetitionPoints BIGINT NOT NULL";
            //Pases name and strucutre to creation method.
            CreateTable(tableName, tableStructure);
        }

        /// <summary>
        /// Outlines the table structure of the Category table and passes it to the CreateTable() method to be built.
        /// Each column/attribute is defined using the following format: 
        ///     <Name> <DataType> <Rules>
        /// 
        /// The rules for each attribute use the following options:
        ///     IDENTITY (1,1) - Sets auto-incrementation for the column with a start number and increment matching the provided numbers
        ///     PRIMARY KEY - Marks the clumn as the primary key for the table
        ///     NULL or NOT NULL sets the nullability of the column accordingly. If not defined the NULL 
        /// 
        /// </summary>
        private void BuildGamesPlayedTable()
        {
            //Stores desired table name
            string tableName = "GamesPlayed";
            //Outlines structure of table
            string tableStructure = "GamesPlayedID BIGINT IDENTITY(1,1) PRIMARY KEY, " +
                                    "GameName VARCHAR(50) NOT NULL, " +
                                    "GameType VARCHAR(50) NOT NULL " +
                                    "ON UPDATE Cascade ON DELETE Cascade ";
            //Pases name and strucutre to creation method.
            CreateTable(tableName, tableStructure);
        }

        /// <summary>
        /// Outlines the table structure of the Products table and passes it to the CreateTable() method to be built.
        /// Each column/attribute is defined using the following format: 
        ///     <Name> <DataType> <Rules>
        /// 
        /// The rules for each attribute use the following options:
        ///     IDENTITY (1,1) - Sets auto-incrementation for the column with a start number and increment matching the provided numbers
        ///     PRIMARY KEY - Marks the clumn as the primary key for the table
        ///     NULL or NOT NULL sets the nullability of the column accordingly. If not defined the NULL 
        /// 
        /// </summary>
        private void BuildEventsTable()
        {
            //Stores desired table name
            string tableName = "Events";
            //Outlines structure of table
            //Also defines a foreigh key reference to the Category table using the following format for foreign key connections:
            //  FOREIGN KEY (<FK column name>) REFERENCES <Table being referenced>(<Primary Key of referecned table>)

            //Constrint rules for the foreigh key(optional) can be dfined using the following format:
            //  ON DELETE <Rule> ON CASCADE <Rule>
            //
            string tableStructure = "EventID BIGINT IDENTITY(1,1) PRIMARY KEY, " +
                                    "EventName VARCHAR(50) NOT NULL, " +
                                    "EventLocation VARCHAR(50) NOT NULL, " +
                                    "EventDate VARCHAR(50) NOT NULL " + 
                                    "ON UPDATE Cascade ON DELETE Cascade ";
            //Pases name and strucutre to creation method.
            CreateTable(tableName, tableStructure);
        }

        /// <summary>
        /// Outlines the table structure of the Builds table and passes it to the CreateTable() method to be built.
        /// Each column/attribute is defined using the following format: 
        ///     <Name> <DataType> <Rules>
        /// 
        /// The rules for each attribute use the following options:
        ///     IDENTITY (1,1) - Sets auto-incrementation for the column with a start number and increment matching the provided numbers
        ///     PRIMARY KEY - Marks the clumn as the primary key for the table
        ///     NULL or NOT NULL sets the nullability of the column accordingly. If not defined the NULL 
        /// 
        /// </summary>
        private void BuildResultsTable()
        {
            //Stores desired table name
            string tableName = "Result";
            //Outlines structure of table
            //Also defines a foreigh key reference to the Category table using the following format for foreign key connections:
            //  FOREIGN KEY (<FK column name>) REFERENCES <Table being referenced>(<Primary Key of referecned table>)

            //Constrint rules for the foreigh key(optional) can be dfined using the following format:
            //  ON DELETE <Rule> ON CASCADE <Rule>
            //
            string tableStructure = "ResultsID BIGINT IDENTITY(1,1) PRIMARY KEY, " +
                                   "FKTeamID BIGINT NOT NULL, " +
                                   "FKTeam_Opposing BIGINT NOT NULL, " +
                                   "FKEventsID BIGINT NOT NULL, " +
                                   "FKGamesPlayed BIGINT NOT NULL, " +
                                   "Result VARCHAR(10) NOT NULL" +
                                   "FOREIGN KEY (FKTeamID) REFERENCES Teams(TeamID), " +
                                   "FOREIGN KEY (FKTeam_Opposing) REFERENCES Teams(TeamID), " +
                                   "FOREIGN KEY (FKEventsID) REFERENCES Events(EventID), " +
                                   "FOREIGN KEY (FKGamesPlayedID) REFERENCES GamesPlayed(GamesPlayedID) ";
            //Pases name and strucutre to creation method.
            CreateTable(tableName, tableStructure);
        }

        /// <summary>
        /// Creates a list of test data to be added to the table and then sends each item to the database.
        /// </summary>
        private void SeedTeamTable()
        {
            //populate list of data model objects with pre-filled data
            List<Teams> teams = new List<Teams>
            {
                new Teams
                {
                    TeamName = "TroysGate",
                    PrimaryContact = "Email",
                    ContactPhone = "0451218564",
                    ContactEmail = "troy.vaughn@tafeqld.edu.au",
                    CompetitionPoints = 12
                },
                new Teams
                {
                    TeamName = "SallysGamers",
                    PrimaryContact = "Email",
                    ContactPhone = "04512185444",
                    ContactEmail = "Sally@tafeqld.edu.au",
                    CompetitionPoints = 16
                },
                new Teams
                {
                   TeamName = "garysGamers",
                    PrimaryContact = "Email",
                    ContactPhone = "04512185777",
                    ContactEmail = "Garry@tafeqld.edu.au",
                    CompetitionPoints = 10
                }
            };
            //Add each to the database using the relevant DataAccess "Add" method.
            foreach (var item in teams)
            {
                AddTeams(item);
            }
        }

        /// <summary>
        /// Creates a list of test data to be added to the table and then sends each item to the database.
        /// </summary>
        private void SeedEventsTable()
        {
            //populate list of data model objects with pre-filled data
            List<Events> events = new List<Events>
            {
                new Events
                {
                    EventName = "BrisbaneGames",
                    EventDate = "12/02/22",
                    EventLocation = "Brisbane",
                },
                new Events
                {
                    EventName = "BrisbaneGames",
                    EventDate = "12/02/22",
                    EventLocation = "Brisbane",
                },
                new Events
                {
                    EventName = "BrisbaneGames",
                    EventDate = "12/02/22",
                    EventLocation = "Brisbane",
                }
            };
            //Add each to the database using the relevant DataAccess "Add" method.
            foreach (var item in events)
            {
                AddEvents(item);
            }
        }

        private void SeedGamesPlayedTable()
        {
            //populate list of data model objects with pre-filled data
            List<GamesPlayed> games_played = new List<GamesPlayed>
            {
                new GamesPlayed
                {
                    GameName = "Halo",
                    GameType = "Solo",
                },
                new GamesPlayed
                {
                    GameName = "Call Of Duty",
                    GameType = "Team",
                },
                new GamesPlayed
                {
                    GameName = "Dota",
                    GameType = "Team",
                }
            };
            //Add each to the database using the relevant DataAccess "Add" method.
            foreach (var item in games_played)
            {
                AddGamesPlayed(item);
            }
        }

        /// <summary>
        /// Creates a list of test data to be added to the table and then sends each item to the database.
        /// </summary>
        private void SeedResultsTable()
        {
            //populate list of data model objects with pre-filled data
            List<Results> builds = new List<Results>
            {
                new Results(1,1,2,1),
                new Results(2,2,2,5),
                new Results(3,1,3,6),
                new Results
                {
                    Result = "Win"
                }
            };
            //Add each to the database using the relevant DataAccess "Add" method.
            foreach (var item in builds)
            {
                WinTransaction(item);
            }
        }
    }
}
