using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace TicTacToe
{
    public struct Point
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Point(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
    public class Board
    {
        private int[,] board;

        private readonly Dictionary<int, string> _marks = new Dictionary<int, string>()
        {
            {0, "."},
            {1, "X"},
            {2, "O"}
        };

        

        private readonly int _rowLength;
        private readonly int _colLength;
        
        public Board(int rowLength, int colLength)
        {
            _rowLength = rowLength;
            _colLength = colLength;
            
            board = new int[rowLength, colLength];

            for (int i = 0; i < rowLength; i++)
            for (int j = 0; j < colLength; j++)
                board[i, j] = 0;
        }

        public void Print(string player1, string player2, int score1, int score2)
        {
            char[] rowsTemplate = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I'};
            
            int[] cols = new int[_colLength];
            for (int i = 0; i < _colLength; i++)
                cols[i] = i + 1;

            string[] colLabels = new string[_colLength];
            for (int i = 0; i < _colLength; i++)
                colLabels[i] = cols[i].ToString();

            char[] rowLabels = new char[_rowLength];
            for (int i = 0; i < _rowLength; i++)
                rowLabels[i] = rowsTemplate[i];

            string[] line = new string[_rowLength];
            for (int i = 0; i < _rowLength; i++)
                line[i] = "---";

            int boardTotalLength = (_rowLength * 3) + (_rowLength - 1) + 2;

            string scoreNames = $"{player1} : {player2}";
            string scoreValues = $"{score1.ToString().PadLeft(player1.Length)} : {score2.ToString()}";
            int scoreOffset = ((boardTotalLength - scoreNames.Length) / 2);

            string spaceLeft = new String(' ', scoreOffset);
            Console.WriteLine($"{spaceLeft}{scoreNames}");
            Console.WriteLine($"{spaceLeft}{scoreValues}");
            Console.WriteLine(" ");
            Console.WriteLine($"   {String.Join("   ", colLabels)}");

            for (int i = 0; i < _rowLength; i++)
            {
                int arrayValue;
                
                Console.Write($"{rowLabels[i]}  ");
                for (int j = 0; j < _colLength - 1; j++)
                {
                    arrayValue = board[i, j];
                    Console.Write($"{_marks[arrayValue]} | ");
                }
                
                arrayValue = board[i, board.GetUpperBound(0)];
                Console.WriteLine(_marks[arrayValue]);
                
                if (i < _rowLength - 1)
                    Console.WriteLine($"  {String.Join("+", line)}");
            }
        }

        public List<Point> GetFreePlaces()
        {
            //int [][] freePlaces = new int[_rowLength][];
            List<Point> freePlaces = new List<Point>();

            for (int i = 0; i < _rowLength; i++)
            {
                for (int j = 0; j < _colLength; j++)
                {
                    if (board[i, j] == 0)
                    {
                        Point rowIndexPair = new Point(i, j);
                        freePlaces.Add(rowIndexPair);
                    }
                }
            }

            return freePlaces;
        }

        public void Mark(Player player, Point location)
        {
            board[location.Row, location.Col] = player.Mark;
        }
    }
}