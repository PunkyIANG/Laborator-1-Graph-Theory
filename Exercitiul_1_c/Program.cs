﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace Exercitiul_1_c
{
    class Program
    {
        private static List<Vertex> discoveredVertices;
        private static List<Vertex> researchedVertices;

        private static Graph graph;
        
        private static void Main(string[] args)
        {
            graph.SetIncidenceMatrix(graph.ParseMatrixFile(@"..\..\..\..\Core\g19incidence.txt"));

            // InitDepthFirstSearch(graph.Vertices.First());
            InitBreadthFirstSearch(graph.Vertices.First());
        }

        private static void InitBreadthFirstSearch(Vertex startingVertex)
        {
            discoveredVertices = new List<Vertex>();
            researchedVertices = new List<Vertex>();
            
            discoveredVertices.Add(startingVertex);
            Console.WriteLine();
            PrintBFSState();

            while (discoveredVertices.Count != 0)
            {
                startingVertex = discoveredVertices.First();

                discoveredVertices.AddRange(GetUndiscoveredNeighbours(startingVertex));
                researchedVertices.Add(startingVertex);
                discoveredVertices.Remove(startingVertex);
                PrintBFSState();
            }
        }
        
        private static void PrintBFSState()
        {
            Console.Write("Atins:    ");
            foreach (var vertex in discoveredVertices)
            {
                Console.Write((vertex.id + 1) + " ");
            }
            Console.WriteLine();
            
            Console.Write("Cercetat: ");
            foreach (var vertex in researchedVertices)
            {
                Console.Write((vertex.id + 1) + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void InitDepthFirstSearch(Vertex startingVertex)
        {
            discoveredVertices = new List<Vertex>();
            researchedVertices = new List<Vertex>();
            
            Console.Write("Varf:  ");
            foreach (var vertex in graph.Vertices)
            {
                Console.Write(vertex.id + " ");
            }
            Console.WriteLine();
            
            DepthFirstSearch(startingVertex);
        }
        
        private static void DepthFirstSearch(Vertex startingVertex)
        {
            discoveredVertices.Add(startingVertex);
            PrintDFSState(graph);
            var undiscoveredNeighbours = GetUndiscoveredNeighbours(startingVertex);

            while (undiscoveredNeighbours.Count != 0) 
            {
                DepthFirstSearch(undiscoveredNeighbours.First());
                undiscoveredNeighbours = GetUndiscoveredNeighbours(startingVertex);
            }

            researchedVertices.Add(startingVertex);
            PrintDFSState(graph);
        }

        private static void PrintDFSState(Graph graph)
        {
            Console.Write("Stare: ");
            
            foreach (var vertex in graph.Vertices)
            {
                if (CheckIfContains(researchedVertices, vertex))
                {
                    Console.Write("2 ");
                }
                else if (CheckIfContains(discoveredVertices, vertex))
                {
                    Console.Write("1 ");
                }
                else
                {
                    Console.Write("0 ");
                }
            }
            
            Console.WriteLine();
        }
        
        private static List<Vertex> GetUndiscoveredNeighbours(Vertex vertex)
        {
            var result = new List<Vertex>();

            foreach (var neighbour in vertex.AdjacentVertices)
            {
                if (!(CheckIfContains(discoveredVertices, neighbour) || CheckIfContains(researchedVertices, neighbour)))
                {
                    result.Add(neighbour);
                }
            }
            
            return result;
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

    }
}