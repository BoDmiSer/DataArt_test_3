using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataArt_test_3
{
    public class GraphVertexInfo
    {
        #region Properties
        public GraphVertex Vertex { get; set; }
        public bool IsUnvisited { get; set; }
        public int EdgesWeightSum { get; set; }
        public GraphVertex PreviousVertex { get; set; }
        #endregion
        #region Constructor
        public GraphVertexInfo(GraphVertex vertex)
        {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
        }
        #endregion
    }
}
