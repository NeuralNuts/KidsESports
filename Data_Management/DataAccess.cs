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
        public List<GamesPlayedView> GetGamesPlayed()
        {
            try
            {
                //Gets all data from GamesPlayed tabel//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = "SELECT * FROM GamesPlayed";
                   
                    return connection.Query<GamesPlayedView>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<GamesPlayedView>();
            }
        }

        public GamesPlayed GetGamesPlayedById(int id)
        {
            try
            {
                //Gets the selected data based on the primary key//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = $"SELECT * FROM GamesPlayed WHERE GamesPlayed.GamesPlayedID = {id}";
                  
                    return connection.QuerySingle<GamesPlayed>(query);
                }
            }
            catch (Exception e)
            {
                return new GamesPlayed();
            }
        }

        public void AddGamesPlayed(GamesPlayed games_played)
        {
            try
            {
              //Adds enetered data into the GamesPlayed tabel//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = "INSERT INTO GamesPlayed (GameName, GameType) " +
                                   "VALUES (@GameName, @GameType)";
                   
                    connection.Execute(query, games_played);
                }
            }
            catch (Exception)
            {

            }
        }

        public void UpdateGamesPlayed(GamesPlayed games_played)
        {
            try
            {
              //Updates selected data//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = "UPDATE GamesPlayed " +
                                   "SET GameName = @GameName, " +
                                   "GameType = @GameType " +
                                   "WHERE GamesPlayedID = @GamesPlayedID";
                 
                    connection.Execute(query, games_played);
                }
            }
            catch (Exception e)
            {

            }
        }

    
        public void DeleteGamesPlayed(int id)
        {
            try
            {
             //Deletes all selcted data//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = $"DELETE FROM GamesPlayed WHERE GamesPlayedID = {id} ";
                  
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }

        #endregion

        #region Events
        public List<EventView> GetEvents()
        {
            try
            {
                //Gets all data from the Events tabel//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = "SELECT * FROM Events";
                    
                    return connection.Query<EventView>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<EventView>();
            }
        }

        public void AddEvents(Events events)
        {
            try
            {
                //Adds entered data to the Events tabel//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = "INSERT INTO Events (EventName, Eventlocation, EventDate) " +
                               "VALUES (@EventName, @EventLocation, @EventDate)";

                   
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
                //Gets selected data from the Events tabel by the primary key//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {  
                    string query = $"SELECT * FROM Events WHERE Events.EventID = {id}";
                   
                    return connection.QuerySingle<Events>(query);
                }
            }
            catch (Exception e)
            {
                return new Events();
            }
        }

        public void UpdateEvents(Events events)
        {
            try
            {
                //Updates the selected data //
                using (var connection = Helper.CreateSQLConnection("Default"))
                { 
                    string query = "UPDATE Events " +
                                   "SET EventName = @EventName, " +
                                   "EventLocation = @EventLocation, EventDate = @EventDate " +
                                   "WHERE EventID = @EventID ";
                 
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
                //Deletes all selected data//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                  
                    string query = $"DELETE FROM Events WHERE EventID = {id}";
                   
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }


        #endregion

        #region Teams
        public List<Teams> GetTeams()
        {
            try
            {
                //Gets all data from the Teams tabel//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = "SELECT * FROM Teams";
               
                    return connection.Query<Teams>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Teams>();
            }
        }

        public Teams GetTeamsById(int id)
        {
            try
            {
                //Gets team data by the primary key//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = $"SELECT * FROM Teams WHERE TeamID = {id}";
                  
                    return connection.QuerySingle<Teams>(query);
                }
            }
            catch (Exception e)
            {
                return new Teams();
            }
        }

        public void AddTeams(Teams teams)
        {
            try
            {
                //Adds the entered data into the Teams tabel//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = "INSERT INTO Teams (TeamName, PrimaryContact, ContactPhone, ContactEmail, CompetitionPoints) " +
                                   "VALUES (@TeamName, @PrimaryContact, @ContactPhone, @ContactEmail, @CompetitionPoints)";
                  
                    connection.Execute(query, teams);
                }
            }
            catch (Exception e)
            {

            }
        }

        public void UpdateTeams(Teams customer)
        {
            try
            {
                //Updates the changed data into the Teams tabel//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = "UPDATE Teams " +
                                   "SET TeamName = @TeamName, PrimaryContact = @PrimaryContact, " +
                                   "ContactPhone = @ContactPhone, ContactEmail = @ContactEmail, CompetitionPoints = @CompetitionPoints " +
                                   "WHERE TeamID = @TeamID";
                  
                    connection.Execute(query, customer);
                }
            }
            catch (Exception e)
            {

            }
        }

        public void DeleteTeams(int id)
        {
            try
            {
                //Delets all team data that is selected//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = $"DELETE FROM Teams WHERE TeamID = {id}";
                    
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }

        #endregion

        #region Results
        public List<ResultView> GetAllResults()
        {
            try
            {
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    //Gets all data from the other tabels and displays in the data grid//
                    string query = "SELECT Result.ResultsID, TeamJoin.TeamName AS Team, OpposingJoin.TeamName AS Opposing, " +
                                   "Events.EventName, GamesPlayed.GameName, Result.Result " +
                                   "FROM Result " +
                                   "INNER JOIN " +
                                   "Teams AS TeamJoin ON TeamJoin.TeamID = Result.FKTeamID " +
                                   "INNER JOIN " +
                                   "Teams AS OpposingJoin ON OpposingJoin.TeamID = Result.FKTeamID_Opposing " +
                                   "INNER JOIN " +
                                   "Events ON Result.FKEventsID = Events.EventID " +
                                   "INNER JOIN " +
                                   "GamesPlayed ON Result.FKGamesPlayedID = GamesPlayed.GamesPlayedID ";

                    return connection.Query<ResultView>(query).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<ResultView>();
            }
        }

        public Results GetResultsById(int id)
        {
            try
            {
                //Gets results by the primary key//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = $"SELECT * FROM Result WHERE ResultsID = {id}";
                  
                    return connection.QuerySingle<Results>(query);
                }
            }
            catch (Exception e)
            {
                return new Results();
            }
        }

        public void UpdateResults(Results results)
        {
            try
            {
                //Updates the selected results//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = "UPDATE [Result] " +
                                   "SET Result = @Result, FKEventsID = @FKEventsID, " +
                                   "FKTeamID = @FKTeamID, FKGamesPlayedID = @FKGamesPlayedID, FKTeamID_Opposing = @FKTeamID_Opposing " +
                                   "WHERE ResultsID = @ResultsID ";
                  
                    connection.Execute(query, results);
                }
            }
            catch (Exception e)
            {

            }
        }

        public bool WinTransaction(Results results)
        {
            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        //Updates the teams points by 2 if WIN is entered//
                        string query = "INSERT INTO Result (FKTeamID, FKEventsID, FKGamesPlayedID, FKTeamID_Opposing, Result) " +
                                       "VALUES (@FKTeamID, @FKEventsID, @FKGamesPlayedID, @FKTeamID_Opposing, @Result) "; 
                                       
                        connection.Execute(query, results, transaction);

                        int id = 0;
                        if (results.Result.ToUpper().Equals("WIN"))
                        {
                            id = results.FKTeamID;
                        }
                        else
                        {
                            id = results.FKTeamID;
                        }

                        query = "UPDATE Teams " +
                               "SET CompetitionPoints = CompetitionPoints + 2 " +
                               $"WHERE TeamID = {id} ";

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

        public bool LossTransaction(Results results)
        {
            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        //Updates the team that lost points by -2 if LOSS is entered//
                        string query = "INSERT INTO Result (FKTeamID, FKEventsID, FKGamesPlayedID, FKTeamID_Opposing, Result) " +
                                       "VALUES (@FKTeamID, @FKEventsID, @FKGamesPlayedID, @FKTeamID_Opposing, @Result) ";


                        connection.Execute(query, results, transaction);

                        int id = 0;
                        if (results.Result.ToUpper().Equals("LOSS"))
                        {
                            id = results.FKTeamID;
                        }
                        else
                        {
                            id = results.FKTeamID_Opposing;
                        }

                        query = "UPDATE Teams " +
                               "SET CompetitionPoints = CompetitionPoints - 2 " +
                               $"WHERE TeamID = {id} ";

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

        public bool DrawTransaction(Results results)
        {
            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        //Updates both teams points by 1 if DRAW is entered//
                        string query = "INSERT INTO Result (FKTeamID, FKEventsID, FKGamesPlayedID, FKTeamID_Opposing, Result) " +
                                       "VALUES (@FKTeamID, @FKEventsID, @FKGamesPlayedID, @FKTeamID_Opposing, @Result) ";


                        connection.Execute(query, results, transaction);

                        

                        query = "UPDATE Teams " +
                               "SET CompetitionPoints = CompetitionPoints + 1 " +
                               $"WHERE TeamID = {results.FKTeamID} ";

                        connection.Execute(query, results, transaction);

                        query = "UPDATE Teams " +
                              "SET CompetitionPoints = CompetitionPoints + 1 " +
                              $"WHERE TeamID = {results.FKTeamID_Opposing} ";

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

        public void DeleteResults(int id)
        {
            try
            {
                //Deletes all data selected//
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = $"DELETE FROM Result WHERE ResultID = {id}";
                    connection.Execute(query);
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion
    }
}
