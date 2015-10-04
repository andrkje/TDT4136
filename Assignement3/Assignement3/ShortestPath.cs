using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement3
{
    class ShortestPath
    {
        private static List<Node> allNodes = new List<Node>();

        // -- A*:  returns list of nodes that are part of the shortest path from A to B using A*
        public static List<Node> RunAStar(Node[,] map, Node start, Node goal)
        {
            allNodes = GetListFromArray(map);            
            List<Node> open = new List<Node>(); 
            List<Node> closed = new List<Node>();

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y].IsWalkable())
                    {
                        map[x, y].SetG(Int32.MaxValue);     // Set start value of g = infinity (Int.MaxValue = 2 147 483 647)
                        map[x, y].UpdateF();                // Update f (f = g+h)    
                    }
                }
            }

            open.Add(start);        // Add start to the open list
            start.SetG(0);          // Set g of the starting node = 0
            start.SetH(GetManhattenDistanceBetweenNodes(start, goal));  // And h = manhattendistance to goal
            start.UpdateF();        

            while (open.Count > 0)
            {
                Node currentNode = open.ElementAt(0);       // Set current node to the first element in the open list (which is ascending sorted on f)
                open.RemoveAt(0);           // Remove it from open
                closed.Add(currentNode);    // Add it to the closed list 

                if (currentNode == goal)    // If goal is found, the search is complete
                {
                    List<Node> path = ConstructPath(currentNode);
                    Visualization.PrintMapWithOpenAndClosedWithPath(map, path, start, goal, currentNode, open, closed);     // Print the map with the path from A to B, and show which nodes are in open (*), and closed (x)
                    return path;            // return the path from start to goal
                }
                List<Node> children = GenerateChildren(currentNode);        // Genereate the children of this node (the nodes south, north, east and wes for current node

                foreach (Node child in children)
                {                    
                    if (!child.IsWalkable())        // If child is not walkable (a obsticle or wall), skip it
                        continue;

                    double tentativeG = currentNode.GetG() + child.GetCost();       // calculate tentative g for child
                   
                    if (open.Contains(child))               // If child is in the open list
                    {
                        if (child.GetG() < tentativeG)      //      and current g < tentativeG, skip it
                            continue;
                    }
                    else if (closed.Contains(child))        // If child is in closed
                    {
                        if (child.GetG() < tentativeG)      //      and current g < tentativeG, skip it
                            continue;
                        closed.Remove(child);               // Remove child from open list
                        open.Add(child);                    //      and add it to closed list
                    }
                    else                        // If child is in neither of the lists
                    {
                        open.Add(child);        // Add it to the open list
                        child.SetH(GetManhattenDistanceBetweenNodes(child, goal));  
                        child.UpdateF();        
                    }
                    child.SetG(tentativeG);     
                    child.UpdateF();
                    child.SetParent(currentNode);   // Set parent of child to current node

                }
                closed.Add(currentNode);            // We're done with current node, so add it to closed
                open = GetListSortedOnF(open);      // Sort the open list scending on f

                Visualization.PrintMapWithOpenAndClosed(map, start, goal, currentNode, open, closed);
            }

            return null;    // No path was found
        }

        // -- BFS:  returns list of nodes that are part of the shortest path from A to B using BFS
        public static List<Node> RunBFS(Node[,] map, Node start, Node goal)
        {
            allNodes = GetListFromArray(map);
            List<Node> open = new List<Node>();     // FIFO queue
            List<Node> closed = new List<Node>();   

            for (int y = 0; y < map.GetLength(1); y++)  
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y].SetG(Int32.MaxValue);             // Set g value of all nodes to infinity (Int.MaxValue = 2 147 483 647)
                }
            }

            start.SetG(0);          // Set distance g to starting node = 0
            open.Add(start);       
            while (open.Count > 0)
            {
                Node currentNode = open.ElementAt(0);       // Pick the first element from open list
                open.RemoveAt(0);                           //      and remove it from the lists
                closed.Add(currentNode);                    

                List<Node> children = GenerateChildren(currentNode);        // Genereate the children of this node (the nodes south, north, east and wes for current node
                foreach (Node child in children)
                {
                    if (!child.IsWalkable())         // If child is not walkable (a obsticle or wall), skip it
                        continue;

                    if (child.GetG() > Int32.MaxValue-1)    // If g value of child = infinity, in other words, not touched yet
                    {
                        child.SetG(currentNode.GetG() + child.GetCost());   // Set g (g of current node + the cost of moving from current node to child)
                        child.SetParent(currentNode);       // Set parent to current ndoe
                        open.Add(child);                    
                    }
                    if (child == goal)      // If the goal node is found
                    {
                        List<Node> path = ConstructPath(currentNode);
                        Visualization.PrintMapWithOpenAndClosedWithPath(map, path, start, goal, currentNode, open, closed);
                        return path;    // return the path from start to goal
                    }
                }
                //Visualization.PrintMapWithOpenAndClosed(map, start, goal, currentNode, open, closed);
            }


            return null;        // No path was found

        }

        // -- Dijkstra's:  returns list of nodes that are part of the shortest path from A to B using Dijkstra's algorithm
        public static List<Node> RunDijkstra(Node[,] map, Node start, Node goal)
        {
            allNodes = GetListFromArray(map);
            List<Node> open = new List<Node>();     // List sorted ascending on g
            List<Node> closed = new List<Node>();

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y].IsWalkable())
                    {
                        map[x, y].SetG(Int32.MaxValue);     // Set g value of all nodes to infinity (Int.MaxValue = 2 147 483 647)
                        open.Add(map[x, y]);                //      and add them to the open list 
                    }
                }               
            }

            start.SetG(0);      // Set g of starting node = 0
  
            while (open.Count > 0)
            {
                open = GetListSortedOnG(open);      // Sort list ascending on g

                Node currentNode = open.ElementAt(0);       // Pick the first element from open list
                open.RemoveAt(0);                           //      and remove it from the lists
                closed.Add(currentNode);

                List<Node> children = GenerateChildren(currentNode);        // Genereate the children of this node (the nodes south, north, east and wes for current node

                foreach (Node child in children)
                {
                    double tentativeG = currentNode.GetG() + child.GetCost();       // calculate tentative g for child  
                    if (tentativeG < child.GetG())          // If tenative g < current g of child
                    {                     
                        child.SetG(tentativeG);             // Set g of child = tentative g
                        child.SetParent(currentNode);       // Set current node to parent of child
                    }
                    if (child == goal)      // If gould node is founde
                    {
                        List<Node> path = ConstructPath(currentNode);
                        Visualization.PrintMapWithOpenAndClosedWithPath(map, path, start, goal, currentNode, open, closed);
                        return path;        // Return path from start to goal
                    }
                }

                //Visualization.PrintMapWithOpenAndClosed(map, start, goal, currentNode, open, closed);
            }
            return null;        // No path was found
        }

        // -- Sort list on g
        private static List<Node> GetListSortedOnG(List<Node> list)
        {
           List<Node> result = list;
           result.Sort((a, b) => a.CompareTo(b, 'g'));
           return result;
        }

        // -- Sort list on f
        private static List<Node> GetListSortedOnF(List<Node> list)
        {
            List<Node> result = list;
            result.Sort((a, b) => a.CompareTo(b, 'f'));
            return result;
        }

        // -- Get manhatten distance from node a to node b
        public static int GetManhattenDistanceBetweenNodes(Node current, Node goal)
        {
            int x = current.GetX() - goal.GetX();
            int y = current.GetY() - goal.GetY();
            return (Math.Abs(x) + Math.Abs(y));
        }

        // -- Return child nodes of input node (the nodes north, south, east, and wes on the map)
        private static List<Node> GenerateChildren(Node currentNode)
        {
            List<Node> children = new List<Node>();

            int x = currentNode.GetX();
            int y = currentNode.GetY();

            if (NodeAtCoordinates(x - 1, y) != null)
                children.Add(NodeAtCoordinates(x - 1, y));      // West
            if (NodeAtCoordinates(x + 1, y) != null)
                children.Add(NodeAtCoordinates(x + 1, y));      // East
            if (NodeAtCoordinates(x, y - 1) != null)
                children.Add(NodeAtCoordinates(x, y - 1));      // North
            if (NodeAtCoordinates(x, y + 1) != null)
                children.Add(NodeAtCoordinates(x, y + 1));      //  South

            return children;
        }

        // -- Return the node at coordinates (x, y)
        private static Node NodeAtCoordinates(int x, int y)
        {
            foreach (Node n in allNodes)
            {
                if (n.GetX() == x && n.GetY() == y)
                    return n;
            }
            return null;    // There is no node with coordinates (x, y)
        }

        // -- Return a List<Node> representation of a 2D array of nodes (the map)
        private static List<Node> GetListFromArray(Node[,] nodes)
        {
            List<Node> list = new List<Node>();
            for (int x = 0; x < nodes.GetLength(0); x++)
            {
                for (int y = 0; y < nodes.GetLength(1); y++)
                {
                    list.Add(nodes[x, y]);
                }
            }
            return list;
        }


        // -- Construct a list of the the nodes that are a part of the path form start to goal
        private static List<Node> ConstructPath(Node current)
        {
            List<Node> path = new List<Node>();
            Node temp = current;
            while (temp.GetParent() != null)       // As long as the current node has a parent,
            {
                path.Add(temp);                     //  add it to the path list
                temp = temp.GetParent();           //  and set temp to this node
            }
            return path;
        }
    } 
}
