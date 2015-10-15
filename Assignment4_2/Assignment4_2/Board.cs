using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4_2
{
    class Board
    {
        public int[][] EggCarton { get; set; }
        public int K { get; set; }
        public int M { get; set; }
        public int N { get; set; }

        public Board(int m, int n, int k)
        {
            M = m;
            N = n;
            K = k;

            EggCarton = GenerateBoard(M, N);
            /*
            EggCarton[0][0] = 1;
            EggCarton[0][2] = 1;
            EggCarton[1][1] = 1;
            EggCarton[1][4] = 1;
            EggCarton[2][1] = 1;
            EggCarton[2][3] = 1;
            EggCarton[3][0] = 1;
            EggCarton[3][4] = 1;
            EggCarton[4][2] = 1;
            EggCarton[4][3] = 1;
            */

            EggCarton[3][3] = 1;
            /*
            EggCarton[0][1] = 1;
            EggCarton[2][0] = 1;
            EggCarton[1][3] = 1;
            EggCarton[3][2] = 1;
             */
            
        }

        public int[][] GenerateBoard(int m, int n)
        { 
            int[][] board = new int[m][];

            for (int i = 0; i < board.Length; i++)
			{
                List<int> temp = new List<int>();
			    for (int j = 0; j < n; j++)
			    {
                    temp.Add(0);
			    }
                board[i] = temp.ToArray();
			}
            return board;
        }

        /*
        private PopulateBoard()
        {
        
        }
         */
        
        public double ObjectiveFunction()
        {
            double violatedRules = 0;
            
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (EggCarton[i][j] == 1)
                       violatedRules += CountViolations(i, j);                    
                }
            }
            // Max antall egg = M*K, gitt kvadratisk (M=N)
            //Console.WriteLine("violatet rules: " + violatedRules);
            return (CountEggs() / (Math.Pow((violatedRules+1), 2) ));     // +1 to avoid deviding by 0. !POW!! Skummel ting!
        }

        public bool IsValidBoard()
        {
            double violatedRules = 0;

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (EggCarton[i][j] == 1)
                        violatedRules += CountViolations(i, j);
                }
            }
            if (violatedRules > 0)
                return false;
            else
                return true;
        }

        public void GenerateRandomBoard()
        { 
            
        }

        public int CountEggs()
        {
            int count = 0;
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (EggCarton[i][j] == 1)
                        count++;
                }
            }
            return count;
        }

        private int CountViolations(int m, int n)
        {
            // Trekke K fra violations for å finne antall noder som er feilplasert

           // Console.WriteLine("________________");
            int violations = 0;
            // Vertical count
            int count = 0;
            for (int i = 0; i < M; i++)
            {
                if (EggCarton[i][n] == 1)
                    count++;
            }
            if (count > K)
                violations++;
            
            // Horizontal count
            count = 0;
            for (int i = 0; i < N; i++)
            {
                if (EggCarton[m][i] == 1)
                    count++;
            }
            if (count > K)
                violations++;
            
            // Diagonal right
            count = 0;
            int startRow = m;
            int startColumn = n;
            while ((startRow >= 0) && (startColumn+1 <= N))
            {
                //Console.WriteLine("row: " + startRow + ", col: " + startColumn);
                if (EggCarton[startRow][startColumn] == 1)
	            {
		            count++;
	            }
                startRow--;
                startColumn++;
            }
            startRow = m;
            startColumn = n;
            while ((startRow+1 <= M) && (startColumn >= 0))
	        {
                if (EggCarton[startRow][startColumn] == 1)
                {
                    count++;
                }
                startRow++;
                startColumn--;
	        }
            if (count-1 > K)
                violations++;

            // Diagonal left
            count = 0;
            startRow = m;
            startColumn = n;
            while ((startRow >= 0) && (startColumn >= 0))
            {
                //Console.WriteLine("row: " + startRow + ", col: " + startColumn);
                if (EggCarton[startRow][startColumn] == 1)
                {
                    count++;
                }
                startRow--;
                startColumn--;
            }
            startRow = m;
            startColumn = n;
            while ((startRow + 1 <= M) && (startColumn + 1 <= N))
            {
                if (EggCarton[startRow][startColumn] == 1)
                {
                    count++;
                }
                startRow++;
                startColumn++;
            }
            if (count - 1 > K)
                violations++;

            // Return
            return violations;
        }

        public override string ToString()
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
