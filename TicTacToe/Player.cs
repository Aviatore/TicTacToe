using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class BestMoveContainer
    {
        public Point BestMove { get; set; }
        public int Length = 0;
    }
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
        private Ai _ai;
        
        
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
            {
                _ai = new Ai(Mark, _board);
                GetMove = _ai.AiGetMove;
            }


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