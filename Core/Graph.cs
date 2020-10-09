using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core
{
    public class Graph
    {
        public List<Vertex> Vertices;
        public List<Edge> Edges;

        public void SetIncidenceMatrix(List<List<int>> matrix)
        {
            var height = matrix.Count;
            var width = matrix[0].Count;
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();

            
            for (var i = 0; i < height; i++)
            {
                if (matrix[i].Count != width)
                {
                    Console.WriteLine("Error: wrong num count on row " + i);    //rewrite this in a separate function
                    return;
                }
            }
            
            for (var i = 0; i < height; i++)
            {
                Vertices.Add(new Vertex(i));    //rewrite this as well
            }
            
            for (var j = 0; j < width; j++)
            {
                Edges.Add(new Edge(j));          //you get it
            }

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (matrix[i][j] == 1)
                    {
                        Edges[j].ConnectedVertices.Add(Vertices[i]);    //just setting the edges for now
                        Vertices[i].AdjacentEdges.Add(Edges[j]);
                    }
                }
            }

            foreach (var edge in Edges)
            {
                edge.ConnectedVertices[0].AdjacentVertices.Add(edge.ConnectedVertices[1]);
                edge.ConnectedVertices[1].AdjacentVertices.Add(edge.ConnectedVertices[0]);    //probably rewrite this as well
            }
            
            PrintGraphStats();
        }

        public void PrintIncidenceMatrix()
        {
            var height = Vertices.Count;
            var width = Edges.Count;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (Edges[j].ConnectedVertices.Contains(Vertices[i]))
                    {
                        Console.Write("1  ");
                    }
                    else
                    {
                        Console.Write("0  ");
                    }
                }
                Console.WriteLine();
            }
        }

        public void SetKirchhoffMatrix(List<List<int>> matrix)
        {
            var height = matrix.Count;
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();

            for (var i = 0; i < height; i++)
            {
                if (matrix[i].Count != height)
                {
                    Console.WriteLine("Error: wrong num count on row " + i);
                    return;
                }
            }            

            for (var i = 0; i < height; i++)
            {
                Vertices.Add(new Vertex(i));
            }

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (matrix[i][j] != matrix[j][i])
                    {
                        Console.WriteLine("Error: numbers at pos (" + i + ", " + j + ") and (" + j + ", " + i + ") should be the same");
                        return;
                    }
                    else if (matrix[i][j] == -1 && j > i) 
                    {
                        var newEdge = new Edge(Vertices[i], Vertices[j]);
                        if (!Edges.Contains(newEdge)) 
                        {
                            Edges.Add(newEdge);

                            Vertices[i].AdjacentEdges.Add(newEdge);
                            Vertices[j].AdjacentEdges.Add(newEdge);

                            Vertices[i].AdjacentVertices.Add(Vertices[j]);
                            Vertices[j].AdjacentVertices.Add(Vertices[i]);
                        }
                    }
                }

                if (matrix[i][i] != Vertices[i].GetVertexDegree())
                {
                    Console.WriteLine("Warning: wrong degree value detected at vertex " + i);
                }
            }
            
            for (int i = 0; i < Edges.Count; i++)
            {
                Edges[i].id = i;
            }

            PrintGraphStats();
        }

        public void PrintKirchhoffMatrix()
        {
            var height = Vertices.Count;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i == j)
                    {
                        Console.Write(Vertices[i].GetVertexDegree() + "  ");
                    }
                    else
                    {
                        if (Vertices[i].AdjacentVertices.Contains(Vertices[j]))
                        {
                            Console.Write("-1  ");
                        }
                        else
                        {
                            Console.Write("0  ");
                        }
                    }
                }

                Console.WriteLine();
            }
        }


        public void SetAdjacencyMatrix(List<List<int>> matrix)
        {
            var height = matrix.Count;
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();

            foreach (var row in matrix) //check matrix
            {
                if (row.Count != height)
                {
                    Console.Write("Error: wrong num count on row ");
                    foreach (var number in row)
                    {
                        Console.Write(number + " ");
                    }

                    Console.WriteLine();
                    return;
                }
            }

            for (int i = 0; i < height; i++)
            {
                Vertices.Add(new Vertex(i));
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i == j) //diagonal stuff
                    {
                        if (matrix[i][j] != 0)
                        {
                            Console.WriteLine("Warning: number at pos (" + i + ", " + j + ") should be 0");
                        }
                    }
                    else
                    {
                        if (matrix[i][j] != matrix[j][i])
                        {
                            Console.WriteLine("Error: numbers at pos (" + i + ", " + j + ") and (" + j + ", " + i + ") should be the same");
                            return;
                        }
                        else if (matrix[i][j] == 1 && j > i) //make sure this ain't gonna repeat twice
                        {
                            var newEdge = new Edge(Vertices[i], Vertices[j]);
                            if (!Edges.Contains(newEdge)) //basically irrelevant, for some reason list.Contains() doesn't work here
                            {
                                Edges.Add(newEdge);

                                Vertices[i].AdjacentEdges.Add(newEdge);
                                Vertices[j].AdjacentEdges.Add(newEdge);

                                Vertices[i].AdjacentVertices.Add(Vertices[j]);
                                Vertices[j].AdjacentVertices.Add(Vertices[i]);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < Edges.Count; i++)
            {
                Edges[i].id = i;
            }
            
            PrintGraphStats();
        }

        public void PrintAdjacencyMatrix()
        {
            var height = Vertices.Count;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i == j)
                    {
                        Console.Write("0  ");
                    }
                    else
                    {
                        if (Vertices[i].AdjacentVertices.Contains(Vertices[j]))
                        {
                            Console.Write("1  ");
                        }
                        else
                        {
                            Console.Write("0  ");
                        }
                    }
                }

                Console.WriteLine();
            }
        }
        
        public List<List<int>> GetAdjacencyMatrix(int diagonalNumber)
        {
            var result = new List<List<int>>();
            
            var height = Vertices.Count;

            for (var i = 0; i < height; i++)
            {
                result.Add(new List<int>());
                for (var j = 0; j < height; j++)
                {
                    if (i == j)
                    {
                        // Console.Write("0  ");
                        result[i].Add(diagonalNumber);
                    }
                    else
                    {
                        if (Vertices[i].AdjacentVertices.Contains(Vertices[j]))
                        {
                            //Console.Write("1  ");
                            result[i].Add(1);
                        }
                        else
                        {
                            //Console.Write("0  ");
                            result[i].Add(0);
                        }
                    }
                }

                Console.WriteLine();
            }
            
            return result;
        }


        public void PrintGraphStats()
        {
            Console.WriteLine("There are " + Vertices.Count + " vertices and " + Edges.Count + " edges");
            
            Console.Write("Vertex IDs: ");
            foreach (var vertex in Vertices)
            {
                Console.Write(vertex.id + " ");
            }
            Console.WriteLine();


            Console.Write("Adjacency: ");
            foreach (var vertex in Vertices)
            {
                Console.Write(vertex.GetVertexDegree() + " ");
            }
            Console.WriteLine();

            Console.Write("Edges: ");
            foreach (var edge in Edges)
            {
                Console.Write("(" + edge.ConnectedVertices[0].id + ", " + edge.ConnectedVertices[1].id + ") ");
            }
            Console.WriteLine();

            
            Console.Write("Edge IDs: ");
            foreach (var edge in Edges)
            {
                Console.Write(edge.id + " ");
            }
            Console.WriteLine();
        }


        public List<List<int>> ParseMatrixFile(string path)
        {
            string line;
            List<List<int>> matrix = new List<List<int>>();

            // Read the file and display it line by line.  
            var file = new StreamReader(path);

            while ((line = file.ReadLine()) != null)
            {
                matrix.Add(Array.ConvertAll(line.Split(' '), int.Parse).ToList());
            }

            foreach (var row in matrix)
            {
                foreach (var number in row)
                {
                    Console.Write(number + "  ");
                }

                Console.WriteLine();
            }

            file.Close();

            return matrix;
        }
    }
}