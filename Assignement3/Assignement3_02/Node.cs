using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement3_02
{
    class Node
    {
        private int x, y, cost;     // Coordinates on the format (x, y)
        private double g;           // g = cost of getting to this node
        private double h;           // h = heuristic, estimated cost from this node to goal
        private double f;           // f = g + h
        private Node parent;        // Parent of this node
        private bool walkable;     

        public Node(int x, int y, int cost)
        {
            this.x = x;
            this.y = y;
            this.cost = cost;
            walkable = true;        // Nodes are walkable by default
        }

        // -- Compare one node to another based on either f or g
        public int CompareTo(Node otherNode, char c)
        {
            if (c == 'f')
            return Convert.ToInt32(this.f - otherNode.f);
            if (c == 'g')
                return Convert.ToInt32(this.g - otherNode.g);
            else
                return -1;

        }

        // -- Update the f value
        public void UpdateF() { f = h + g; }

        // ---- Getters and setter ----
        public int GetX() { return x; }
        public int GetY() { return y; }
        public int GetCost() { return cost; }
        public double GetG() { return g; }
        public double GetH() { return g; }
        public double GetF() { return g + h; }
        public Node GetParent() { return parent; }
        public bool IsWalkable() { return walkable; }

        public void SetCost(int cost) { this.cost = cost; }
        public void SetG(double g) { this.g = g; }
        public void SetH(double h) { this.h = h; }
        public void SetParent(Node parent) { this.parent = parent; }
        public void SetWalkable(bool walkable) { this.walkable = walkable; }

        // -- ToString 
        public override string ToString() { return "[ (" + x + "," + y + "), cost=" + cost + ", g=" + g + ", h=" + h + ", f=" + GetF() + " ]"; }

    }
}
