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

    public enum GameMode
    {
        HumanHuman,
        HumanComputer,
        ComputerComputer
    }
    
    public struct Preferences
    {
        public int BoardSize;
        public GameMode GameMode;
        public PlayerData Player1;
        public PlayerData Player2;
        public int numberOfMarksToWin;
    }

    public struct PlayerData
    {
        public string Name;
        public Species Species;
        public Colors Color;
    }

    public class Game
    {
        public Player Player1;
        public Player Player2;
        
        private Board board;

        public Game(int row, int col, int numberToWin)
        {
            Preferences preferences = new Preferences();
            preferences.BoardSize = 9;
            preferences.GameMode = GameMode.HumanComputer;
            preferences.numberOfMarksToWin = 5;
            preferences.Player1 = new PlayerData()
            {
                Color = Colors.Blue,
                Name = "Player1",
                Species = Species.Human
            };
            preferences.Player2 = new PlayerData()
            {
                Color = Colors.Red,
                Name = "Player2",
                Species = Species.Computer
            };
            
            GameMenu.CreateMenu(preferences);
            
            board = new Board(preferences.BoardSize, preferences.BoardSize, preferences.numberOfMarksToWin);
            
            Player1 = new Player(preferences.Player1.Species, preferences.Player1.Name, 1, board, preferences.Player1.Color);
            Player2 = new Player(preferences.Player2.Species, preferences.Player2.Name, 2, board, preferences.Player2.Color);
            //Player1 = new Player(Species.Human, "John", 1, board, Colors.Blue);
            //Player2 = new Player( Species.Computer,"Mike", 2, board, Colors.Red);
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