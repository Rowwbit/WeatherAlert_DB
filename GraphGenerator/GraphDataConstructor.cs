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
            graphData = new GraphData(new GraphDataValidator(TupleList,graphFilter).TupleList);
        }

        public void RequestAndGenerateData()
        {
            // Request DB records
            var Alerts = SQLite_Data_Access.SelectAll_DB();

            // Aggregate the data values from the Expression and save them into the TupleList
            foreach (var item in Alerts.GroupBy(graphFilter.filterName.ToString()).Select(z => new { Key = z.Key, Count = z.Count() }))
            {
                TupleList.Add(new Tuple<string, int>(item.Key.ToString(), item.Count));
            }
        }
    }
}
