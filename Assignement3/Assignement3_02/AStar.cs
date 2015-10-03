using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement3_02
{
    class AStar
    {
        private static List<Node> allNodes = new List<Node>();

        public static List<Node> Run(Node[,] map, Node start, Node goal, char algorightm)
        {

            allNodes = GetListFromArray(map);
            //char algorithm = 'b';
            //return Run(map, start, goal, algorithm);

            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();

            //int count = 0;


            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y].IsWalkable())
                    {
                        map[x, y].SetG(Int32.MaxValue);
                        map[x, y].UpdateF();
                       
                    }
                }
            }

            
            open.Add(start);
            start.SetG(0);
            start.SetH(GetManhattenDistanceBetweenNodes(start, goal));
            start.UpdateF();

            //Console.WriteLine("start: "+ start);


            int c = 0;
            while (open.Count > 0)
            {
                //Console.WriteLine("Open.Length: " + open.Count() + ", count = " + c++);
                
                Node currentNode = open.ElementAt(0);
                open.RemoveAt(0);
                closed.Add(currentNode);

                //Console.WriteLine(currentNode + "\n");

                if (currentNode == goal)
                {
                    List<Node> path = ConstructPath(currentNode);
                   
                    Visualization.PrintMapWithOpenAndClosedWithPath(map, path, start, goal, currentNode, open, closed);
                    return path;
                }
                List<Node> children = GenerateChildren(currentNode);

                foreach (Node child in children)
                {                    
                    if (!child.IsWalkable())
                        continue;

                    double tentativeG = currentNode.GetG() + child.GetCost();
                   
                    if (open.Contains(child))
                    {
                        if (child.GetG() < tentativeG)
                            continue;
                    }
                    else if (closed.Contains(child))
                    {
                        if (child.GetG() < tentativeG)
                            continue;
                        closed.Remove(child);
                        open.Add(child);
                    }
                    else
                    {
                        open.Add(child);
                        child.SetH(GetManhattenDistanceBetweenNodes(child, goal));
                        child.UpdateF();
                    }
                    child.SetG(tentativeG);
                    child.UpdateF();
                    child.SetParent(currentNode);

                }
                closed.Add(currentNode);
                open = GetListSortedOnF(open);

                //Visualization.PrintMapWithOpenAndClosed(map, start, goal, currentNode, open, closed);
            }


            return null;
        }

        
        
        public static List<Node> RunAStar(Node[,] map, Node start, Node goal)
        {
            char algorithm  = 'a';
            return Run(map, start, goal, algorithm);
        }

        public static List<Node> RunBFS(Node[,] map, Node start, Node goal)
        {
            allNodes = GetListFromArray(map);
            //char algorithm = 'b';
            //return Run(map, start, goal, algorithm);

            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();

            //int count = 0;

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y].SetG(Int32.MaxValue); 
                }
            }

            start.SetG(0);
            open.Add(start);
            while (open.Count > 0)
            {
                Node currentNode = open.ElementAt(0);
                open.RemoveAt(0);
                closed.Add(currentNode);

                List<Node> children = GenerateChildren(currentNode);
                foreach (Node child in children)
                {
                    
                    if (!child.IsWalkable())
                        continue;

                    if (child.GetG() > Int32.MaxValue-1)
                    {
                        child.SetG(currentNode.GetG() + child.GetCost());
                        child.SetParent(currentNode);
                        open.Add(child);
                    }
                    if (child == goal)
                    {
                        List<Node> path = ConstructPath(currentNode);
                        Visualization.PrintMapWithOpenAndClosedWithPath(map, path, start, goal, currentNode, open, closed);
                        return path;
                    }
                }
                //Visualization.PrintMapWithOpenAndClosed(map, start, goal, currentNode, open, closed);
            }


            return null;

        }

        public static List<Node> RunDijkstra(Node[,] map, Node start, Node goal)
        {
            allNodes = GetListFromArray(map);
            //char algorithm = 'b';
            //return Run(map, start, goal, algorithm);

            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();

            //int count = 0;

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y].IsWalkable())
                    {
                        map[x, y].SetG(Int32.MaxValue);
                        open.Add(map[x, y]);
                    }
                }               
            }

            start.SetG(0);
            //open.Remove(start);
            //closed.Add(start);
  
            while (open.Count > 0)
            {
                open = GetListSortedOnG(open);

                Node currentNode = open.ElementAt(0);
                open.RemoveAt(0);
                closed.Add(currentNode);

                List<Node> children = GenerateChildren(currentNode);

                foreach (Node child in children)
                {
                    double tentativeG = currentNode.GetG() + child.GetCost();
                    if (tentativeG < child.GetG())
                    {                     
                        child.SetG(tentativeG);
                        child.SetParent(currentNode);
                        //open.Remove(child);

                    }
                    if (child == goal)
                    {
                        List<Node> path = ConstructPath(currentNode);
                        Visualization.PrintMapWithOpenAndClosedWithPath(map, path, start, goal, currentNode, open, closed);
                        return path;
                    }
                }

                //Visualization.PrintMapWithOpenAndClosed(map, start, goal, currentNode, open, closed);
            }


            return null;
        }

        private static List<Node> GetListSortedOnG(List<Node> list)
        {
           List<Node> result = list;
           result.Sort((a, b) => a.CompareTo(b, 'g'));
           return result;
        }

        private static List<Node> GetListSortedOnF(List<Node> list)
        {
            List<Node> result = list;
            result.Sort((a, b) => a.CompareTo(b, 'f'));
            return result;
        }


        public static int GetManhattenDistanceBetweenNodes(Node current, Node goal)
        {
            int x = current.GetX() - goal.GetX();
            int y = current.GetY() - goal.GetY();
            return (Math.Abs(x) + Math.Abs(y));
        }

        private static List<Node> GenerateChildren(Node currentNode)
        {
            List<Node> children = new List<Node>();

            int x = currentNode.GetX();
            int y = currentNode.GetY();

            if (NodeAtCoordinates(x - 1, y) != null)
                children.Add(NodeAtCoordinates(x - 1, y));
            if (NodeAtCoordinates(x + 1, y) != null)
                children.Add(NodeAtCoordinates(x + 1, y));
            if (NodeAtCoordinates(x, y - 1) != null)
                children.Add(NodeAtCoordinates(x, y - 1));
            if (NodeAtCoordinates(x, y + 1) != null)
                children.Add(NodeAtCoordinates(x, y + 1));

            return children;
        }

        // -- Return the node at coordinates (x, y)
        private static Node NodeAtCoordinates(int x, int y)
        {
            foreach (Node n in allNodes)
            {
                //Console.WriteLine(n.ToString());
                if (n.GetX() == x && n.GetY() == y)
                    return n;
            }
            return null;
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
