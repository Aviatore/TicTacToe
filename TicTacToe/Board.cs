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
        readonly char[] _rowsTemplate = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I'};
        private int[,] board;

        private readonly Dictionary<int, string> _marks = new Dictionary<int, string>()
        {
            {0, "."},
            {1, "X"},
            {2, "O"}
        };
        
        private readonly int _rowLength;
        private readonly int _colLength;
        private readonly int[] _cols;
        private readonly int _intemsNumberToWin;
        public readonly string[] ColLabels;
        public readonly char[] RowLabels;

        public Board(int rowLength, int colLength, int intemsNumberToWin)
        {
            _rowLength = rowLength;
            _colLength = colLength;
            _intemsNumberToWin = intemsNumberToWin;
            
            _cols = new int[_colLength];
            for (int i = 0; i < _colLength; i++)
                _cols[i] = i + 1;

            ColLabels = new string[_colLength];
            for (int i = 0; i < _colLength; i++)
                ColLabels[i] = _cols[i].ToString();

            RowLabels = new char[_rowLength];
            for (int i = 0; i < _rowLength; i++)
                RowLabels[i] = _rowsTemplate[i];
            
            board = new int[rowLength, colLength];

            for (int i = 0; i < rowLength; i++)
            for (int j = 0; j < colLength; j++)
                board[i, j] = 0;
        }

        public void Print(Player player1, Player player2)
        {
            string[] line = new string[_rowLength];
            for (int i = 0; i < _rowLength; i++)
                line[i] = "---";

            int boardTotalLength = (_rowLength * 3) + (_rowLength - 1) + 2;

            string scoreNames = $"{player1.Name} : {player2.Name}";
            string scoreValues = $"{player1.Score.ToString().PadLeft(player1.Name.Length)} : {player2.Score.ToString()}";
            int scoreOffset = ((boardTotalLength - scoreNames.Length) / 2);

            string spaceLeft = new String(' ', scoreOffset);
            Console.WriteLine($"{spaceLeft}{scoreNames}");
            Console.WriteLine($"{spaceLeft}{scoreValues}");
            Console.WriteLine(" ");
            Console.WriteLine($"   {String.Join("   ", ColLabels)}");

            for (int i = 0; i < _rowLength; i++)
            {
                int arrayValue;
                
                Console.Write($"{RowLabels[i]}  ");
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

        public bool IsBoardFull()
        {
            for (int i = 0; i < _rowLength; i++)
                for (int j = 0; j < _colLength; j++)
                    if (board[i, j] == 0)
                        return false;

            return true;
        }

        public void Mark(Player player, Point location)
        {
            board[location.Row, location.Col] = player.Mark;
        }

        public int GetBoardValue(Point location)
        {
            return board[location.Row, location.Col];
        }

        public bool IsPlayerWon(Player player)
        {
            return true;
        }
    }
}