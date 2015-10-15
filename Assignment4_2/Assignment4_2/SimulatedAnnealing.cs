using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4_2
{
    class SimulatedAnnealing
    {
        private static int NEIGHBOR_AMOUNT = 10;

        // !VIKTIG!! Returnere liste med "moves"
        public static void Run(Node start, int fMax) 
        {
            Random rnd = new Random();
            double q = 0;
            double p = 0;
            //1.Begin at a start point P (either user-selected or randomly-generated).
            Node currentNode = start;
            Node bestNode = currentNode;

            GenerateNeghbors(currentNode);
            //2.Set the temperature, T, to its starting value: Tmax
            double t = 100000;
            double dT = 0.5;
            //3. Evaluate P with an objective function, F. This yields the value F(P).
            //double score = startPoint.ObjectiveFunctionValue;
            //4. If F(P) ≥ Ftarget then EXIT and return P as the solution; else continue.
            
            Console.WriteLine("Current node Objective: " + currentNode.ObjectiveFunctionValue );
            Console.WriteLine("fMax : " + fMax);

            while(!(currentNode.ObjectiveFunctionValue >= fMax) && (t > 0))
            {
                //5. Generate n neighbors of P in the search space: (P1, P2, ..., Pn).
                List<Node> neighbors = GenerateNeghbors(currentNode);

                //6. Evaluate each neighbor, yielding (F(P1), F(P2), ..., F(Pn)).
                //7. Let Pmax be the neighbor with the highest evaluation.
                Node bestNeighbor = neighbors.ElementAt(0);
                foreach (Node neigbhor in neighbors)
                {
                    if (neigbhor.ObjectiveFunctionValue > bestNeighbor.ObjectiveFunctionValue)
                    {
                        
                        bestNeighbor = neigbhor;
                    }
                }
                /* VIKTIG!!!
                if (bestNeighbor.ObjectiveFunctionValue > bestNode.ObjectiveFunctionValue)
                {
                    bestNode.ObjectiveFunctionValue = bestNeighbor.ObjectiveFunctionValue;
                    
                }
                 */



                //8. Let q = (F(Pmax )−F(P)) / F(P)
                q = (bestNeighbor.ObjectiveFunctionValue - currentNode.ObjectiveFunctionValue) / (currentNode.ObjectiveFunctionValue);

                //9. Let p = min h 1, e−qTi
                p = Min(1, Math.Pow(Math.E, ((-q)/t)));

                //10. Generate x, a random real number in the closed range [0, 1].
                double x = rnd.NextDouble();
                currentNode = bestNeighbor;
                //11. If x > p then P ← Pmax ;; ( Exploiting )
               
                if (x > p)
                {
                    currentNode = bestNeighbor;
                }
                //12. else P ← a random choice among the n neighbors. ;; (Exploring)
                else
                {
                    currentNode = neighbors.ElementAt(rnd.Next(neighbors.Count()));
                }
                
                //13. T ← T − dT
                t -= dT;

                //Console.WriteLine(currentNode);
            //14. GOTO Step 4
            }
            Console.WriteLine(currentNode + "\nNumber of eggs: " + currentNode.Board.CountEggs());

        }

        private static double Min(double d1, double d2)
        {
            if (d1 < d2)
                return d1;
            return d2;
        }

        private static List<Node> GenerateNeghbors(Node node)
        {
            Random rnd = new Random();
            List<Node> neighbors = new List<Node>();

            int[][] board = node.Board.EggCarton;

            int[] validMoves = { -1, 0, 1 };
            int counterNeighbors = 0;

            while (counterNeighbors <= NEIGHBOR_AMOUNT)
            {
                int[][] tempBoard = new int[board.Length][];        // Copy of the board that is beeing changed
                //Array.Copy(board, 0, tempBoard, 0, board.Length);

                for (int i = 0; i < board.Length; i++)
                {
                    List<int> temp = new List<int>();
                    for (int j = 0; j < node.Board.N; j++)
                    {
                        temp.Add(node.Board.EggCarton[i][j]);
                    }
                    tempBoard[i] = temp.ToArray();
                }



                int randomM = rnd.Next(3);      // Movement row
                int randomN = rnd.Next(3);      // Movement column
                #region
                //int randomNumberOfEggsToSkip = rnd.Next(node.Board.CountEggs() - 1);  
                //int count = 0;       // Number of eggs we have skipped
                //Console.WriteLine(randomM + "  ______________sdfsdfsdf_______________  " + randomN);
                /*
                for (int m = 0; m < node.Board.M; m++)
                {
                    for (int n = 0; n < node.Board.N; n++)
                    {
                        
                        if (board[m][n] == 1)
                        {
                            if (((m + validMoves[randomM]) >= 0 && (m + validMoves[randomM]) < node.Board.M) &&
                                ((n + validMoves[randomN]) >= 0 && (n + validMoves[randomN]) < node.Board.N))
                            {
                                
                                tempBoard[m + validMoves[randomM]][n + validMoves[randomN]] = 1;
                                if (!(randomM == 1 && randomN == 1))
                                    tempBoard[m][n] = 0;
                                int randInsertM = rnd.Next(node.Board.M);
                                int randInsertN = rnd.Next(node.Board.N);
                                //tempBoard[randInsertM][randInsertN] = 1;
                            }
                            
                        }
                        Console.WriteLine(DebugPrint(tempBoard));
                    }
                }
                 */
                #endregion
                int randInsertM1 = rnd.Next(node.Board.M);
                int randInsertN1 = rnd.Next(node.Board.N);
                int randInsertM2 = rnd.Next(node.Board.M);
                int randInsertN2 = rnd.Next(node.Board.N);

                int randSelect = rnd.Next(2);

                //if (randSelect == 1)
                    tempBoard[randInsertM1][randInsertN1] = 1;
                //else
                    tempBoard[randInsertM2][randInsertN2] = 0;
                
                //Console.WriteLine(DebugPrint(tempBoard));
                Board b = new Board(node.Board.M, node.Board.N, node.Board.K);
                b.EggCarton = tempBoard;

                if (b.IsValidBoard())
                {
                    neighbors.Add(new Node((b)));
                    counterNeighbors++;
                }
                

            }
            return neighbors;
        }

        public static string DebugPrint(int[][] EggCarton)
        {
            string s = "";
            for (int i = 0; i < EggCarton.Length; i++)
            {
                for (int j = 0; j < EggCarton[i].Length; j++)
                {
                    s += "[" + EggCarton[i][j] + "]";
                }
                s += "\n";
            }
            return s;
        }

    }
}
