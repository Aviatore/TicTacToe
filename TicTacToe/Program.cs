using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            Game game = new Game(5, 5);
            game.ShowBoard();
        }
    }
}