using System;
using System.Collections.Generic;

namespace WeatherAlert_DB.AlertGenerator
{
    /// <summary>
    /// Handles building alerts and automatically sending them to DB.
    /// </summary>
    public class AlertBuilder
    {
        // Retrieve datasets to start building alerts
        static List<string[]> dataSets = new AlertApiDataHandler().GetAllPropDataSetArrays();
        AlertDBInserter AlertDBInserter = new AlertDBInserter();

        public AlertBuilder()
        {

        }

        /// <summary>
        /// Builds alerts and automatically sends them to be inserted into the DB.
        /// </summary>
        public void BuildAllAlerts()
        {
            foreach (var dataSet in dataSets)
            {
                var myvar = GenerateListPerAlert(dataSet);

                Alert alert = CreateNewAlert(myvar);

                AlertDBInserter.AddAlertEntry(alert);
            }
            
            // Finalize the Entries and send to DB.
            AlertDBInserter.FinalizeEntriesToDB();
        }

        private List<string> GetParsedData(string stringToParse, AlertProps alertProp)
        {
            // Setup parser to retrieve parsed data
            // add tmp list to retrieve data from
            var AlertDataParser = new AlertDataParser(stringToParse, alertProp);
            var tmpList = new List<string>(AlertDataParser.parsedData);

            return tmpList;
        }

        private List<string> GenerateListPerAlert(string[] stringArr)
        {
            var tmpList = new List<string>();
            AlertProps AlertProp = 0;
            // Loop through all available AlertProps
            while ((int)AlertProp < Enum.GetNames(typeof(AlertProps)).Length)
            {
                foreach (var line in stringArr)
                {
                    if (line.Contains(AlertEnums.ConvertEnumToSearchableString(AlertProp)) || line.Contains("NOT SPECIFIED"))
                    {
                        foreach (var parsedLine in GetParsedData(line, AlertProp))
                        {
                            tmpList.Add(parsedLine);
                        }
                        AlertProp++;
                    }
                    
                }
            }
            return tmpList;
        }
        private Alert CreateNewAlert(List<string> stringList)
        {
            Alert alert = new Alert(id: stringList[0], areaDescription: stringList[1],
                date: stringList[2], time: stringList[3], severity: stringList[4],
                eventType: stringList[5], state: stringList[6], city: stringList[7],
                description: stringList[8], descriptionKeywords: 
                AlertDataParser.CleanDescriptiveKeywords(stringList[9] + stringList[11]), 
                nwsHeadline: stringList[10]);

            return alert;
        }
    }
}
