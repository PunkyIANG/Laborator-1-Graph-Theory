using System;
using System.Collections.Generic;

namespace Core
{
    public class Vertex
    {
        public int id;
        public List<Vertex> AdjacentVertices = new List<Vertex>();
        public List<Edge> AdjacentEdges = new List<Edge>();

        public int GetVertexDegree()
        {
            return AdjacentVertices.Count;
        }

        public Vertex(int id)
        {
            this.id = id;
        }
    }
}