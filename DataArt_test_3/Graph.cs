using System.Collections.Generic;

namespace DataArt_test_3
{
    public class Graph
    {
        #region Properties
        public List<GraphVertex> Vertices { get; }
        #endregion

        #region Constructors
        public Graph()
        {
            Vertices = new List<GraphVertex>();
        }
        #endregion

        #region Methods
        public void AddVertex(string vertexName)
        {
            Vertices.Add(new GraphVertex(vertexName));
        }
        public GraphVertex FindVertex(string vertexName)
        {
            foreach (var v in Vertices)
            {
                if (v.Name.Equals(vertexName))
                {
                    return v;
                }
            }

            return null;
        }
        public void AddEdge(string firstName, string secondName, int weight)
        {
            var v1 = FindVertex(firstName);
            var v2 = FindVertex(secondName);
            if (v2 != null && v1 != null)
            {
                v1.AddEdge(v2, weight);
                v2.AddEdge(v1, weight);
            }
        }
        #endregion
    }
}