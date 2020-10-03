using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace Exercitiul_1_b
{
    class Program
    {
        private static List<List<int>> DominantSets = new List<List<int>>();
        private static int dominationNumber = 0;
        private static List<List<int>> allCombos;
        private static List<List<int>> allDominantSets;


        static void Main(string[] args)
        {
            var graph = new Graph();
            graph.SetAdjacencyMatrix(graph.ParseMatrixFile(@"..\..\..\..\Core\adjacencyMatrix.txt"));    //yes, this looks like shit
                                                                                                                //no, I can't be bothered to fix this
                                                                                                                GetDominantSet(graph.GetAdjacencyMatrix(1));

            Console.WriteLine("Dominant sets: ");

            foreach (var dominantSet in allDominantSets)
            {
                foreach (var id in dominantSet)
                {
                    Console.Write(id + "  ");
                }

                Console.WriteLine();
            }
        }

        private static void GetDominantSet(List<List<int>> modifiedAdjMatrix)
        {
            var allVertexIDs = new List<int>();
            allDominantSets = new List<List<int>>();

            for (var i = 0; i < modifiedAdjMatrix.Count; i++)
            {
                allVertexIDs.Add(i);
            }

            for (var i = 1; i < modifiedAdjMatrix.Count; i++)
            {
                allCombos = new List<List<int>>();
                GetAllCombos(new List<int>(), i, allVertexIDs, -1);

                foreach (var combo in allCombos)
                {
                    var checkAdjacency = new int[allVertexIDs.Count]; //checking 1 1 1 1 1

                    foreach (var id in combo)
                    {
                        for (int j = 0; j < allVertexIDs.Count; j++)
                        {
                            checkAdjacency[j] += modifiedAdjMatrix[id][j]; //adding 1 1 1 1 1
                        }
                    }

                    var isDominantSet = true;

                    foreach (var adjValue in checkAdjacency)
                    {
                        if (adjValue == 0)
                        {
                            isDominantSet = false;
                        }
                    }

                    if (isDominantSet)
                    {
                        allDominantSets.Add(combo);
                    }
                }

                if (allDominantSets.Count != 0)
                {
                    return;
                }
            }
        }

        private static void GetAllCombos(List<int> combos, int reqCombos, List<int> allVertexIDs, int lastVertexID)
        {
            if (reqCombos == 0)
            {
                allCombos.Add(combos);
            }
            else
            {
                for (var i = lastVertexID + 1; i < allVertexIDs.Count; i++)
                {
                    var newCombo = new List<int>(combos);
                    newCombo.Add(allVertexIDs[i]);
                    GetAllCombos(newCombo, reqCombos - 1, allVertexIDs, i);
                }
            }
        }
    }
}