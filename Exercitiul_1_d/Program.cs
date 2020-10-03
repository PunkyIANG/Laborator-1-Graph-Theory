using System;
using System.Collections.Generic;
using Core;

namespace Exercitiul_1_d
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph();
            
            // graph.SetAdjacencyMatrix(graph.ParseMatrixFile(@"C:\Users\Professional\iCloudDrive\Grafuri\lab1\Laborator_1\Core\adjacencyMatrix.txt"));
            graph.SetIncidenceMatrix(graph.ParseMatrixFile(@"C:\Users\Professional\iCloudDrive\Grafuri\lab1\Laborator_1\Core\incidenceMatrix.txt"));
            graph.PrintAdjacencyMatrix();
        }
    }
}