﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement3
{
    class Program
    {

        static void Main(string[] args)
        {
            string[,] urls = GetMapUrlList();

            // Test maps
            #region
            string map1 =
                    "......\n" +
                    "......\n" +
                    ".A.#..\n" +
                    "......\n" +
                    ".#.#..\n" +
                    "...#B.\n" +
                    "......";
            string map2 =
                "........\n" +
                ".....##.\n" +
                ".....A#B";
            string map3 =
                "............\n" +
                ".........##.\n" +
                ".........A#B";

            //InputHandler ih = new InputHandler();
            //Node[,] map = ih.GetMapFromTextFile(urls[0, 0]);
            //Node[,] map = ih.GetMap(map3);
            //Node start = ih.GetStart();
            //Node goal = ih.GetGoal();
            #endregion  
            
            InputHandler ih = new InputHandler();

            for (int exercise = 0; exercise < urls.GetLength(0); exercise++)
            {
                for (int mapNumber = 0; mapNumber < urls.GetLength(1); mapNumber++)
                {
                    Console.WriteLine("Exercise A." + exercise + " - board-" + (exercise+1) + "-" + (mapNumber+1));
                    Node[,] map = ih.GetMapFromTextFile(urls[exercise, mapNumber]);
                    Node start = ih.GetStart();
                    Node goal = ih.GetGoal();
                    List<Node> result = ShortestPath.RunAStar(map, start, goal);
                    Visualization.PrintResultMap(map, result, start, goal);
                }
            }
        
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadLine();            

        }

        private static string[,] GetMapUrlList()
        {
            string[,] l =  
            {
                {
                    "boards\\board-1-1.txt",
                    "boards\\board-1-2.txt",
                    "boards\\board-1-3.txt",
                    "boards\\board-1-4.txt"
                },
                {
                    "boards\\board-2-1.txt",
                    "boards\\board-2-2.txt",
                    "boards\\board-2-3.txt",
                    "boards\\board-2-4.txt"
                }
            };

            return l;
        }
    }
}









