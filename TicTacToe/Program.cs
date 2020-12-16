using System;
using System.Collections;

namespace TicTacToe
{
    
    class Program
    {
        static void Main(string[] args)
        {
           Game game = new Game(15, 15, 5);
            //game.TestGetFreePlaces();
            game.GameLoop();
        }
    }
}