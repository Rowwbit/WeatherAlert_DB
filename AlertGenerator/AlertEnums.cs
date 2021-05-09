using System;

namespace WeatherAlert_DB.AlertGenerator
{
    public enum AlertProps
    {
        id,
        areaDesc,
        sent,
        severity,
        eventType,
        senderName,
        description,
        NWSheadline
    }
    public class AlertEnums
    {
        /// <summary>
        /// Takes an enumb and converts this to the correct string.
        /// </summary>
        public static string ConvertEnumToSearchableString(AlertProps availAlertProps)
        {
            switch (availAlertProps)
            {
                case AlertProps.id:
                    return "@id:";
                case AlertProps.areaDesc:
                    return "areaDesc:";
                case AlertProps.sent:
                    return "sent:";
                case AlertProps.severity:
                    return "severity:";
                case AlertProps.eventType:
                    return "event:";
                case AlertProps.senderName:
                    return "senderName:";
                case AlertProps.description:
                    return "description:";
                case AlertProps.NWSheadline:
                    return "NWSheadline:";
                default:
                    throw new System.Exception("Invalid Case. AlertEnums");
            }
        }
        /// <summary>
        /// Gets the total number of available enums to search for.
        /// </summary>
        public static int GetNumOfAvailAlertProps()
        {
            int numOfProps = 0;
            foreach (int prop in Enum.GetValues(typeof(AlertProps)))
            {
                numOfProps++;
            }
            return numOfProps;
        }
    }
}
