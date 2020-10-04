using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace Exercitiul_1_a
{
    internal class Program
    {
        private static List<List<Vertex>> IndependentSets = new List<List<Vertex>>();
        private static int alpha0 = 0;
        
        private static void Main(string[] args)
        {
            var graph = new Graph();
            graph.SetIncidenceMatrix(graph.ParseMatrixFile(@"..\..\..\..\Core\g19incidence.txt"));
            
            BronKerbosch(graph.Vertices, new List<Vertex>(), new List<Vertex>());
            
            Console.WriteLine("Results: ");
            foreach (var independentSet in IndependentSets)
            {
                if (independentSet.Count != alpha0)    //comment this statement to print maximal independent sets
                {
                    continue;
                }
                
                foreach (var vertex in independentSet)
                {
                    Console.Write(vertex.id + " ");
                }
                Console.WriteLine();
            }
        }

        private static void BronKerbosch(List<Vertex> qPlus, List<Vertex> qMinus, List<Vertex> s)
        {
            Console.WriteLine("started cycle " + CheckQPlusQMinusAdj(qPlus, qMinus));
            while ((qPlus.Count != 0) && CheckQPlusQMinusAdj(qPlus, qMinus))
            {
                var v = qPlus.First();
                Console.WriteLine(v.id);
                s.Add(v);
                var qPlusNew = GetAllNonNeighbors(qPlus, v, true);
                var qMinusNew = GetAllNonNeighbors(qMinus, v, false);
                
                if (qPlusNew.Count == 0 && qMinusNew.Count == 0)
                {
                    IndependentSets.Add(new List<Vertex>(s));
                    if (s.Count > alpha0 || alpha0 == 0)
                    {
                        alpha0 = s.Count;
                    }
                }
                else
                {
                    BronKerbosch(qPlusNew, qMinusNew, s);
                }

                s.Remove(v);
                qPlus.Remove(v);
                qMinus.Add(v);
            }
            Console.WriteLine("stopped cycle");
        }

        private static bool CheckQPlusQMinusAdj(List<Vertex> qPlus, List<Vertex> qMinus)
        {
            //check if each vertex in q- is a neighbor of any vertex in q+
            foreach (var vertex in qMinus)
            {
                foreach (var adjacentVertex in vertex.AdjacentVertices)
                {
                    if (CheckIfContains(qPlus, adjacentVertex))
                    {
                        break;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool CheckIfContains(List<Vertex> list, Vertex vertex)
        {
            foreach (var mainVertex in list)
            {
                if (mainVertex.id == vertex.id)
                {
                    return true;
                }
            }

            return false;
        }

        private static List<Vertex> GetAllNonNeighbors(List<Vertex> mainList, Vertex exception, bool excludeException)
        {
            var result = new List<Vertex>();
            foreach (var vertex in mainList)
            {
                if (CheckIfContains(exception.AdjacentVertices, vertex))
                {
                    continue;
                }
                if (vertex.id == exception.id && excludeException)
                {
                    continue;
                }
                result.Add(vertex);
            }

            return result;
        }
    }
}