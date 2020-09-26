namespace DataArt_test_3
{
    public class GraphEdge
    {
        #region Properties
        public GraphVertex ConnectedVertex { get; }
        public int EdgeWeight { get; }
        #endregion
        #region Constructor
        public GraphEdge(GraphVertex connectedVertex, int weight)
        {
            ConnectedVertex = connectedVertex;
            EdgeWeight = weight;
        }
        #endregion
    }
}