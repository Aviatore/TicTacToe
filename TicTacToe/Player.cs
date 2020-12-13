using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class LocationContainer
    {
        public List<Point> FreeCoords;
        public List<Point> Coords;
        public int Num {get; set; }

        
        public void AddFreeCoords(int row, int col)
        {
            Point location = new Point(row, col);
            FreeCoords.Add(location);
        }
        
        public void AddCoords(int row, int col)
        {
            Point location = new Point(row, col);
            Coords.Add(location);
        }

        public void ClearCoords()
        {
            Coords.Clear();
        }

        public void ClearFreeCoords()
        {
            FreeCoords.Clear();
        }

        public bool CoordsContains(int row, int col)
        {
            Point location = new Point(row, col);

            return Coords.Contains(location);
        }
        
        public bool FreeCoordsContains(int row, int col)
        {
            Point location = new Point(row, col);

            return FreeCoords.Contains(location);
        }
        public LocationContainer()
        {
            FreeCoords = new List<Point>();
            Coords = new List<Point>();
            Num = 0;
        }
    }
    
    public class Player
    {
        public delegate Point DGetMove();
        public string Name {get; set; }
        public int Mark { get; set; }
        public int Points { get; set; }
        
        public Colors Color { get; set; }
        public Species Species { get; set; }
        public int Score {get; set; }
        private Board _board;
        public DGetMove GetMove;
        private Random _random = new Random();
        
        
        public Player(Species species, string name, int mark, Board board, Colors color)
        {
            Species = species;
            Name = name;
            Mark = mark;
            _board = board;
            Score = 0;
            Color = color;

            if (Species == Species.Human)
                GetMove = HumanGetMove;
            else
                GetMove = AiGetMove;


            // Assigns a color to the player's mark
            board.AddColor(Mark, color);
        }
        
        public Point HumanGetMove()
        {
            bool loop = true;

            Point location = new Point();
            
            while (loop)
            {
                Console.Write("Please, give coordinates: ");
                string userInput = Console.ReadLine();

                if (userInput != null)
                {
                    if (userInput == "quit")
                    {
                        Console.WriteLine("Good bye!");
                        Environment.Exit(0);
                    }
                    else if (userInput.Length == 2)
                    {
                        int rowIndex = Array.IndexOf(_board.RowLabels, Char.ToUpper(userInput[0]));
                        int colIndex = Array.IndexOf(_board.ColLabels, Char.ToUpper(userInput[1]).ToString());
                        
                        if (rowIndex >= 0 && colIndex >= 0)
                            location = new Point(rowIndex, colIndex);
                        else
                            continue;
                    }
                }
                
                if (_board.GetBoardValue(location) == 0)
                    return location;
            }
            
            return location;
        }

        public Point AiGetMove()
        {
            LocationContainer bestMove = new LocationContainer();
            
            int[] marksToCheck = new int[2];
            marksToCheck[0] = Mark;
            marksToCheck[1] = Mark == 1 ? 2 : 1;

            foreach (int mark in marksToCheck)
            {
                LocationContainer rowData = new LocationContainer();
                
                for (int rowIndex = 0; rowIndex < _board.RowLength; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < _board.ColLength; colIndex++)
                    {
                        int lenToReact;
                        int boardValue = _board.GetValue(rowIndex, colIndex);
                        if (boardValue == mark)
                        {
                            if (rowData.CoordsContains(rowIndex, colIndex - 1) ||
                                rowData.FreeCoordsContains(rowIndex, colIndex - 1))
                            {
                                rowData.AddCoords(rowIndex, colIndex);
                            }
                            else
                            {
                                rowData.ClearCoords();
                                rowData.ClearFreeCoords();
                                rowData.AddCoords(rowIndex, colIndex);
                            }
                        }
                        else if (boardValue == 0 && !rowData.FreeCoordsContains(rowIndex, colIndex))
                        {
                            rowData.AddFreeCoords(rowIndex, colIndex);
                            rowData.Num++;
                        }
                        else if (boardValue == 0)
                        {
                            rowData.ClearCoords();
                            rowData.ClearFreeCoords();
                            rowData.AddFreeCoords(rowIndex, colIndex);
                            rowData.Num++;
                        }
                        else
                        {
                            rowData.ClearCoords();
                            rowData.ClearFreeCoords();
                        }

                        lenToReact = (_board.RowLength - _board.ItemsNumberToWin) >= 1 ? 2 : 1;

                        if (rowData.Coords.Count >= (_board.ItemsNumberToWin - lenToReact) &&
                            rowData.Num >= (_board.ItemsNumberToWin - rowData.Coords.Count))
                        {
                            if (rowData.FreeCoords.Count > 0)
                            {
                                Point location = rowData.FreeCoords[0];

                                if (rowData.Coords.Count > bestMove.Num)
                                {
                                    bestMove.AddFreeCoords(location.Row, location.Col);
                                    bestMove.Num = rowData.Coords.Count;
                                }
                            }
                        }
                    }

                    rowData.ClearCoords();
                    rowData.ClearFreeCoords();
                    rowData.Num = 0;
                }

                if (bestMove.Num == _board.ItemsNumberToWin - 1)
                    return bestMove.FreeCoords[0];
            }

            if (bestMove.Num > 0)
                return bestMove.FreeCoords[0];
            
            List<Point> freePlaces = _board.GetFreePlaces();
            Point randomLocation = freePlaces[_random.Next(freePlaces.Count)];
            return randomLocation;
            
        }

        public bool Turn()
        {
            Point newMove = GetMove();
            _board.Mark(this, newMove);

            if (_board.IsPlayerWon(this))
            {
                return false;
            }
            else if (_board.IsBoardFull())
            {
                //Console.WriteLine("Game over!");
                return false;
            }

            return true;
        }
    }
}