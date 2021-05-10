using System;
using System.Collections.Generic;

namespace WeatherAlert_DB.AlertGenerator
{
    /// <summary>
    /// Takes raw data and filters/sanitizes the content.
    /// </summary>
    public class AlertDataParser
    {
        public List<string> parsedData { get; } = new List<string>();
        AlertProps alertProp;
        public AlertDataParser(string stringToParse, AlertProps _alertProp)
        {
            alertProp = _alertProp;
            ParseData(stringToParse);
        }

        private void ParseData(string stringToParse)
        {
            switch (alertProp)
            {
                case AlertProps.id:
                    parsedData.Add(ParseID(stringToParse));
                    break;
                case AlertProps.areaDesc:
                    parsedData.Add(ParseAreaDescription(stringToParse));
                    break;
                case AlertProps.sent:
                    parsedData.Add(ParseDate(stringToParse));
                    parsedData.Add(ParseTime(stringToParse));
                    break;
                case AlertProps.severity:
                    parsedData.Add(ParseSeverity(stringToParse));
                    break;
                case AlertProps.eventType:
                    parsedData.Add(ParseEvent(stringToParse));
                    break;
                case AlertProps.senderName:
                    parsedData.Add(ParseState(stringToParse));
                    parsedData.Add(ParseCity(stringToParse));
                    break;
                case AlertProps.description:
                    parsedData.Add(ParseDescription(stringToParse));
                    parsedData.Add(ParseDescrKeywords(stringToParse));
                    break;
                case AlertProps.NWSheadline:
                    parsedData.Add(ParseNWSHeadline(stringToParse));
                    parsedData.Add(ParseDescrKeywords(stringToParse));
                    break;
            }
        }

        /// <summary>
        /// Converts the raw ID from the API into the correct format for the DB. Json tag: "@id"
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Truncated ID as a string.</returns>
        private string ParseID(string id)
        {
            return id.Replace("@id: https://api.weather.gov/alerts/", "").Replace(",","");
        }
        /// <summary>
        /// Converts the raw Date from the API into the correct format for the DB. Json tag: "sent"
        /// </summary>
        /// <param name="sent"></param>
        /// <returns>Truncated Date as a string.</returns>
        private string ParseDate(string sent)
        {
            sent = sent.Remove(0, 6);
            int numOfCharsToRemove = sent.LastIndexOf('T');
            return sent.Remove(numOfCharsToRemove).Trim(',');
        }
        /// <summary>
        /// Converts the raw Time from the API into the correct format for the DB. Json tag: "sent"
        /// </summary>
        /// <param name="sent"></param>
        /// <returns>Truncated Time as a string.</returns>
        private string ParseTime(string sent)
        {
            int numOfCharsToRemove = sent.LastIndexOf('T');
            return sent.Remove(0, numOfCharsToRemove + 1).Trim(',');
        }
        /// <summary>
        /// Converts the raw State from the API into the correct format for the DB. Json tag: "senderName"
        /// </summary>
        /// <param name="senderName"></param>
        /// <returns>State abbreviation as a string.</returns>
        private string ParseState(string senderName)
        {
            string ParsedString = senderName.Remove(0, 17);
            int NumOfCharsToDelete = ParsedString.LastIndexOf(' ');
            ParsedString = ParsedString.Remove(0, NumOfCharsToDelete + 1);

            // ParsedString now contains either the full states name OR the states abbreviation. 
            // Now iterate through the Dictionary to match values and make sure to output the states abbreviation.
            ParsedString = ParsedString.ToUpper();
            foreach (var keyValuePair in Alert.StateDictionary)
            {
                // Check if State is an abreviation 
                if (ParsedString == keyValuePair.Key.ToUpper())
                {
                    break;
                }
                // Check if State is the full name
                else if (ParsedString == keyValuePair.Value.ToUpper())
                {
                    ParsedString = keyValuePair.Key.ToUpper();
                }
            }
            return ParsedString.Trim(',');
        }
        /// <summary>
        /// Converts the raw state into the correct format for the DB. Json tag: "senderName"
        /// </summary>
        /// <param name="senderName"></param>
        /// <returns>Truncated City as a string.</returns>
        private string ParseCity(string senderName)
        {
            senderName = senderName.Remove(0, 16).Trim(',');
            int NumOfCharsUntilState = senderName.LastIndexOf(' ');
            senderName = senderName.Substring(0, NumOfCharsUntilState);
            return senderName.ToUpper();
        }
        /// <summary>
        /// Converts the raw state into the correct format for the DB. Json tag: "description"
        /// </summary>
        /// <param name="description"></param>
        /// <returns>Truncated Description as a string.</returns>
        private string ParseDescription(string description)
        {
            return description.Remove(0, 13).Trim(',');
        }

        /// <summary>
        /// Iterates through a string to check for key words. Json tag: "NWSHeadline"
        /// </summary>
        /// <param name="description"></param>
        /// <returns>A string with descriptor words.</returns>
        private string ParseDescrKeywords(string description)
        {

            string[] SeperatedWords = description.ToString().ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string CombinedDescriptorWords = "";
            foreach (var word in SeperatedWords)
            {
                foreach (var descriptorWord in Alert.DescriptorWords)
                {
                    if (word.Contains(descriptorWord) && !CombinedDescriptorWords.Contains(descriptorWord))
                    {
                        CombinedDescriptorWords += descriptorWord + " ";
                    }
                }
            }
            // Check if the string was assigned any keywords. 
            // If not then specifically say its UNKNOWN to prevent a null database entry.
            if (string.IsNullOrEmpty(CombinedDescriptorWords))
            {
                CombinedDescriptorWords = "UNKNOWN ";
            }
            return CombinedDescriptorWords;
        }
        /// <summary>
        /// Converts the raw Headline into the correct format for the DB. Json tag: "NWSheadline"
        /// </summary>
        /// <param name="NWSheadline"></param>
        /// <returns>Truncated NWSHeadline as a string.</returns>
        private string ParseNWSHeadline(string NWSheadline)
        {
            if (NWSheadline != "NOT SPECIFIED")
            {
                return NWSheadline.Remove(0, 13).Trim(',');
            }
            else return "NOT SPECIFIED";
        }
        /// <summary>
        /// Converts the raw Severity into the correct format for the DB. Json tag: "severity"
        /// </summary>
        /// <param name="severity"></param>
        /// <returns>Truncated Severity as a string.</returns>
        private string ParseSeverity(string severity)
        {
            return severity.Remove(0, 10).Trim(',');
        }
        /// <summary>
        /// Converts the raw EventType into the correct format for the DB. Json tag: "event"
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        private string ParseEvent(string eventType)
        {
            return eventType.Remove(0, 7).Trim(',');
        }
        private string ParseAreaDescription(string areaDesc)
        {
            return areaDesc.Remove(0, 10).Trim(',');
        }
        /// <summary>
        /// Takes a combined string for the headline and descriptive keywords then cleans them.
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns>Filtered string of Keywords</returns>
        public static string CleanDescriptiveKeywords(string keywords)
        {
            // Check for duplicate keywords and removes them
            string[] KeywordsArray = keywords.Split(' ');
            string ReturnedString = "";
            for (int WordAsIndex = 0; WordAsIndex < KeywordsArray.Length; WordAsIndex++)
            {
                if (!ReturnedString.Contains(KeywordsArray[WordAsIndex]))
                {
                    ReturnedString += KeywordsArray[WordAsIndex] + " ";
                }
            }
            // Check if string contains more than just the word UNKNOWN.
            // This prevents outputting UNKNOWN if there are other keywords with it
            if (ReturnedString.Contains("UNKNOWN") && ReturnedString.Length > 8)
            {
                ReturnedString = ReturnedString.Replace("UNKNOWN", "");
            }
            return ReturnedString;
        }
    }
}
