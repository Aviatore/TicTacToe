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

    public class Game
    {
        public Player Player1;
        public Player Player2;
        
        private Board board;
        
        public Game(int row, int col)
        {
            board = new Board(row, col);
            
            Player1 = new Player("Human", "John", 1, board);
            Player2 = new Player( "Human","Mike", 2, board);
        }

        public void ShowBoard()
        {
            board.Print(Player1, Player2);
            
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