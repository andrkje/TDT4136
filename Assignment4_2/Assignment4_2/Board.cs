using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4_2
{
    class Board
    {
        int[][] board;
        int K, M, N;

        public Board(int m, int n, int k)
        {
            M = m;
            N = n;
            K = k;

            board = GenerateBoard(M, N);
            board[0][0] = 1;
            board[0][2] = 1;
            board[1][1] = 1;
            board[1][4] = 1;
            board[2][1] = 1;
            board[2][3] = 1;
            board[3][0] = 1;
            board[3][4] = 1;
            board[4][2] = 1;
            board[4][3] = 1;

            board[3][3] = 1;
            board[3][2] = 1;
        }

        private int[][] GenerateBoard(int m, int n)
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
                    if (board[i][j] == 1)
                       violatedRules += CountViolations(i, j);                    
                }
            }
            return (CountEggs() / (Math.Pow((violatedRules+1), 2) ));     // +1 to avoid deviding by 0. !POW!! Skummel ting!
        }

        private int CountEggs()
        {
            int count = 0;
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (board[i][j] == 1)
                        count++;
                }
            }
            return count;
        }

        private int CountViolations(int m, int n)
        {
            // Trekke K fra violations for å finne antall noder som er feilplasert

            Console.WriteLine("________________");
            int violations = 0;
            // Vertical count
            int count = 0;
            for (int i = 0; i < M; i++)
            {
                if (board[i][n] == 1)
                    count++;
            }
            if (count > K)
                violations++;
            
            // Horizontal count
            count = 0;
            for (int i = 0; i < N; i++)
            {
                if (board[m][i] == 1)
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
                Console.WriteLine("row: " + startRow + ", col: " + startColumn);
                if (board[startRow][startColumn] == 1)
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
                if (board[startRow][startColumn] == 1)
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
                Console.WriteLine("row: " + startRow + ", col: " + startColumn);
                if (board[startRow][startColumn] == 1)
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
                if (board[startRow][startColumn] == 1)
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
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    s += "[" + board[i][j] + "]";
                }
                s += "\n";
            }
            return s;
        }
    }
}
