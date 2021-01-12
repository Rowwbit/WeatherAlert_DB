﻿using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SQLite;

namespace WeatherAlert_DB
{
    static public class SQLite_Data_Access
    {
        // Is the user wanting to use the DummyDB
        public static bool IsUsingDummyDB = Properties.Settings.Default.UserUsingDummyDB; 

        // DB ConnectionStrings currently available to the application.
        public enum ConnectionString
        {
            MainDB,
            DummyDB,
        }

        private static string LoadConnectionString(ConnectionString connectionStringDB)
        {
            // Retrieve the connection string from config for DB.
            return ConfigurationManager.ConnectionStrings[connectionStringDB.ToString()].ConnectionString;
        }

        /// <summary>
        /// Queries ALL Alerts from a DB.
        /// </summary>
        /// <param name="conString">Allows a manual Connection String</param>
        /// <returns>List of Alerts</returns>\
        public static List<Alert> SelectAll_DB(string conString = null)
        {
            // Check if user is wanting to import DB or not.
            IDbConnection connection;
            if (conString == null)
            {
                connection = new SQLiteConnection(CheckWhichDBIsUsed());
            }
            else
            {
                connection = new SQLiteConnection("Data Source=" + conString);
            }

            // Start connection and return alerts from whichever DB is used.
            using (connection)
            {
                List<Alert> alerts = new List<Alert>();

                // Build sequel query
                SQLiteCommand CMD = new SQLiteCommand();
                CMD.Connection = (SQLiteConnection)connection;
                CMD.CommandType = CommandType.Text;
                CMD.CommandText = "SELECT * FROM Alerts";
                CMD.Connection.Open();
                SQLiteDataReader rdr = CMD.ExecuteReader();

                // Read from DB
                while (rdr.Read())
                {
                    var Alert = new Alert(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2),
                                           rdr.GetString(3), rdr.GetString(4), rdr.GetString(5),
                                           rdr.GetString(6), rdr.GetString(7), rdr.GetString(8),
                                           rdr.GetString(9), rdr.GetString(10));
                    alerts.Add(Alert);
                }
                rdr.Close();
                CMD.Connection.Close();
                return alerts;
            }
        }
       /// <summary>
       /// Inserts objects in the Database.
       /// </summary>
       /// <param name="alert"></param>
       /// <returns>Returns false if object already exists. If object was added it returns true.</returns>
        public static bool InsertIn_DB(Alert alert)
        {
            bool WasObjectAddedToDB;
            // Check if DB entry already exists
            if (!DoesObjectAlreadyExist(alert))
            {
                using (IDbConnection connection = new SQLiteConnection(LoadConnectionString(ConnectionString.MainDB)))
                {
                    SQLiteCommand CMD = new SQLiteCommand();
                    CMD.Connection = (SQLiteConnection)connection;
                    CMD.CommandType = CommandType.Text;
                    CMD.Parameters.AddWithValue("@Id", alert.Id);
                    CMD.Parameters.AddWithValue("@Date", alert.Date);
                    CMD.Parameters.AddWithValue("@Time", alert.Time);
                    CMD.Parameters.AddWithValue("@EventType", alert.EventType);
                    CMD.Parameters.AddWithValue("@State", alert.State);
                    CMD.Parameters.AddWithValue("@City", alert.City);
                    CMD.Parameters.AddWithValue("@Severity", alert.Severity);
                    CMD.Parameters.AddWithValue("@NWSHeadline", alert.NWSHeadline);
                    CMD.Parameters.AddWithValue("@Description", alert.Description);
                    CMD.Parameters.AddWithValue("@DescriptionKeywords", alert.DescriptionKeywords);
                    CMD.Parameters.AddWithValue("@AreaDescription", alert.AreaDescription);
                    CMD.CommandText = @"INSERT INTO Alerts (Id,Date,Time,EventType,State,City,Severity,NWSHeadline,Description,DescriptionKeywords,AreaDescription) 
                                    values (@Id,@Date,@Time,@EventType,@State,@City,@Severity,@NWSHeadline,@Description,@DescriptionKeywords,@AreaDescription)";
                    CMD.Connection.Open();
                    CMD.ExecuteNonQuery();
                    CMD.Connection.Close();
                    WasObjectAddedToDB = true;
                }
            }
            else
            {
                WasObjectAddedToDB = false;
            }
            return WasObjectAddedToDB;
        }
        /// <summary>
        /// Check if record already exists.
        /// </summary>
        /// <param name="alert"></param>
        /// <returns>True if Record exists, false if it doesn't</returns>
        private static bool DoesObjectAlreadyExist(Alert alert)
        {
            string IdOfObject = "";
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString(ConnectionString.MainDB)))
            {
                // Build sequel query
                SQLiteCommand CMD = new SQLiteCommand();
                CMD.Connection = (SQLiteConnection)connection;
                CMD.CommandType = CommandType.Text;
                CMD.CommandText = $"SELECT Id FROM Alerts WHERE Id LIKE '{alert.Id.Replace('-','_')}'";
                CMD.Connection.Open();
                SQLiteDataReader rdr = CMD.ExecuteReader();

                // Read from DB
                while (rdr.Read())
                {
                    IdOfObject = rdr.GetString(0);
                }
                rdr.Close();
                CMD.Connection.Close();
            }
            if (IdOfObject == alert.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Deletes ALL Alerts from the DB.
        /// </summary>
        public static void DeleteAllIn_DB()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString(ConnectionString.MainDB)))
            {
                SQLiteCommand CMD = new SQLiteCommand();
                CMD.Connection = (SQLiteConnection)connection;
                CMD.CommandType = CommandType.Text;
                CMD.CommandText = "DELETE FROM Alerts";
                CMD.Connection.Open();
                CMD.ExecuteNonQuery();
                CMD.Connection.Close();
            }
        }
        /// <summary>
        /// Import another Alert DB to the existing one.
        /// </summary>
        /// <param name="FilePath"></param>
        public static void ImportDBFile(string filePath)
        {
            List<Alert> alerts = SelectAll_DB(filePath);

            foreach (var alert in alerts)
            {
                InsertIn_DB(alert);
            }
        }
        private static string CheckWhichDBIsUsed(string _connectionstring = null)
        {
            // Checks if the user wants to use the DummyDB or not
            if (IsUsingDummyDB == false)
            {
                return LoadConnectionString(ConnectionString.MainDB);
            }
            else
            {
                return LoadConnectionString(ConnectionString.DummyDB);
            }
        }
    }
}
