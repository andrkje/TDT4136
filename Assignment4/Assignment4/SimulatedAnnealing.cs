using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment4
{
    class SimulatedAnnealing
    {
        //private static int m, n, k;

        public static State RunExercise(State startingPoint)
        {
            double T


            return new State();  // TEMP
        }


        public static State RunBook(Problem problem, Schedule schedule)
        {
            /* Currently not in use, might be moved to the Problem class..?
            m = 0;
            n = 0;
            k = 0;
            */

            Node current = new Node(problem.InitialState);      // Set current node to the starting node

            int t = 0;      
            while(true)         // for 1 to infinity
            {
                double T = schedule.T;
                t++;

                if (Math.Abs(T) < 0.001)        // Check if T = 0,
                    Console.WriteLine("Solution was found!");   // TEMP, output to see whether a solution was found or not

                List<Node> children = GenerateChildren(current, problem);       // Generates a list of childrem
                Random rnd = new Random();
                Node next = children.ElementAt(rnd.Next(children.Count));       // A random child is selected

                double deltaE = next.Value - current.Value;         // ΔE is calulated

                if (deltaE > 0) 
                {
                    current = next;
                }
                else
                {
                    if (IsAcceptedBasedOnProbability(deltaE, T))    // current = next with probability of e^(ΔE/T)
                        current = next;
                }
                 

                //Thread.Sleep(10);
                //Console.WriteLine("random number: " + IsAcceptedBasedOnProbability(-rnd.NextDouble(), rnd.NextDouble()) + "\n\n");

  
                if (t > 100)    // TEMP to prevent infinate loop
                    break;
            }

            return new State(); // TEMP, empty state

        }


        private static bool IsAcceptedBasedOnProbability(double deltaE, double T)
        {
            Random rnd = new Random();
            double randomNumber = rnd.NextDouble();
            double probability = Math.Pow(Math.E, (deltaE / T));
            //Console.WriteLine("Current = " + randomNumber +"P = " +probability);
            
            return randomNumber >= probability ;
        }


       private static List<Node> GenerateChildren(Node current, Problem problem) 
       {
           List<Node> children = new List<Node>();

           // Must be generated!

           children.Add(new Node(new State()));     // TEMP, testing
           children.Add(new Node(new State()));     // TEMP, testing
           children.Add(new Node(new State()));     // TEMP, testing
           children.Add(new Node(new State()));     // TEMP, testing
           children.Add(new Node(new State()));     // TEMP, testing

           return children;
       }


    }
}
