using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

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
        
        public static Dictionary<int, Colors> IntToColorDict = new Dictionary<int, Colors>()
        {
            {1, Colors.Blue},
            {2, Colors.Yellow},
            {3, Colors.Red},
            {4, Colors.Gray}
        };
        
        public static Dictionary<Colors, int> ColorToIntDict = new Dictionary<Colors, int>()
        {
            {Colors.Blue, 1},
            {Colors.Yellow, 2},
            {Colors.Red, 3},
            {Colors.Gray, 4}
        };
        
        public static void CreateMenu(Preferences preferences)
        {
            Menu menu = new Menu(preferences);

            Menu boardProperty = new Menu($"Board properties: {preferences.BoardSize}×{preferences.BoardSize}", "Board properties", preferences);
            Menu gameMode = new Menu($"Game mode: {GameModeDict[preferences.GameMode]}", "Game mode", preferences);
            Menu nextStep = new Menu("Next step", "Final settings", preferences);

            Menu boardSize = new Menu($"Board size: {preferences.BoardSize}×{preferences.BoardSize}", "Board size", preferences);
            boardSize.SetCallBack((string s, Menu element) =>
            {
                int boardSizeTmp;
                if (int.TryParse(s, out boardSizeTmp) && boardSizeTmp >= 6 && boardSizeTmp <= 19)
                {
                    preferences.BoardSize = boardSizeTmp;
                    element.Title = $"Board size: {preferences.BoardSize}×{preferences.BoardSize}";
                    element.BackReference.Title = $"Board properties: {preferences.BoardSize}×{preferences.BoardSize}";
                    return true;
                }

                return false;
            });
            Menu boardSizeSet = new Menu("Please, input a single digit from 6 to 19.", "", preferences);
            boardSize.AddElement(boardSizeSet);
            
            Menu numberOfMarksToWin = new Menu($"Number of marks to win: {preferences.numberOfMarksToWin}", "Number of marks to win", preferences);
            
            numberOfMarksToWin.SetCallBack((string s, Menu element) =>
            {
                int numberOfMarksToWinTmp;
                if (int.TryParse(s, out numberOfMarksToWinTmp) && numberOfMarksToWinTmp >= 5)
                {
                    preferences.numberOfMarksToWin = numberOfMarksToWinTmp;
                    element.Title = $"Number of marks to win: {preferences.numberOfMarksToWin}";
                    return true;
                }

                return false;
            });
            
            Menu numberOfMarksToWinSet = new Menu("Numbers of marks to win (choose between 3-5)", "", preferences);
            numberOfMarksToWin.AddElement(numberOfMarksToWinSet);
            boardProperty.AddElement(boardSize, numberOfMarksToWin);
            
            Menu player1Settings = new Menu($"Player settings: Player <{ColorToIntDict[preferences.Player1.Color]}>X</>", "Player settings", preferences); 
            Menu player2Settings = new Menu($"Computer settings: Computer <{ColorToIntDict[preferences.Player2.Color]}>O</>", "Computer settings", preferences);
            
            Menu player1Name = new Menu($"Player's name: {preferences.Player1.Name}", "Player's name", preferences);
            Menu player2Name = new Menu($"Computer's name: {preferences.Player2.Name}", "Player's name", preferences);
            Menu player1Color = new Menu($"Player's mark color: <{ColorToIntDict[preferences.Player1.Color]}>X</>", "Player's mark color", preferences);
            Menu player2Color = new Menu($"Computer's mark color: <{ColorToIntDict[preferences.Player2.Color]}>O</>", "Player's mark color", preferences);
            
            Menu player1ColorSet = new Menu("Set player's color:\n" +
                                            "1. <1>Blue</>\n" +
                                            "2. <2>Yellow</>\n" +
                                            "3. <3>Red</>\n" +
                                            "4. Gray", "", preferences);
            player1Color.SetCallBack((string s, Menu element) =>
            {
                int colorTmp;
                if (int.TryParse(s, out colorTmp))
                {
                    preferences.Player1.Color = IntToColorDict[colorTmp];
                    element.Title = $"Player's mark color: <{ColorToIntDict[preferences.Player1.Color]}>X</>";
                    
                    Regex replace = new Regex(@"<\d>\w+<\/>");
                    element.BackReference.Title = replace.Replace(element.BackReference.Title,
                        $"<{ColorToIntDict[preferences.Player1.Color]}>X</>");
                    return true;
                }

                return false;
            });
            player1Color.AddElement(player1ColorSet);
            
            Menu player2ColorSet = new Menu("Set player's color:\n" +
                                            "1. <1>Blue</>\n" +
                                            "2. <2>Yellow</>\n" +
                                            "3. <3>Red</>\n" +
                                            "4. Gray", "", preferences);
            
            player2Color.SetCallBack((string s, Menu element) =>
            {
                int colorTmp;
                if (int.TryParse(s, out colorTmp))
                {
                    preferences.Player2.Color = IntToColorDict[colorTmp];
                    element.Title = $"Player's mark color: <{ColorToIntDict[preferences.Player2.Color]}>X</>";
                    
                    Regex replace = new Regex(@"<\d>\w+<\/>");
                    element.BackReference.Title = replace.Replace(element.BackReference.Title,
                        $"<{ColorToIntDict[preferences.Player2.Color]}>O</>");

                    return true;
                }

                return false;
            });
            player2Color.AddElement(player2ColorSet);
            
            Menu player1NameSet = new Menu("Set player's name", "", preferences);
            player1Name.SetCallBack((string s, Menu element) =>
            {
                if (s.Length > 0)
                {
                    Regex replace = new Regex(@"\w+$");
                    
                    element.Title = replace.Replace(element.Title, s);
                    
                    replace = new Regex(@"\w+(?= <)");
                    element.BackReference.Title =  replace.Replace(element.BackReference.Title, s);
                    preferences.Player1.Name = s;
                    return true;
                }

                return false;
            });
            
            player1Name.AddElement(player1NameSet);
            player1Settings.AddElement(player1Name, player1Color);
            
            player2Name.SetCallBack((string s, Menu element) =>
            {
                if (s.Length > 0)
                {
                    Regex replace = new Regex(@"\w+$");
                    
                    element.Title = replace.Replace(element.Title, s);
                    
                    replace = new Regex(@"\w+(?= <)");
                    element.BackReference.Title =  replace.Replace(element.BackReference.Title, s);
                    preferences.Player2.Name = s;
                    return true;
                }

                return false;
            });
            
            Menu player2NameSet = new Menu("Set player's name", "", preferences);
            player2Name.AddElement(player2NameSet);
            player2Settings.AddElement(player2Name, player2Color);
            
            gameMode.SetCallBack((string s, Menu element) =>
            {
                int userInput;
                if (int.TryParse(s, out userInput) && userInput > 0 && userInput < 4)
                {
                    preferences.GameMode = IntToGameModeDict[userInput];
                    element.Title = $"Game mode: {GameModeDict[preferences.GameMode]}";

                    switch (preferences.GameMode)
                    {
                        case GameMode.ComputerComputer:
                            preferences.Player1.Species = Species.Computer;
                            preferences.Player1.Name = "Computer";
                            preferences.Player2.Species = Species.Computer;
                            preferences.Player2.Name = "Computer";
                            player1Settings.Title = $"Computer1 settings: Computer1 <{ColorToIntDict[preferences.Player1.Color]}>X</>";
                            player1Settings.MenuLabel = "Computer settings";
                            player1Name.Title = $"Computer's name: {preferences.Player1.Name}";
                            player1Color.Title = $"Computer's mark color: <{ColorToIntDict[preferences.Player1.Color]}>X</>";
                            player2Settings.Title = $"Computer2 settings: Computer2 <{ColorToIntDict[preferences.Player2.Color]}>O</>";
                            player2Settings.MenuLabel = "Computer settings";
                            player2Name.Title = $"Computer's name: {preferences.Player2.Name}";
                            player2Color.Title = $"Computer's mark color: <{ColorToIntDict[preferences.Player2.Color]}>O</>";
                            break;
                        case GameMode.HumanComputer:
                            preferences.Player1.Species = Species.Human;
                            preferences.Player1.Name = "Player";
                            preferences.Player2.Species = Species.Computer;
                            preferences.Player2.Name = "Computer";
                            player1Settings.Title = $"Player settings: {preferences.Player1.Name} <{ColorToIntDict[preferences.Player1.Color]}>X</>";
                            player1Settings.MenuLabel = "Player settings";
                            player1Name.Title = $"Player's name: {preferences.Player1.Name}";
                            player1Color.Title = $"Player's mark color: <{ColorToIntDict[preferences.Player1.Color]}>X</>";
                            player2Settings.Title = $"Computer settings: Computer <{ColorToIntDict[preferences.Player2.Color]}>O</>";
                            player2Settings.MenuLabel = "Computer settings";
                            player2Name.Title = $"Computer's name: {preferences.Player2.Name}";
                            player2Color.Title = $"Computer's mark color: <{ColorToIntDict[preferences.Player2.Color]}>O</>";
                            break;
                        case GameMode.HumanHuman:
                            preferences.Player1.Species = Species.Human;
                            preferences.Player1.Name = "Player1";
                            preferences.Player2.Species = Species.Human;
                            preferences.Player2.Name = "Player2";
                            player1Settings.Title = $"Player1 settings: {preferences.Player1.Name} <{ColorToIntDict[preferences.Player1.Color]}>X</>";
                            player1Settings.MenuLabel = "Player settings";
                            player1Name.Title = $"Computer's name: {preferences.Player1.Name}";
                            player1Color.Title = $"Player's mark color: <{ColorToIntDict[preferences.Player1.Color]}>X</>";
                            player2Settings.Title = $"Player2 settings: {preferences.Player2.Name} <{ColorToIntDict[preferences.Player2.Color]}>O</>";
                            player2Settings.MenuLabel = "Player settings";
                            player2Name.Title = $"Computer's name: {preferences.Player2.Name}";
                            player2Color.Title = $"Player's mark color: <{ColorToIntDict[preferences.Player2.Color]}>O</>";
                            break;
                    }

                    return true;
                }

                return false;
            });
            Menu gameModeSet = new Menu("Please, input a digit (1, 2 or 3) that corresponds to the chosen game mode\n" +
                                        "1. Computer vs. Computer\n" +
                                        "2. Human vs. Computer\n" +
                                        "3. Human vs. Human", "", preferences);
            gameMode.AddElement(gameModeSet);
            
            Menu startGame = new Menu("Start the game", "", preferences);
            Menu launch = new Menu("", "", preferences);
            
            startGame.AddElement(launch);
            
            nextStep.AddElement(player1Settings, player2Settings, startGame);
            

            

            menu.AddElement(boardProperty, gameMode, nextStep);

            menu.ShowMenu();
        }
    }
}