using System.Collections.Generic;

namespace Core
{
    public class Edge
    {
        public int id;
        public List<Vertex> ConnectedVertices = new List<Vertex>();

        public Edge()
        {
            
        }

        public Edge(Vertex firstVertex, Vertex secondVertex)
        {
            ConnectedVertices.Add(firstVertex);
            ConnectedVertices.Add(secondVertex);
        }
    }
}