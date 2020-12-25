using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherAlert_DB
{
    public class GraphDataConstructor
    {
        public GraphData graphData { get; }
        public GraphFilter graphFilter { get; }
        List<Tuple<string, int>> TupleList = new List<Tuple<string, int>>();

        public GraphDataConstructor(GraphFilter _graphFilter)
        {
            graphFilter = _graphFilter;
            RequestAndGenerateData();
            graphData = new GraphData(TupleList);
        }

        public void RequestAndGenerateData()
        {
            // Request DB records
            var Alerts = SQLite_Data_Access.SelectAll_DB();

            switch (graphFilter.filterName)
            {
                // Aggregate the data values from the Expression and save them into the TupleList
                case GraphFilter.FilterName.EventType:
                    foreach (var item in Alerts.GroupBy(graphFilter.filterName.ToString()).Select(z => new { EventType = z.Key, Count = z.Count() }))
                    {
                        TupleList.Add(new Tuple<string, int>(item.EventType.ToString(), item.Count));
                    }
                    break;

                case GraphFilter.FilterName.State:
                    foreach (var item in Alerts.GroupBy(graphFilter.filterName.ToString()).Select(z => new { State = z.Key, Count = z.Count() }))
                    {
                        TupleList.Add(new Tuple<string, int>(item.State.ToString(), item.Count));
                    }
                    break;

                case GraphFilter.FilterName.Severity:
                    foreach (var item in Alerts.GroupBy(graphFilter.filterName.ToString()).Select(z => new { Severity = z.Key, Count = z.Count() }))
                    {
                        TupleList.Add(new Tuple<string, int>(item.Severity.ToString(), item.Count));
                    }
                    break;

                case GraphFilter.FilterName.DescriptionKeywords:
                    foreach (var item in Alerts.GroupBy(graphFilter.filterName.ToString()).Select(z => new { DescriptionKeywords = z.Key, Count = z.Count() }))
                    {
                        TupleList.Add(new Tuple<string, int>(item.DescriptionKeywords.ToString(), item.Count));
                    }
                    break;
            }
        }
    }
}
