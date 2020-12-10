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
            board = new Board(row, col);
            
            Player1 = new Player(Species.Human, "John", 1, board);
            Player2 = new Player( Species.Human,"Mike", 2, board);
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
            
            while (loop)
            {
                Console.Clear();
                board.Print(Player1, Player2);
                Player1.Turn();

                Console.Clear();
                board.Print(Player1, Player2);
                Player2.Turn();
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