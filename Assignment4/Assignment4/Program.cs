using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            SimulatedAnnealing.Run(new Problem(new State()), new Schedule());


            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

    
    }
}
