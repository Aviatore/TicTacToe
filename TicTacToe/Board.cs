using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Point> WinnerLocation = null;
        readonly char[] _colsTemplate = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private int[,] _board;

        public readonly Dictionary<int, char> marks = new Dictionary<int, char>()
        {
            {0, '.'},
            {1, 'X'},
            {2, 'O'}
        };
        
        private Dictionary<char, ConsoleColor> _markColors = new Dictionary<char, ConsoleColor>()
        {
            {'.', ConsoleColor.Gray},
            {'X', ConsoleColor.Gray},
            {'O', ConsoleColor.Gray}
        };

        public void AddColor(int character, Colors color)
        {
            _markColors[marks[character]] = (ConsoleColor)color;
        }
        
        public readonly int RowLength;
        public readonly int ColLength;
        private readonly int[] _rows;
        public readonly int ItemsNumberToWin;
        public readonly string[] RowLabels;
        public readonly char[] ColLabels;
        //public List<Point> WinnerLocation;

        public Board(int rowLength, int colLength, int itemsNumberToWin)
        {
            RowLength = rowLength;
            ColLength = colLength;
            ItemsNumberToWin = itemsNumberToWin;
            
            _rows = new int[RowLength];
            for (int i = 0; i < RowLength; i++)
                _rows[i] = i + 1;

            RowLabels = new string[RowLength];
            for (int i = 0; i < RowLength; i++)
                RowLabels[i] = _rows[i].ToString();

            ColLabels = new char[ColLength];
            for (int i = 0; i < ColLength; i++)
                ColLabels[i] = _colsTemplate[i];

            foreach (char val in ColLabels)
            {
                Console.WriteLine(val);
            }
            
            _board = new int[rowLength, colLength];

            ClearBoard();
        }

        public void ClearBoard()
        {
            for (int i = 0; i < RowLength; i++)
            for (int j = 0; j < ColLength; j++)
                _board[i, j] = 0;

            WinnerLocation = null;
        }

        public void Print(Player player1, Player player2)
        {
            string[] line = new string[RowLength];
            for (int i = 0; i < RowLength; i++)
                line[i] = "---";

            int boardTotalLength = (RowLength * 3) + (RowLength - 1) + 2;

            string scoreNames = $"{player1.Name} : {player2.Name}";
            string scoreValues = $"{player1.Points.ToString().PadLeft(player1.Name.Length)} : {player2.Points.ToString()}";
            int scoreOffset = ((boardTotalLength - scoreNames.Length) / 2) > 0 ? ((boardTotalLength - scoreNames.Length) / 2) : 0;

            string spaceLeft = new String(' ', scoreOffset);
            Console.WriteLine($"{spaceLeft}{scoreNames}");
            Console.WriteLine($"{spaceLeft}{scoreValues}");
            Console.WriteLine(" ");
            //Console.WriteLine($"   {String.Join("   ", ColLabels)}");
            Console.WriteLine($"   {String.Join("   ", ColLabels)}");

            for (int i = 0; i < RowLength; i++)
            {
                //Console.Write($"{RowLabels[i]}  ");
                Console.Write($"   ");
                for (int j = 0; j < ColLength - 1; j++)
                {
                    PrintColor(i, j);

                    Console.Write(" | ");
                    //Console.Write($"{_marks[arrayValue]} | ");
                }
                
                PrintColor(i, ColLength - 1);
                
                Console.WriteLine($"  {RowLabels[i]}");
                
                if (i < RowLength - 1)
                    Console.WriteLine($"  {String.Join("+", line)}");
            }
        }

        public List<Point> GetFreePlaces()
        {
            //int [][] freePlaces = new int[_rowLength][];
            List<Point> freePlaces = new List<Point>();

            for (int i = 0; i < RowLength; i++)
            {
                for (int j = 0; j < ColLength; j++)
                {
                    if (_board[i, j] == 0)
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
            for (int i = 0; i < RowLength; i++)
                for (int j = 0; j < ColLength; j++)
                    if (_board[i, j] == 0)
                        return false;

            return true;
        }

        public void Mark(Player player, Point location)
        {
            _board[location.Row, location.Col] = player.Mark;
        }

        public int GetBoardValue(Point location)
        {
            return _board[location.Row, location.Col];
        }

        public bool IsPlayerWon(Player player)
        {
            for (int rowIndex = 0; rowIndex < RowLength; rowIndex++)
            {
                for (int colIndex = 0; colIndex < ColLength; colIndex++)
                {
                    Point location = new Point(rowIndex, colIndex);

                    (bool IsFive, List<Point> Locations) five;
                    
                    five = GetFiveInRow(location, player);
                    if (five.IsFive)
                    {
                        WinnerLocation = five.Locations;
                        return true;
                    }
                    
                    five = GetFiveInCol(location, player);
                    if (five.IsFive)
                    {
                        WinnerLocation = five.Locations;
                        return true;
                    }
                    
                    five = GetFiveInDiagonal(location, player);
                    if (five.IsFive)
                    {
                        WinnerLocation = five.Locations;
                        return true;
                    }
                }
            }
            return false;
        }

        private (bool, List<Point>) GetFiveInRow(Point location, Player player)
        {
            (bool IsFive, List<Point> Locations) output = (false, new List<Point>());
            
            // Check if the required number of marks (to win) can be put to the right from the current location
            if (RightPossible(location))
            {

                for (int i = location.Col; i < ColLength; i++)
                {
                    if (_board[location.Row, i] == player.Mark)
                    {
                        Point newPoint = new Point(location.Row, i);
                        output.Locations.Add(newPoint);
                    }
                    else
                    {
                        output.Locations.Clear();
                    }

                    if (output.Locations.Count >= ItemsNumberToWin)
                    {
                        output.IsFive = true;
                        return output;
                    }
                }
            }
            
            output.IsFive = false;

            return output;
        }

        public int UpperBound(int dimension)
        {
            return _board.GetUpperBound(dimension);
        }
        
        private (bool, List<Point>) GetFiveInCol(Point location, Player player)
        {
            (bool IsFive, List<Point> Locations) output = (false, new List<Point>());
            
            // Check if the required number of marks (to win) can be put down from the current location
            if (DownPossible(location))
            {

                for (int i = location.Row; i < RowLength; i++)
                {
                    if (_board[i, location.Col] == player.Mark)
                    {
                        Point newPoint = new Point(i, location.Col);
                        output.Locations.Add(newPoint);
                    }
                    else
                    {
                        output.Locations.Clear();
                    }

                    if (output.Locations.Count >= ItemsNumberToWin)
                    {
                        output.IsFive = true;
                        return output;
                    }
                }
            }
            
            output.IsFive = false;

            return output;
        }
        
        private (bool, List<Point>) GetFiveInDiagonal(Point location, Player player)
        {
            (bool IsFive, List<Point> Locations) output = (false, new List<Point>());
            int[] boardDimensions = new int[] {RowLength, ColLength};
            int indexMin = boardDimensions.Min();
            int rowIndex = location.Row;
            int colIndex = location.Col;
            
            // Check if the required number of marks (to win) can be put down-right || down-left from the current location
            if (RightPossible(location) && DownPossible(location))
            {
                for (int i = 0; i < indexMin; i++)
                {
                    // Check if the calculated indexes are valid for the board array
                    if (_board.GetUpperBound(0) >= rowIndex && _board.GetUpperBound(1) >= colIndex)
                    {
                        if (_board[rowIndex, colIndex] == player.Mark)
                        {
                            Point newPoint = new Point(rowIndex, colIndex);
                            output.Locations.Add(newPoint);
                        }
                        else
                        {
                            output.Locations.Clear();
                        }
                    }

                    if (output.Locations.Count >= ItemsNumberToWin)
                    {
                        output.IsFive = true;
                        return output;
                    }

                    rowIndex++;
                    colIndex++;
                }
            }
            
            rowIndex = location.Row;
            colIndex = location.Col;
            if (LeftPossible(location) && DownPossible(location))
            {
                for (int i = 0; i < indexMin; i++)
                {
                    // Check if the calculated indexes are valid for the board array
                    if (_board.GetUpperBound(0) >= rowIndex && _board.GetLowerBound(1) <= colIndex)
                    {
                        if (_board[rowIndex, colIndex] == player.Mark)
                        {
                            Point newPoint = new Point(rowIndex, colIndex);
                            output.Locations.Add(newPoint);
                        }
                        else
                        {
                            output.Locations.Clear();
                        }
                    }
                    
                    if (output.Locations.Count >= ItemsNumberToWin)
                    {
                        output.IsFive = true;
                        return output;
                    }

                    rowIndex++;
                    colIndex--;
                }
            }
            
            output.IsFive = false;

            return output;
        }

        public bool RightPossible(Point location)
        {
            if ((ColLength - location.Col) >= ItemsNumberToWin)
                return true;
            return false;
        }

        public bool LeftPossible(Point location)
        {
            if ((location.Col + 1) >= ItemsNumberToWin)
                return true;
            return false;
        }

        public bool DownPossible(Point location)
        {
            if ((RowLength - location.Row) >= ItemsNumberToWin)
                return true;
            return false;
        }

        private void PrintColor(int row, int col)
        {
            int arrayValue;
            arrayValue = _board[row, col];
            Point location = new Point(row, col);

            if (WinnerLocation != null && WinnerLocation.Contains(location))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(marks[arrayValue]);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = _markColors[marks[arrayValue]];
                Console.Write(marks[arrayValue]);
                Console.ResetColor();
            }
        }

        public int GetValue(int row, int col)
        {
            return _board[row, col];
        }
    }
}