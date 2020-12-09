using System;
using System.Collections.Generic;
using System.Drawing;

namespace TicTacToe
{
    public class Game
    {
        private Board board;
        
        public Game(int row, int col)
        {
            board = new Board(row, col);
        }

        public void ShowBoard()
        {
            //board.Print("John", "Mike", 2, 5);
            
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