using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4_2
{
    class Node
    {
        public Board Board { get; set; }
        public double ObjectiveFunctionValue 
        {
            get { return Board.ObjectiveFunction(); } 
        }
        public List<Node> Neighbors { get; set; }       // !VIKTIG!! Legge til moves

        public Node(Board board)
        {
            Board = board;
        }

        //public void SetBoard(Board board) { return Board; }

        public override string ToString()
        {
            return Board.ToString();
        }

    }
}
