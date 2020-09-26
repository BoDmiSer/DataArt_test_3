using System.Collections.Generic;

namespace DataArt_test_3
{
    public class GraphVertex
    {
        #region Properties
        public string Name { get; }
        public List<GraphEdge> Edges { get; }
        #endregion
        #region Constructor
        public GraphVertex(string vertexName)
        {
            Name = vertexName;
            Edges = new List<GraphEdge>();
        }
        #endregion
        #region Methods
        public void AddEdge(GraphEdge newEdge)
        {
            Edges.Add(newEdge);
        }
        public void AddEdge(GraphVertex vertex, int edgeWeight)
        {
            AddEdge(new GraphEdge(vertex, edgeWeight));
        }
        #endregion
        #region Overide
        public override string ToString() => Name;
        #endregion
    }
}