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
            Console.WriteLine(b);
            Console.WriteLine("Objective function: " + b.ObjectiveFunction());
               



            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
