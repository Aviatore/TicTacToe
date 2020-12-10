using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            Game game = new Game(3, 3);
            //game.TestGetFreePlaces();
            game.ShowBoard();
        }
    }
}