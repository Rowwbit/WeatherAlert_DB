using ScottPlot;

namespace WeatherAlert_DB
{
    /// <summary>
    /// Handles all the logic required for rendering and displaying a graph.
    /// </summary>
    public class GraphGenerator
    {
        GraphDataConstructor graphDataConstructor;
        public enum GraphType
        {
            BarGraph,
            PieChart,
        }
        public GraphGenerator(GraphFilter _graphFilter, WpfPlot wpfPlot, GraphType graphType)
        {
            graphDataConstructor = new GraphDataConstructor(_graphFilter); 
            GenerateGraph(wpfPlot, graphType);
        }
        /// <summary>
        /// Generates a graph for the user based on what graph they want.
        /// </summary>
        /// <param name="wpfPlot"></param>
        /// <param name="graphType"></param>
        public void GenerateGraph(WpfPlot wpfPlot, GraphType graphType)
        {
            switch (graphType)
            {
                case GraphType.BarGraph:
                    SetAsBarGraph(wpfPlot);
                    break;

                case GraphType.PieChart:
                    SetAsPieChart(wpfPlot);
                    break;
            }
        }
        private void SetAsPieChart(WpfPlot wpfPlot)
        {
            // Make the PieChart
            wpfPlot.plt.PlotPie(graphDataConstructor.graphData.DataValues, graphDataConstructor.graphData.TextLabels, showLabels: true);

            // Customize the plot to make it look nicer
            wpfPlot.plt.Grid(false);
            wpfPlot.plt.Frame(false);
            wpfPlot.plt.Ticks(false, false);
        }
        private void SetAsBarGraph(WpfPlot wpfPlot)
        {
            // Make the bar plot
            wpfPlot.plt.PlotBar(graphDataConstructor.graphData.ConsecutiveDataCount, graphDataConstructor.graphData.DataValues);

            // Customize the plot to make it look nicer
            wpfPlot.plt.Axis(y1: 0);
            wpfPlot.plt.Grid(enableVertical: false, lineStyle: LineStyle.Solid);
            wpfPlot.plt.Grid(true);
            wpfPlot.plt.Frame(true);
            wpfPlot.plt.Ticks(true, true);

            // Apply custom axis tick labels
            wpfPlot.plt.XTicks(graphDataConstructor.graphData.ConsecutiveDataCount, graphDataConstructor.graphData.TextLabels);

            // Set graph movement restrictions
            wpfPlot.Configure(enablePanning: true, recalculateLayoutOnMouseUp: false, enableRightClickZoom: true, lockVerticalAxis: false, enableScrollWheelZoom: true);
        }
    }
}
