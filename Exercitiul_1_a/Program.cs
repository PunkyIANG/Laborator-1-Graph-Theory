using System;
using System.Collections.Generic;
using Core;

namespace Exercitiul_1_a
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            Graph graph = new Graph();
            
            graph.SetIncidenceMatrix(graph.ParseMatrixFile());
            graph.PrintAdjacencyMatrix();
        }
    }
}