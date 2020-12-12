using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TicTacToe
{
    public enum Colors
    {
        Blue = ConsoleColor.Cyan,
        Yellow = ConsoleColor.Yellow,
        Red = ConsoleColor.Red
    }

    public enum Species
    {
        Human,
        Computer
    }

    public class Game
    {
        public Player Player1;
        public Player Player2;
        
        private Board board;

        public Game(int row, int col)
        {
            board = new Board(row, col, 3);
            
            Player1 = new Player(Species.Human, "John", 1, board, Colors.Blue);
            Player2 = new Player( Species.Human,"Mike", 2, board, Colors.Red);
        }

        public void ShowBoard()
        {
            board.Print(Player1, Player2);

            Point output = Player1.GetMove();
            
            Console.WriteLine($"Output: {output.Row},{output.Col}");
        }

        public void GameLoop()
        {
            bool loop = true;
            
            Console.Clear();
            board.Print(Player1, Player2);
            
            while (loop)
            {
                loop = Player1.Turn();

                Console.Clear();
                board.Print(Player1, Player2);
                
                if (!loop)
                {
                    Console.WriteLine($"{Player1.Name} won the game!");
                    break;
                }
                
                
                loop = Player2.Turn();
                
                Console.Clear();
                board.Print(Player1, Player2);
                
                if (!loop)
                {
                    Console.WriteLine($"{Player2.Name} won the game!");
                    break;
                }
            };
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