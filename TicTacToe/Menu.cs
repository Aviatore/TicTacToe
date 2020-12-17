using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TicTacToe
{
    public class Menu
    {
        private List<Menu> MenuElements;
        //private List<string> MenuLabel;
        public string MenuLabel { get; set; }
        public string Title { get; set; }
        public Menu BackReference { get; set; }
        private Func<string, Menu, bool> Callback;

        private Preferences _preferences;
        private Game _game;
        
        
        public Menu(Preferences preferences)
        {
            MenuElements = new List<Menu>();
            //MenuLabel = new List<string>();
            //MenuLabel.Insert(0, "Menu");
            MenuLabel = "Menu";
            _preferences = preferences;
        }

        public Menu(string title, string menuLabel, Preferences preferences)
        {
            MenuElements = new List<Menu>();
            Title = title;
            //MenuLabel = new List<string>();
            MenuLabel = menuLabel;
            _preferences = preferences;
        }
        
        public Menu(Game game, Preferences preferences)
        {
            _game = game;
            _preferences = preferences;
            
        }

        public void SetCallBack(Func<string, Menu, bool> callback)
        {
            Callback = callback;
        }

        public void ShowMenu()
        {
            
            if (MenuElements[0].Title == "")
            {
                Game game = new Game(_preferences.BoardSize, _preferences.BoardSize, _preferences.numberOfMarksToWin);
                
                game.CreateGame(_preferences);
                
                BackReference.ShowMenu();
            }
            Console.Clear();
            Console.WriteLine(CreateMenuLabels(this));
            //Console.WriteLine(MenuElements.Count);
            if (Callback != null)
            {
                //Console.WriteLine(MenuElements[0].Title);
                PrintInColor(MenuElements[0].Title);
            }
            else
            {
                for (int i = 0; i < MenuElements.Count; i++)
                {
                    //Console.WriteLine($"{i + 1}. {MenuElements[i].Title}");
                    PrintInColor($"{i + 1}. {MenuElements[i].Title}");
                }

                if (BackReference != null)
                {
                    Console.WriteLine("0. Back");
                }
            }
            
            GetUserInput();
        }

        private void PrintInColor(string message)
        {
            //Console.WriteLine($"color: {Colors.Blue}");
            Regex marked = new Regex(@"(<\d>)(\w+)(?=<)");
            Regex nonMarked = new Regex(@"([\w\s]+)(?=<)");
            Regex removeBraces = new Regex(@"[<>]");
            
            //MatchCollection markedMatches = marked.Matches(message);
            //MatchCollection nonMarkedMatches = nonMarked.Matches(message);

            string[] lines = message.Split("\n");
            
            foreach (string line in lines)
            {
                string[] words = line.Split(" ");
                foreach (string word in words)
                {
                    //Console.WriteLine($"word: {word}");
                    MatchCollection markedMatches = marked.Matches(word);
                    
                    if (markedMatches.Count > 0)
                    {
                        {for (int i = 0; i < markedMatches.Count; i++)
                            //foreach (Match match in markedMatches)
                        {
                            int colorNumber;
                            bool success = int.TryParse(removeBraces.Replace(markedMatches[i].Groups[1].Value, ""),
                                out colorNumber);

                            if (!success)
                                colorNumber = 4;

                            Console.ForegroundColor = (ConsoleColor) GameMenu.IntToColorDict[colorNumber];
                            Console.Write(markedMatches[i].Groups[2].Value);
                            Console.ResetColor();

                        }}
                    }
                    else
                    {
                        Console.Write($"{word} ");
                    }
                    
                }
                Console.WriteLine("");
            }
        }

        private void GetUserInput()
        {
            bool loop = true;
            
            while (loop)
            {
                Console.Write("> ");
                string userInput = Console.ReadLine();

                if (Callback != null)
                {
                    if (!Callback(userInput, this))
                    {
                        ClearPreviousLine();
                        continue;
                    }
                    
                    BackReference?.ShowMenu();
                }
                else
                {
                    int userInputInt;
                    bool success = int.TryParse(userInput, out userInputInt);

                    if (!success || userInputInt > MenuElements.Count || userInputInt < 0)
                    {
                        ClearPreviousLine();
                        continue;
                    }

                    if (userInputInt == 0)
                    {
                        BackReference?.ShowMenu();
                        ClearPreviousLine();
                    }
                    else
                    {
                        if (MenuElements.Count > 0)
                        {
                            MenuElements[userInputInt - 1].ShowMenu();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The function creates the menu label by recursively analysing back-referencing elements
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private string CreateMenuLabels(Menu element)
        {
            string output;
            
            if (element.BackReference != null)
            {
                output = CreateMenuLabels(element.BackReference);
                return $"{output} => {element.MenuLabel}";
            }
            else
            {
                output = "Menu";
            }

            return output;
        }

        private void ClearPreviousLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth)); 
            Console.SetCursorPosition(0, currentLineCursor - 1);
        }

        public void AddMenuLabel(string label)
        {
            //MenuLabel.Add(label);
        }
        
        public void AddElement(Menu element)
        {
            element.BackReference = this;
            //element.MenuLabel.InsertRange(0, this.MenuLabel);
            
            MenuElements.Add(element);
            
            BackReference = this;
        }
        
        public void AddElement(params Menu[] elements)
        {
            foreach (Menu element in elements)
            {
                element.BackReference = this;
                //element.MenuLabel.InsertRange(0, this.MenuLabel);
                MenuElements.Add(element);                
            }
        }
    }
}