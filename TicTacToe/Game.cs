using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TicTacToe
{
    public enum Colors
    {
        Blue = ConsoleColor.Blue,
        Yellow = ConsoleColor.Yellow,
        Red = ConsoleColor.Red
    }
    public struct Player
    {
        public string Name {get; set; }
        public int Mark { get; set; }
        public int Points { get; set; }
        public Colors Color { get; set; }
    }
    public class Game
    {
        public Player player1 = new Player();
        public Player player2 = new Player();
        
        private Board board;
        
        public Game(int row, int col)
        {
            board = new Board(row, col);
        }

        public void ShowBoard()
        {
            board.Print("John", "Mike", 2, 5);
            
        }

        public Point GetMove()
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
                        int rowIndex = Array.IndexOf(board.RowLabels, Char.ToUpper(userInput[0]));
                        int colIndex = Array.IndexOf(board.ColLabels, Char.ToUpper(userInput[1]).ToString());
                        
                        if (rowIndex >= 0 && colIndex >= 0)
                            location = new Point(rowIndex, colIndex);
                        else
                            continue;
                    }
                }
                
                if (board.GetBoardValue(location) == 0)
                    return location;
            }
            
            return location;
        }

        /// <summary>
        /// Test function that prints all free places within the board
        /// </summary>
        public void TestGetFreePlaces()
        {
            int row = 0;
            List<Point> freePlaces = board.GetFreePlaces();
            
            foreach (Point pair in freePlaces)
            {
                
                Console.WriteLine($"Row {row}: {pair.Row},{pair.Col}");
                
            }
        }
    }
}