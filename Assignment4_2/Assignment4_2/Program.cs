using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int M = 5;      // Rows
            int N = 5;      // Columns
            int K = 2;      // Max eggs per row, column and diagonal


            Board b = new Board(M, N, K);
            Node start = new Node(b);
            SimulatedAnnealing.Run(start, M*K);

            //Console.WriteLine(b);
            //Console.WriteLine("Objective function: " + b.ObjectiveFunction());
               
            //Node n = new Node();
            //Console.WriteLine(n.ObjectiveFunctionValue);

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
