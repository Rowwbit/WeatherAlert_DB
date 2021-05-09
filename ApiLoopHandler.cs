using System;
using System.Timers;

namespace WeatherAlert_DB
{
    /// <summary>
    /// This class handles the GET Request Logic on a timer event.
    /// </summary>
    static class ApiLoopHandler
    { 
        // Declare a single repeating timer to request the NWS Api after the elapsed time
        private static Timer ApiTimer = new Timer(900000);
        public static TimeSpan ApiTimeSpan = new TimeSpan(0, 0, 0, 0, (int)ApiTimer.Interval);

        private static async void CallApiEvent(object source, ElapsedEventArgs e)
        {
            // Check if user is using DummyDb instead. 
            // If so prevent API Calls here.
            if (!SQLite_Data_Access.IsUsingDummyDB)
            {
                while (SyncingInfoToDB())
                {
                    ApiTimeSpan = new TimeSpan(0,0,0,0,-1);
                }
                // Reset API Timer
                ApiTimer.Interval = 900000;
                ApiTimeSpan = new TimeSpan(0, 0, 0, 0, (int)ApiTimer.Interval);
            }
            else
            {
                LogHandler Log = new LogHandler(LogType.INFO);
                Log.WriteLogFile("Skipped API request: User is using DummyDB.");
            }
        }
        /// <summary>
        /// Start an auto resetting Timer to request the API. 
        /// This is meant to continuously request the API while the app is running.
        /// </summary>
        public static void StartApiTimerLoop()
        {
            if (!ApiTimer.Enabled)
            {
                // Request API every 15 minutes.
                ApiTimer.AutoReset = true;
                ApiTimer.Elapsed += new ElapsedEventHandler(CallApiEvent);
                ApiTimer.Start();
            }
        }
        /// <summary>
        /// Sets the ApiTimer to request the API in 30 seconds.
        /// </summary>
        public static void TriggerTimerIn30sec()
        {
            ApiTimer.Interval = 30000;
            ApiTimeSpan = new TimeSpan(0, 0, 0, 0, (int)ApiTimer.Interval);
        }
        private static bool SyncingInfoToDB()
        {
            var alertBuilder = new AlertGenerator.AlertBuilder();
            alertBuilder.BuildAllAlerts();
            return false;
        }
    }
}
