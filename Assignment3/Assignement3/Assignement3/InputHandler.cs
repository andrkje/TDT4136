using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement3
{
    class InputHandler
    {

        Dictionary<char, int> moveMentCosts;        // Dictionary with all the movement costs, represented by characters
        
        Node start = null;      // Start node
        Node goal = null;       // Goal node

        public InputHandler()
	    {
            moveMentCosts = new Dictionary<char, int>();
            initMovementCosts();
	    }

        // -- Initialize the movement cost list
        private void initMovementCosts()
        {
            moveMentCosts.Add('A', 1);     // starting node
            moveMentCosts.Add('B', 1);     // Goal node
            moveMentCosts.Add('.', 1000);  // Free node (exercise A.1), using the movement cost of 1000 ti differentiate form exercise2
            moveMentCosts.Add('#', 1);     // Obsticle / wall (exercise A.1)
            moveMentCosts.Add('w', 100);   // Water (exercise A.2)
            moveMentCosts.Add('m', 50);    // Mountain (exercise A.2)
            moveMentCosts.Add('f', 10);    // Forest (exercise A.2)
            moveMentCosts.Add('g', 5);     // Grass (exercise A.2)
            moveMentCosts.Add('r', 1);     // Road 
        }

        // -- Read map from textfile
        public Node[,] GetMapFromTextFile(string url)
        {
            string text = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, url));
            return GetMap(text.Trim());     // Trim to ensure there is no white space on the end of the text
        }
        public Node[,] GetMap(string stringMap)
        {
            string[] lines = stringMap.Split('\n');
            Node[,] mapList = new Node[lines[0].Length, lines.Length];

            for (int y = 0; y < lines.Length; y++)
            {
                char[] squares = lines[y].ToCharArray();
                for (int x = 0; x < squares.Length; x++)
                {                    
                    int cost = moveMentCosts[squares[x]];       // Gets the cost to move to current current node
                    mapList[x, y] = new Node(x, y, cost);
                    if (squares[x] == '#')                      // Set to non-walkable
                        mapList[x, y].SetWalkable(false);       
                    else if (squares[x] == 'A')                 // Set to start node
                        start = mapList[x, y];
                    else if (squares[x] == 'B')                 // set to goal node
                        goal = mapList[x, y];
                    
                }
            }
            return mapList;
        }

        // ---- Getters ----
        public Node GetStart() { return start; }
        public Node GetGoal() { return goal; }

    }
}
