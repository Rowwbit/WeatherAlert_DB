using SharedResourceLib.DirAndIOSystems;
using System.Collections.Generic;
using System.Data;

namespace WeatherAlert_DB.UISystems
{
    /// <summary>
    /// Reads the DB file and saves the info to a list.
    /// </summary>
    class Table_Builder
    {
        // Is the user wanting to use the DummyDB
        public static bool isUsingDummyDB = Properties.Settings.Default.UserUsingDummyDB;


        private void ReadTablesFromDB()
        {
            SQLite_Data_Access.GetAllRowsFromTable(ConnStringManager.GetMainDBConnectionString());
        }
    }
}
