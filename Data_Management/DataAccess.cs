#region Imports
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Data_Management.Models;
#endregion

namespace Data_Management
{
    public class DataAccess
    {
        #region GamesPlayed

        /// <summary>
        /// Retrieves all product records from the database
        /// </summary>
        /// <returns>A list of product records</returns>
        public List<GamesPlayedView> GetGamesPlayed()
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = "SELECT GamesPlayed.GamesPlayedID, GamesPlayed.GameName, Teams.TeamName, " +
                                    "GamesPlayed.GameType " +
                                    "FROM Teams INNER JOIN " +
                                    "GamesPlayed ON Teams.TeamID = GamesPlayed.FKTeamID";
                    //Method to requests the desired data from the database and return the results to the user.
                    return connection.Query<GamesPlayedView>(query).ToList();
                }
            }
            catch (Exception e)
            {
                //Returns an empty list if the query fails.
                return new List<GamesPlayedView>();
            }
        }

        /// <summary>
        /// Retrieves a specified product record from the database based upon a provided Id
        /// </summary>
        /// <param name="id">The primary key of the record to be the retrieved.</param>
        /// <returns>A product records model matching the provided Id</returns>
        public GamesPlayed GetGamesPlayedById(int id)
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = $"SELECT * FROM GamesPlayed WHERE GamesPlayed.GamesPlayedID = {id}";
                    //Method to requests the desired data from the database and return the results to the user.
                    return connection.QuerySingle<GamesPlayed>(query);
                }
            }
            catch (Exception e)
            {
                //Returns an empty record if the query fails.
                return new GamesPlayed();
            }
        }

        /// <summary>
        /// Adds a new product record to the database based upon the provided data model.
        /// </summary>
        /// <param name="product">The product model holding the new data to be saved to the database.</param>
        public void AddGamesPlayed(GamesPlayed games_played)
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = "INSERT INTO GamesPlayed (GameName, FKTeamID, GameType) " +
                                   "VALUES (@GameName, @FKTeamID, @GameType)";
                    //Method to requests the provided data model top be saved to the database.
                    connection.Execute(query, games_played);
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Updates a  product record within database based upon the provided data model.
        /// </summary>
        /// <param name="product">The product model holding the updated data to be saved to the database.</param>
        public void UpdateGamesPlayed(GamesPlayed games_played)
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = "UPDATE GamesPlayed " +
                                   "SET GameName = @GameName, FKTeamID = @FKTeamID, " +
                                   "GameType = @GameType " +
                                   "WHERE GamesPlayedID = @GamesPlayedID";
                    //Method to requests a record to be updated based upon the provided data model.
                    connection.Execute(query, games_played);
                }
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Deletes a record from the database based upon the provided id(Primary Key)
        /// </summary>
        /// <param name="id">The primary key of the record to be deleted</param>
        public void DeleteGamesPlayed(int id)
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = $"DELETE FROM GamesPlayed WHERE GamesPlayedID = {id}";
                    //Method to requests the desired record to be removed from the database.
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Retrieves all category records from the database
        /// </summary>
        /// <returns>A list of category records</returns>
        public List<EventView> GetEvents()
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = "SELECT Events.EventID, Events.EventName, Teams.TeamName, " +
                                    "Events.EventDate, Events.EventLocation " +
                                    "FROM Teams INNER JOIN " +
                                    "Events ON Teams.TeamID = Events.FKTeamID";
                    //Method to requests the desired data from the database and return the results to the user.
                    return connection.Query<EventView>(query).ToList();
                }
            }
            catch (Exception e)
            {
                //Returns an empty list if the quey fails.
                return new List<EventView>();
            }
        }

        /// <summary>
        /// Adds a new product record to the database based upon the provided data model.
        /// </summary>
        /// <param name="category">The data model holding the details to be stored in the database</param>
        public void AddEvents(Events events)
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = "INSERT INTO Events (EventName, Eventlocation, EventDate, FKTeamID) " +
                               "VALUES (@EventName, @EventLocation, @EventDate, @FKTeamID)";
                    //Method to requests the desired record to be removed from the database.
                    connection.Execute(query, events);
                }
            }
            catch (Exception e)
            {

            }
        }

        public Events GetEventsByID(int id)
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = $"SELECT * FROM Events WHERE Events.EventID = {id}";
                    //Method to requests the desired data from the database and return the results to the user.
                    return connection.QuerySingle<Events>(query);
                }
            }
            catch (Exception e)
            {
                //Returns an empty record if the query fails.
                return new Events();
            }
        }

        public void UpdateEvents(Events events)
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = "UPDATE Events " +
                                   "SET EventName = @EventName, FKTeamID = @FKTeamID, " +
                                   "EventLocation = @EventLocation, EventDate = @EventDate " +
                                   "WHERE EventID = @EventID";
                    //Method to requests a record to be updated based upon the provided data model.
                    connection.Execute(query, events);
                }
            }
            catch (Exception e)
            {

            }
        }

        public void DeleteEvents(int id)
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = $"DELETE FROM Events WHERE EventID = {id}";
                    //Method to requests the desired record to be removed from the database.
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }


        #endregion

        #region Teams

        /// <summary>
        /// Retrieves all customer records from the database
        /// </summary>
        /// <returns>A list of customer records</returns>
        public List<Teams> GetTeams()
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = "SELECT * FROM Teams";
                    //Method to requests the desired data from the database and return the results to the user.
                    return connection.Query<Teams>(query).ToList();
                }
            }
            catch (Exception e)
            {
                //Returns an empty record if the quey fails.
                return new List<Teams>();
            }
        }

        /// <summary>
        /// Retrieves a customer record from the database based upon a given id(Primary Key).
        /// </summary>
        /// <param name="id">The Id number of the record to be retrieved</param>
        /// <returns>A customer model representing a single DB record.</returns>
        public Teams GetTeamsById(int id)
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = $"SELECT * FROM Teams WHERE TeamID = {id}";
                    //Method to requests the desired data from the database and return the results to the user.
                    return connection.QuerySingle<Teams>(query);
                }
            }
            catch (Exception e)
            {
                //Returns an empty entry if the quey fails.
                return new Teams();
            }
        }

        

        /// <summary>
        /// Adds a new product record to the database based upon the provided data model.
        /// </summary>
        /// <param name="customer">The data model holding the details to be stored in the database</param>
        public void AddTeams(Teams teams)
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = "INSERT INTO Teams (TeamName, PrimaryContact, ContactPhone, ContactEmail, CompetitionPoints) " +
                                   "VALUES (@TeamName, @PrimaryContact, @ContactPhone, @ContactEmail, @CompetitionPoints)";
                    //Method to requests the provided data model top be saved to the database.
                    connection.Execute(query, teams);
                }
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Updates a product record in the database based upon the provided data model.
        /// </summary>
        /// <param name="customer">The data model holding the details of the updated record to be stored in the database</param>
        public void UpdateTeams(Teams customer)
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = "UPDATE Teams " +
                                   "SET TeamName = @TeamName, PrimaryContact = @PrimaryContact, " +
                                   "ContactPhone = @ContactPhone, ContactEmail = @ContactEmail, CompetitionPoints = @CompetitionPoints " +
                                   "WHERE TeamID = @TeamID";
                    //Method to requests a record to be updated based upon the provided data model.
                    connection.Execute(query, customer);
                }
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Deletes a record from the database based upon the provided id(Primary Key)
        /// </summary>
        /// <param name="id">The primary key of the record to be deleted</param>
        public void DeleteTeams(int id)
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = $"DELETE FROM Teams WHERE TeamID = {id}";
                    //Method to requests the desired record to be removed from the database.
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }

        #endregion

        #region Results

        /// <summary>
        /// Method to retrive all build records from the database and return them in a list.
        /// </summary>
        /// <returns>A list of objects representing the databse records.</returns>
        public List<ResultView> GetAllResults()
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction. Due to the databse table having multiple
                    //Foreign Keys referencing the same table, this queries relies on using aliases as part of the joins to join each key to the table separately
                    //in order to accurately retrieve all 3 Product Name's. Additinoal aliases are used to tell the fileds apart and to map them to differently named
                    //fields in the model. 
                    string query = "SELECT Result.ResultsID, team.TeamName AS Team, opposing.TeamName AS Opposing, " +
                                   "Events.EventName, GamesPlayed.GameName, Result.Result " +
                                   "FROM Result " +
                                   "INNER JOIN " +
                                   "Teams team ON team.TeamID = Result.FKTeamID " +
                                   "INNER JOIN " +
                                   "Teams opposing ON opposing.TeamID = Result.FKTeamID_Opposing " +
                                   "INNER JOIN" +
                                   "Events ON Result.FKEventsID = Events.EventID " +
                                   "INNER JOIN " +
                                   "GamesPlayed ON Result.FKGamesPlayedID = GamesPlayed.GamesPlayedID ";

                    //Method to requests the desired record to be removed from the database.
                    return connection.Query<ResultView>(query).ToList();
                }
            }
            catch (Exception e)
            {
                //Returns blank list if query fails.
                return new List<ResultView>();
            }
        }

        /// <summary>
        /// Adds a new product record to the database based upon the provided data model.
        /// </summary>
        /// <param name="build">The data model holding the details to be stored in the database</param>
        public bool ResultsTransaction(Results results)
        {
            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                //Checks if the connection is currently open, if not, it opens the connection.<= Normally done for us by Dapper 
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = "INSERT INTO  (ResultsID, FKTeamID, FKEventsID, FKGamesPlayedID, FKTeamID_Opposing, Result) " +
                                       "VALUES (@ResultsID, @FKTeamID, @FKEventsID, @FKGamesPlayedID, @FKTeamID_Opposing, @Result)";

                        connection.Execute(query, results, transaction);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public void Addresults(Results results)
        {
            try
            {
                //using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //query string to be passed to the sql database to perform the desired database interaction.
                    string query = "insert into Result (ResultsID, FKTeamID, FKEventsID, FKGamesPlayedID, FKTeamID_Opposing, Result) " +
                                   "values (@ResultsID, @FKTeamID, @FKEventsID, @FKGamesPlayedID, @FKTeamID_Opposing, @Result)";
                    //method to requests the provided data model top be saved to the database.
                    connection.Execute(query, results);
                }
            }
            catch (Exception e)
            {

            }
        }

        public void DeleteResults(int id)
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = $"DELETE FROM Result WHERE ResultID = {id}";
                    //Method to requests the desired record to be removed from the database.
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }

        public void UpdateResults(Results product)
        {
            try
            {
                //Using statement structure which uses the provided resource  to perform the provided logic and then automatically
                //disposes of the resource once the structure finishes or an error occurs.
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Query string to be passed to the SQL database to perform the desired database interaction.
                    string query = "UPDATE Result " +
                                   "SET Result = @Result, FKTeamID = @FKTeamID, " +
                                   "FKEventID = @FKEventID, FKTeamID_Opposing = @FKTeamID_Opposing, FKGamesPlayedID = @FKGamesPlayedID " +
                                   "WHERE Result = @Result";
                    //Method to requests a record to be updated based upon the provided data model.
                    connection.Execute(query, product);
                }
            }
            catch (Exception e)
            {

            }
        }
    
        #endregion
    }
}
