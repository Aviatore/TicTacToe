using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace TicTacToe
{
    public class Board
    {
        private int[,] board;

        private Dictionary<int, string> Marks = new Dictionary<int, string>()
        {
            {0, "."},
            {1, "X"},
            {2, "O"}
        };

        private int _row;
        private int _col;
        
        public Board(int row, int col)
        {
            _row = row;
            _col = col;
            
            board = new int[row, col];

            for (int i = 0; i < row; i++)
            for (int j = 0; j < col; j++)
                board[i, j] = 0;
        }

        public void Print(string player1, string player2, int score1, int score2)
        {
            char[] rowsTemplate = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I'};
            
            int[] cols = new int[_col];
            for (int i = 0; i < _col; i++)
                cols[i] = i + 1;

            string[] colsString = new string[_col];
            for (int i = 0; i < _col; i++)
                colsString[i] = cols[i].ToString();

            char[] rows = new char[_row];
            for (int i = 0; i < _row; i++)
                rows[i] = rowsTemplate[i];

            string[] line = new string[_row];
            for (int i = 0; i < _row; i++)
                line[i] = "---";

            int boardTotalLength = (_row * 3) + (_row - 1) + 2;

            string scoreNames = $"{player1} : {player2}";
            string score = $"{score1.ToString().PadLeft(player1.Length)} : {score2.ToString()}";
            int scoreOffset = (int)((boardTotalLength - scoreNames.Length) / 2);

            string spaceLeft = new String(' ', scoreOffset);
            Console.WriteLine($"{spaceLeft}{scoreNames}");
            Console.WriteLine($"{spaceLeft}{score}");
            Console.WriteLine(" ");
            Console.WriteLine($"   {String.Join("   ", colsString)}");

            for (int i = 0; i < _row; i++)
            {
                int arrayValue;
                
                Console.Write($"{rows[i]}  ");
                for (int j = 0; j < _col - 1; j++)
                {
                    arrayValue = board[i, j];
                    Console.Write($"{Marks[arrayValue]} | ");
                }
                
                arrayValue = board[i, board.GetUpperBound(0)];
                Console.WriteLine(Marks[arrayValue]);
                
                if (i < _row - 1)
                    Console.WriteLine($"  {String.Join("+", line)}");
            }
        }
    }
}