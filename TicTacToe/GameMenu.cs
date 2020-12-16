using System;
using System.Runtime.CompilerServices;

namespace TicTacToe
{
    public class GameMenu
    {
        public static void CreateMenu(Preferences preferences)
        {
            Menu menu = new Menu();

            Menu boardProperty = new Menu($"Board properties: {preferences.BoardSize}×{preferences.BoardSize}", "Board properties");
            Menu gameMode = new Menu("Game mode", "Game mode");
            Menu nextStep = new Menu("Next step", "Final settings");

            Menu boardSize = new Menu($"Board size: {preferences.BoardSize}×{preferences.BoardSize}", "");
            boardSize.SetCallBack((string s, Menu element) =>
            {
                if (int.TryParse(s, out preferences.BoardSize))
                {
                    element.Title = $"Board properties: {preferences.BoardSize}×{preferences.BoardSize}";
                    return true;
                }

                return false;
            });
            Menu boardSizeSet = new Menu("Please, input a single digit from 6 to 9.", "");
            
            Menu numberOfMarksToWin = new Menu($"Number of marks to win: {preferences.numberOfMarksToWin}", "");

            boardSize.AddElement(boardSizeSet);
            boardProperty.AddElement(boardSize, numberOfMarksToWin);

            Menu lvlC1 = new Menu("C1", "C1 label");
            Menu lvlC2 = new Menu("C2", "C2 label");

            //lvlA2.AddElement(lvlC1, lvlC2);

            Menu acc = new Menu("Change something", "Change label");

            //Menu acc2 = new Menu("Provide a number:", "ppp");
            //acc.SetCallBack((string s) => { Console.WriteLine($"Congrats! Your message: {s}"); });
            //acc.AddElement(acc2);

            menu.AddElement(boardProperty, gameMode, nextStep);

            menu.ShowMenu();
        }
    }
}