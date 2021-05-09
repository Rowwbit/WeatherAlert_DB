using System.Collections.Generic;

namespace WeatherAlert_DB
{
    /// <summary>
    /// This class includes methods to convert the raw json info from the API to construct Alert Objects.
    /// </summary>
    public class Alert
    {
        public string Id { get; }
        public string Date { get; }
        public string Time { get; }
        public string EventType { get; }
        public string State { get; }
        public string City { get; }
        public string Severity { get; }
        public string NWSHeadline { get; }
        public string Description { get; }
        public string DescriptionKeywords { get; }
        public string AreaDescription { get; }
        public static Dictionary<string, string> StateDictionary { get; } = new Dictionary<string, string> 
        { { "AL", "ALABAMA" }, { "AK", "ALASKA" }, { "AZ", "ARIZONA" }, { "AR", "ARKANSAS" },
          { "CA", "CALIFORNIA" }, { "CO", "COLORADO" }, { "CT", "CONNECTICUT" }, { "DE", "DELAWARE" },
          { "FL", "FLORIDA" }, { "GA", "GEORGIA" }, { "HI", "HAWAII" }, { "ID", "IDAHO" },
          { "IL", "ILLINOIS" }, { "IN", "INDIANA" }, { "IA", "IOWA" }, { "KS", "KANSAS" },
          { "KY", "KENTUCKY" }, { "LA", "LOUISIANA" }, { "ME", "MAINE" }, { "MD", "MARYLAND" },
          { "MA", "MASSACHUSETTS" }, { "MI", "MICHIGAN" }, { "MN", "MINNESOTA" }, { "MS", "MISSISSIPPI" },
          { "MO", "MISSOURI" }, { "MT", "MONTANA" }, { "NE", "NEBRASKA" }, { "NV", "NEVADA" },
          { "NH", "NEW HAMPSHIRE" }, { "NJ", "NEW JERSEY" }, { "NM", "NEW MEXICO" }, { "NY", "NEW YORK" },
          { "NC", "NORTH CAROLINA" }, { "ND", "NORTH DAKOTA" }, { "OH", "OHIO" }, { "OK", "OKLAHOMA" },
          { "OR", "OREGON" }, { "PA", "PENNSYLVANIA" }, { "RI", "RHODE ISLAND" }, { "SC", "SOUTH CAROLINA" },
          { "SD", "SOUTH DAKOTA" }, { "TN", "TENNESSEE" }, { "TX", "TEXAS" }, { "UT", "UTAH" },
          { "VT", "VERMONT" }, { "VA", "VIRGINIA" }, { "WA", "WASHINGTON" }, { "WV", "WEST VIRGINIA" },
          { "WI", "WISCONSIN" }, { "WY", "WYOMING" }
        };
        public static string[] DescriptorWords { get; } =
        {     "FOG", "GALE", "SNOW", "RAIN", "ICE", "STORM",
              "EARTHQUAKE", "TORNADO", "FLOOD", "HURRICANE", "CYCLONE",
              "BLIZZARD", "HAIL", "WIND", "DUST", "FIRE", "WILDFIRE",
              "SLUSH", "ADVISORY", "SLEET", "FREEZING", "CLOUDY", 
              "WATER LEVEL", "WAVE", "SHOWER", "THUNDER", "LIGHTNING",
              "SURF", "THUNDERSTORM"
        };
        public Alert(string id, string date, string time, string eventType, string state, string city, string severity, string nwsHeadline, string description, string descriptionKeywords, string areaDescription)
        {
            Id = id;
            Date = date;
            Time = time;
            EventType = eventType;
            State = state;
            City = city;
            Severity = severity;
            NWSHeadline = nwsHeadline;
            Description = description;
            DescriptionKeywords = descriptionKeywords;
            AreaDescription = areaDescription;
        }
    }
}
