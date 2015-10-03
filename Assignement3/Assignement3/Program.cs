using System;
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
            
            InputHandler ih = new InputHandler();
            Node[,] map = ih.GetMapFromTextFile(urls[1, 3]);
            //Node[,] map = ih.GetMap(map3);
            Node start = ih.GetStart();
            Node goal = ih.GetGoal();

            //Visualization.PrintMap(map, start, goal);

            //Algorithm a = new Algorithm.BFS;

            List<Node> result = ShortestPath.RunAStar(map, start, goal);
            //Visualization.PrintListOfNodes(result);
            //Visualization.PrintResultMap(map, result, start, goal);

            /*
            InputHandler ih = new InputHandler();
            Node[,] map = ih.GetMapFromTextFile(urls[0, 0]);
            Node start = ih.GetStart();
            Node goal = ih.GetGoal();
            List<Node> result = AStar.RunAStar(map, start, goal);

            Console.WriteLine("\n_____________________________________________________________\n\n");

            map = ih.GetMapFromTextFile(urls[0, 1]);
            start = ih.GetStart();
            goal = ih.GetGoal();
            result = AStar.RunAStar(map, start, goal);
            
            Console.WriteLine("\n_____________________________________________________________\n\n");

            map = ih.GetMapFromTextFile(urls[0, 2]);
            start = ih.GetStart();
            goal = ih.GetGoal();
            result = AStar.RunAStar(map, start, goal);

            Console.WriteLine("\n_____________________________________________________________\n\n");

            map = ih.GetMapFromTextFile(urls[0, 3]);
            start = ih.GetStart();
            goal = ih.GetGoal();
            result = AStar.RunAStar(map, start, goal);
            */


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
                },
                {
                    "",
                    "",
                    "",
                    ""
                }
            };

            return l;
        }
    }
}
