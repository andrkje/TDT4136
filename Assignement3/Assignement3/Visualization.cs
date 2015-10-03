using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement3
{
    static class Visualization
    {
        // -- Print the map of nodes, with start (A) and goal (B)
        public static void PrintMap(Node[,] map, Node start, Node goal)
        {
            string s = "";

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (!map[x, y].IsWalkable())      // If the current node (x,y) is not wakable (a wall/obstacle)
                        s += "#";
                    else if (start.GetX() == x && start.GetY() == y)    // If current node is start, print 'A'
                        s += "A";
                    else if (goal.GetX() == x && goal.GetY() == y)      // If current node is goal, print 'A'
                        s += "B";
                    else if (map[x, y].GetCost() == 100)    // If current node is water, cost = 100, print 'w'
                        s += "w";
                    else if (map[x, y].GetCost() == 50)     // If current node is moutain, cost = 50, print 'm'
                        s += "m";
                    else if (map[x, y].GetCost() == 10)     // If current node is water, cost = 10, print 'f'
                        s += "f";
                    else if (map[x, y].GetCost() == 5)      // If current node is water, cost = 5, print 'g'
                        s += "g";
                    else if (map[x, y].GetCost() == 1)      // If current node is water, cost = 1, print 'r'
                        s += "r";
                    else
                        s += ".";
                }
                s += "\n";
            }
            Console.WriteLine("Map:\n" + s);      // Print the map
        }

        // -- Print the map with the path from A to B
        public static void PrintResultMap(Node[,] map, List<Node> result, Node start, Node goal)
        {
            string s = "";

            result.RemoveAt(0);     // Removes the first element (goal), to show the goal as B when printed

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    bool isInResult = false;
                    foreach (Node node in result)       // Check if the current node (x,y) is a part of the path
                    {
                        if (node.GetX() == x && node.GetY() == y)
                            isInResult = true;
                    }
                    if (isInResult)      // If the current node (x,y) is a part of the path, represent it as 'O'  
                        s += "O";
                    else if (!map[x, y].IsWalkable())      // If the current node (x,y) is not wakable (a wall/obstacle)
                        s += "#";
                    else if (start.GetX() == x && start.GetY() == y)    // If current node is start, print 'A'
                        s += "A";
                    else if (goal.GetX() == x && goal.GetY() == y)      // If current node is goal, print 'A'
                        s += "B";
                    else if (map[x, y].GetCost() == 100)    // If current node is water, cost = 100, print 'w'
                        s += "w";
                    else if (map[x, y].GetCost() == 50)     // If current node is moutain, cost = 50, print 'm'
                        s += "m";
                    else if (map[x, y].GetCost() == 10)     // If current node is water, cost = 10, print 'f'
                        s += "f";
                    else if (map[x, y].GetCost() == 5)      // If current node is water, cost = 5, print 'g'
                        s += "g";
                    else if (map[x, y].GetCost() == 1)      // If current node is water, cost = 1, print 'r'
                        s += "r";
                    else
                        s += ".";               
                }
                s += "\n";
                
            }
            Console.WriteLine("Map with path:\n" + s);      // Print the map

        }

        // -- Print the map with the nodes in the open list (*) and in the closed list(x)
        public static void PrintMapWithOpenAndClosed(Node[,] map, Node start, Node goal, Node current, List<Node> open, List<Node> closed)
        {
            string s = "";

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y] == current)       // If current node is the position we are in, print 'o'
                        s += "o";
                    else if (start.GetX() == x && start.GetY() == y)    // If current node is start, print 'A'
                        s += "A";
                    else if (goal.GetX() == x && goal.GetY() == y)      // If current node is goal, print 'B'
                        s += "B";
                    else if (open.Contains(map[x, y]))      // If current node is in the open list, print '*'
                        s += "*";
                    else if (closed.Contains(map[x, y]))    // If current node is in the open list, print 'x'
                        s += "x";
                    else if (!map[x, y].IsWalkable())      // If the current node (x,y) is not wakable (a wall/obstacle)
                        s += "#";                    
                    else if (map[x, y].GetCost() == 100)    // If current node is water, cost = 100, print 'w'
                        s += "w";
                    else if (map[x, y].GetCost() == 50)     // If current node is moutain, cost = 50, print 'm'
                        s += "m";
                    else if (map[x, y].GetCost() == 10)     // If current node is water, cost = 10, print 'f'
                        s += "f";
                    else if (map[x, y].GetCost() == 5)      // If current node is water, cost = 5, print 'g'
                        s += "g";
                    else if (map[x, y].GetCost() == 1)      // If current node is water, cost = 1, print 'r'
                        s += "r";
                    else
                        s += ".";
                }
                s += "\n";
            }
            Console.WriteLine("Map with open('*') and closed('x'):\n" + s);      // Print the map
        }

        //  // -- Print the map with the path from A to B, pluss which nodes are in the open list (*) and in the closed list(x)  
        public static void PrintMapWithOpenAndClosedWithPath(Node[,] map, List<Node> path, Node start, Node goal, Node current, List<Node> open, List<Node> closed)
        {
            string s = "";

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    bool isInResult = false;
                    foreach (Node node in path)       // Check if the current node (x,y) is a part of the path
                    {
                        if (node.GetX() == x && node.GetY() == y)
                            isInResult = true;
                    }
                    if (isInResult)      // If the current node (x,y) is a part of the path, represent it as 'O'  
                        s += "O";                  
                    else if (start.GetX() == x && start.GetY() == y)    // If current node is start, print 'A'
                        s += "A";
                    else if (goal.GetX() == x && goal.GetY() == y)      // If current node is goal, print 'B'
                        s += "B";
                    else if (open.Contains(map[x, y]))      // If current node is in the open list, print '*'
                        s += "*";
                    else if (closed.Contains(map[x, y]))    // If current node is in the open list, print 'x'
                        s += "x";
                    else if (!map[x, y].IsWalkable())      // If the current node (x,y) is not wakable (a wall/obstacle)
                        s += "#";
                    else if (map[x, y].GetCost() == 100)    // If current node is water, cost = 100, print 'w'
                        s += "w";
                    else if (map[x, y].GetCost() == 50)     // If current node is moutain, cost = 50, print 'm'
                        s += "m";
                    else if (map[x, y].GetCost() == 10)     // If current node is water, cost = 10, print 'f'
                        s += "f";
                    else if (map[x, y].GetCost() == 5)      // If current node is water, cost = 5, print 'g'
                        s += "g";
                    else if (map[x, y].GetCost() == 1)      // If current node is water, cost = 1, print 'r'
                        s += "r";
                    else
                        s += ".";
                }
                s += "\n";
            }
            Console.WriteLine("Map with open('*'), closed('X') and path('O'):\n" + s);      // Print the map
        }

        // -- Print list of nodes
        public static void PrintListOfNodes(List<Node> list)
        {
            if (list == null)
                Console.WriteLine("List is empty");
            foreach (Node node in list)
	        {
                Console.WriteLine(node);
	        }
        }




    }
}
