using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class Player
    {
        public delegate Point DGetMove();
        public string Name {get; set; }
        public int Mark { get; set; }
        public int Points { get; set; }
        public List<Point> WinnerLocation = null;
        public Colors Color { get; set; }
        public Species Species { get; set; }
        public int Score {get; set; }
        private Board _board;
        public DGetMove GetMove;
        
        public Player(Species species, string name, int mark, Board board)
        {
            Species = species;
            Name = name;
            Mark = mark;
            _board = board;
            Score = 0;

            if (Species == Species.Human)
            {
                GetMove = HumanGetMove;
            }
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