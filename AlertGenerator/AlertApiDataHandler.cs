using System;
using System.Collections.Generic;

namespace WeatherAlert_DB.AlertGenerator
{
    public class AlertApiDataHandler
    {
        // Entry Point for API request. Gets filtered data from json request
        List<string> alertApiDataList = NWS_ApiController.ReturnApiCall();

        // Save all objects here to send off later.
        List<string[]> PropDataSets = new List<string[]>();

        public AlertApiDataHandler()
        {
            LoopAPIListAndBuildStringArrys();
        }

        // Returns a bool if property matches the line.
        private bool IsPropOnThisLine(AlertProps availAlertProp, string stringToSearch)
        {
            if (stringToSearch.StartsWith(AlertEnums.ConvertEnumToSearchableString(availAlertProp)) && stringToSearch != null)
            {
                return true;
            }
            else return false;
        }
        // Returns a string based on whether a property exists or not.
        private string GetValueToAdd(AlertProps availAlertProp, string stringToAdd)
        {
            if (IsPropOnThisLine(availAlertProp, stringToAdd) && stringToAdd != null)
            {
                return stringToAdd;
            }
            else
            {
                return null;
            }
        }

        private string[] BuildArrayProps()
        {
            string[] tmpPropsArr = new string[AlertEnums.GetNumOfAvailAlertProps()];

            // Loop through available props that is searched for.
            foreach (int propNum in Enum.GetValues(typeof(AlertProps)))
            {
                // Check to make sure loop doesn't go OOB.
                if (!(propNum > alertApiDataList.Count - 1))
                {
                    tmpPropsArr[propNum] = GetValueToAdd((AlertProps)propNum, alertApiDataList[propNum]);
                }

            }
            ReplaceArrNullEntries(tmpPropsArr);
            return tmpPropsArr;
        }
        private int CountFoundDataInArray(string[] propArr)
        {
            // Checks an array to see what props were found.
            // This is used to remove the correct number of index's from the API list.
            int numOfFoundData = 0;
            foreach (var line in propArr)
            {
                if (line != "NOT SPECIFIED" && line != null)
                {
                    numOfFoundData++;
                }
            }
            return numOfFoundData;
        }
        private void LoopAPIListAndBuildStringArrys()
        {
            // The main method to build all the data sets.
            while (alertApiDataList.Count != 0)
            {
                string[] tmparry = BuildArrayProps();
                alertApiDataList.RemoveRange(0, CountFoundDataInArray(tmparry));
                PropDataSets.Add(tmparry);
            }
        }
        private void ReplaceArrNullEntries(string[] propArr)
        {
            // Ensure any null values are replaced
            for (int i = 0; i < propArr.Length; i++)
            {
                if (string.IsNullOrEmpty(propArr[i]))
                {
                    propArr[i] = "NOT SPECIFIED";
                }
            }
        }
        /// <summary>
        /// Retrieve the DataSets to start building alerts.
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetAllPropDataSetArrays()
        {
            return PropDataSets;
        }
    }
}
