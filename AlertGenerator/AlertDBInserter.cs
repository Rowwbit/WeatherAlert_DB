using System;
using System.Collections.Generic;

namespace WeatherAlert_DB.AlertGenerator
{
    /// <summary>
    /// Holds the Alerts that have been created and then insert them all into the DB.
    /// </summary>
    class AlertDBInserter
    {
        List<Alert> alerts = new List<Alert>();

        /// <summary>
        /// Add an alert to a list. These will be added to the DB sequentially.
        /// </summary>
        public void AddAlertEntry(Alert alert)
        {
            alerts.Add(alert);
        }
        /// <summary>
        /// Add an alert to a list. These will be added to the DB sequentially.
        /// </summary>
        public void FinalizeEntriesToDB()
        {
            try
            {
                LogHandler LogHandler = new LogHandler(LogType.INFO);
                foreach (var entry in alerts)
                {
                    SQLite_Data_Access.InsertIn_DB(entry);
                }
                LogHandler.WriteLogFile("Records Synced.");
            }
            catch (Exception e)
            {
                LogHandler LogHandler = new LogHandler(LogType.WARNING, e);
                LogHandler.WriteLogFile("Failed in DB insertion. Occured @ AlertDBInserter.");
            }
        }
    }
}
