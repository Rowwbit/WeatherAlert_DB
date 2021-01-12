using System;
using System.Collections.Generic;

namespace WeatherAlert_DB
{
    /// <summary>
    /// This class offers a way to Parse the Tuples data based on the GraphFilter before passing it to the GraphData class. 
    /// </summary>
    public class GraphDataValidator
    {
        public List<Tuple<string, int>> TupleList = new List<Tuple<string, int>>();

        public GraphDataValidator(List<Tuple<string, int>> tupleList, GraphFilter _graphFilter)
        {
            TupleList = tupleList;
            ChooseDataToValidate(_graphFilter, tupleList);
        }
        private void ChooseDataToValidate(GraphFilter graphFilter, List<Tuple<string, int>> tupleList)
        {
            // Run specific methods depending on filtername
            switch (graphFilter.filterName)
            {
                case GraphFilter.FilterName.EventType:
                    break;
                case GraphFilter.FilterName.State:
                    break;
                case GraphFilter.FilterName.Severity:
                    break;
                case GraphFilter.FilterName.DescriptionKeywords:
                    ValidateForDescriptiveKeywords(tupleList);
                    break;
            }
        }
        private void ValidateForDescriptiveKeywords(List<Tuple<string, int>> tupleList)
        {
            // Make temp TupleList
            List<Tuple<string, int>> TupleListToEdit = new List<Tuple<string, int>>();

            // Grab the Words set in the Alert class and then count the number of times the word is referenced.
            foreach (var word in Alert.DescriptorWords)
            {
                TupleListToEdit.Add(new Tuple<string, int>(word, tupleList.FindAll(x => x.Item1.Contains(word)).Count));
            }
            // Remove any elements that have zero counts to prevent unnecessary clutter
            TupleListToEdit.RemoveAll(x => x.Item2 < 1);           
            
            // Save the current edited temp tuple list to the main one
            TupleList = TupleListToEdit;
        }
    }
}
