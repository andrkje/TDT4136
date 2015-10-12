using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    class Node
    {       
        public State State { get; set; }
        public double Value { get; set; }

        public Node(State state)
        {
            State = state; 
        }


        
        
    }
}
