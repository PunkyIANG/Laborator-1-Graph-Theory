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
            
            graph.SetIncidenceMatrix(graph.ParseMatrixFile(@"..\..\..\..\Core\g19incidence.txt"));
            graph.PrintAdjacencyMatrix();
        }
    }
}