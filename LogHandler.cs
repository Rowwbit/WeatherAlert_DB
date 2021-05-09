using System;
using System.Collections.Generic;
using System.IO;

namespace WeatherAlert_DB
{
    public enum LogType
    {
        // >Log Type Explanations<
        // Info: general information
        // Warning: exceptions/errors caught and handled
        // Critical: unexpected errors or failures

        INFO,
        WARNING,
        CRITICAL
    }

    /// <summary>
    /// Provides Logging utility to a text file.
    /// </summary>
    class LogHandler
    {
        string currentFilePath;
        LogType logType;
        List<string> Entries = new List<string>(); 
        DateTime DateTimeNow;
        Exception LoggedException;
        
        public LogHandler(LogType _logType, Exception e = null)
        {
            logType = _logType;
            DateTimeNow = DateTime.Now;
            LoggedException = e;
            currentFilePath = Environment.CurrentDirectory + "\\Weather_DB_LOG.txt";
        }
        /// <summary>
        /// Checks current working directory to make sure Log File exists. If it doesnt then it creates one.
        /// </summary>
        private void CheckForLogFile()
        {
            if (!File.Exists(currentFilePath))
            {
                File.WriteAllText(currentFilePath,
                    $"---------------------------------------------------------\n" +
                    $"| Log File Successfully Generated @ {DateTimeNow.ToShortDateString()} " +
                    $"{DateTimeNow.ToShortTimeString()} |\n" +
                    $"---------------------------------------------------------\n");
            }
        }
        /// <summary>
        /// Add string entries to the log.
        /// </summary>
        public void AddLogEntry(string entryToAdd)
        {
            Entries.Add(entryToAdd);
        }
        private string ConvertAllEntriesToString()
        {
            // Check if there are any entries if so convert them to a single string
            if (Entries != null)
            {
                string tmpString = "Log content: ";
                foreach (var item in Entries)
                {
                    tmpString += item + " ";
                }
                return tmpString;
            }
            else return "";
        }
        /// <summary>
        /// Writes this log entry to the Log File.
        /// </summary>
        public void WriteLogFile(string logReason)
        {
            CheckForLogFile();
            if (LoggedException != null)
            {
                // Dump trace.
                this.AddLogEntry(LoggedException.StackTrace);
                this.AddLogEntry( "Inner Exception " + LoggedException.InnerException.Message);

                File.AppendAllText(currentFilePath,
                     String.Format("\n[Date: {0}]-[Time: {1}]\n" +
                                   "Log Type: {2}\n" +
                                   "EXCEPTION: {3}\n" + "{4}\n",
                                   DateTimeNow.ToShortDateString(), 
                                   DateTimeNow.ToShortTimeString(), logType, 
                                   LoggedException.Message, ConvertAllEntriesToString()));
            }
            else
            {
                File.AppendAllText(currentFilePath,
                     String.Format("\n[Date: {0}]-[Time: {1}]\n" +
                                   "Log Type: {2}\n" +
                                   logReason + ":\n {3}",
                                   DateTimeNow.ToShortDateString(), 
                                   DateTimeNow.ToShortTimeString(),
                                   logType, ConvertAllEntriesToString()));
            } 
        }
    }
}
