using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TicTacToe
{
    public class GameMenu
    {
        public static Dictionary<GameMode, string> GameModeDict = new Dictionary<GameMode, string>()
        {
            {GameMode.ComputerComputer, "Computer vs. Computer"},
            {GameMode.HumanComputer, "Human vs. Computer"},
            {GameMode.HumanHuman, "Human vs. Human"}
        };
        
        public static Dictionary<int, GameMode> IntToGameModeDict = new Dictionary<int, GameMode>()
        {
            {1, GameMode.ComputerComputer},
            {2, GameMode.HumanComputer},
            {3, GameMode.HumanHuman}
        };
        
        public static void CreateMenu(Preferences preferences)
        {
            Menu menu = new Menu();

            Menu boardProperty = new Menu($"Board properties: {preferences.BoardSize}×{preferences.BoardSize}", "Board properties");
            Menu gameMode = new Menu($"Game mode: {GameModeDict[preferences.GameMode]}", "Game mode");
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
            boardSize.AddElement(boardSizeSet);
            
            Menu numberOfMarksToWin = new Menu($"Number of marks to win: {preferences.numberOfMarksToWin}", "");
            
            numberOfMarksToWin.SetCallBack((string s, Menu element) =>
            {
                if (int.TryParse(s, out preferences.numberOfMarksToWin))
                {
                    element.Title = $"Number of marks to win: {preferences.numberOfMarksToWin}";
                    return true;
                }

                return false;
            });
            
            Menu numberOfMarksToWinSet = new Menu("Numbers of marks to win (choose between 3-5)", "");
            numberOfMarksToWin.AddElement(numberOfMarksToWinSet);
            
            
            gameMode.SetCallBack((string s, Menu element) =>
            {
                int userInput;
                if (int.TryParse(s, out userInput) && userInput > 0 && userInput < 4)
                {
                    preferences.GameMode = IntToGameModeDict[userInput];
                    element.Title = $"Game mode: {GameModeDict[preferences.GameMode]}";
                    return true;
                }

                return false;
            });
            Menu gameModeSet = new Menu("Please, input a digit (1, 2 or 3) that corresponds to the chosen game mode\n" +
                                        "1. Computer vs. Computer\n" +
                                        "2. Human vs. Computer\n" +
                                        "3. Human vs. Human", "");
            gameMode.AddElement(gameModeSet);
            
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