using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherAlert_DB
{
    /// <summary>
    /// This class holds specific Data values and Label values required for making a graph.
    /// </summary>
    public class GraphData
    {
        public double[] DataValues { get; set; }
        public string[] TextLabels { get; set; }
        public double[] ConsecutiveDataCount { get; }

        public GraphData(List<Tuple<string, int>> dataList)
        {
            AssignDataValues(dataList);
            AssignTextValues(dataList);
            ConsecutiveDataCount = 
                InitializeArrayWithConsecutiveData(DataValues.Count());
        }

        private double[] InitializeArrayWithConsecutiveData(int numOfValues)
        {
            // This automatically adds the increments needed for the graph
            var consecutiveDataCount = new double[numOfValues];
            for (int i = 0; i < numOfValues; i++)
            {
                consecutiveDataCount[i] = i;
            }
            return consecutiveDataCount;
        }

        private void AssignDataValues(List<Tuple<string, int>> dataList)
        {
            // Declare a tmp List and convert the List to the array
            List<double> TmpList = new List<double>();
            foreach (var item in dataList)
            {
                TmpList.Add(item.Item2);
            }
            DataValues = TmpList.ToArray();
        }

        private void AssignTextValues(List<Tuple<string, int>> dataList)
        {
            // Declare a tmp List and convert the List to the array
            List<string> TmpList = new List<string>();
            foreach (var item in dataList)
            {
                TmpList.Add(item.Item1);
            }
            TextLabels = TmpList.ToArray();
        }
    }
}
