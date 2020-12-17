using System;
using System.Collections;

namespace TicTacToe
{
    
    class Program
    {
        static void Main(string[] args)
        {
           //Game game = new Game(15, 15, 5);
           
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
            //game.TestGetFreePlaces();
            //game.GameLoop();
        }
    }
}