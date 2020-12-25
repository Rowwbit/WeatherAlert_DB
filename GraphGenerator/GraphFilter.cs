namespace WeatherAlert_DB
{
    /// <summary>
    /// This class creates a filter name for the FilterBy section in the graph view.
    /// </summary>
    public class GraphFilter
    {
        public enum FilterName
        {
            EventType,
            State,
            Severity,
            DescriptionKeywords
        }

        public string ComboBoxFilterName { get; set; }
        public FilterName filterName;

        public GraphFilter(FilterName _filterName)
        {
            ComboBoxFilterName = SetGraphFilterName(_filterName);
            filterName = _filterName;
        }
        
        private static string SetGraphFilterName(FilterName _filterName)
        {
            // Set the correct name for the GraphFilterName Property
            string name = "";
            switch (_filterName)
            {
                case FilterName.EventType:
                    name = "Event Type";
                    break;

                case FilterName.State:
                    name = "States";
                    break;

                case FilterName.Severity:
                    name = "Severity";
                    break;

                case FilterName.DescriptionKeywords:
                    name = "Description Keywords";
                    break;
            }
            return name;
        }
    }
}