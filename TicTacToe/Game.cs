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
            board = new Board(row, col, 4);
            
            Player1 = new Player(Species.Human, "John", 1, board, Colors.Blue);
            Player2 = new Player( Species.Computer,"Mike", 2, board, Colors.Red);
        }

        public void ShowBoard()
        {
            board.Print(Player1, Player2);

            Point output = Player1.GetMove();
            
            Console.WriteLine($"Output: {output.Row},{output.Col}");
        }

        public void GameLoop()
        {
            Menu menu = new Menu();
            
            Menu lvlA1 = new Menu("A1");
            Menu lvlA2 = new Menu("A2");
            
            Menu lvlB1 = new Menu("B1");
            Menu lvlB2 = new Menu("B2");

            lvlA1.AddElement(lvlB1, lvlB2);
            
            Menu lvlC1 = new Menu("C1");
            Menu lvlC2 = new Menu("C2");
            
            lvlA2.AddElement(lvlC1, lvlC2);
            
            Menu acc = new Menu("Change something");
            
            Menu acc2 = new Menu("Provide a number:");
            acc.SetCallBack((string s) =>
            {
                Console.WriteLine($"Congrats! Your message: {s}");
            });
            acc.AddElement(acc2);
            
            menu.AddElement(lvlA1, lvlA2, acc);
            
            menu.ShowMenu();
            
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