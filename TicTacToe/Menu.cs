using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class Menu
    {
        private List<Menu> MenuElements;
        //private List<string> MenuLabel;
        private string MenuLabel;
        public string Title { get; set; }
        public Menu BackReference { get; set; }
        private Func<string, Menu, bool> Callback;
        
        public Menu()
        {
            MenuElements = new List<Menu>();
            //MenuLabel = new List<string>();
            //MenuLabel.Insert(0, "Menu");
            MenuLabel = "Menu";
        }

        public Menu(string title, string menuLabel)
        {
            MenuElements = new List<Menu>();
            Title = title;
            //MenuLabel = new List<string>();
            MenuLabel = menuLabel;
        }

        public void SetCallBack(Func<string, Menu, bool> callback)
        {
            Callback = callback;
        }

        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine(CreateMenuLabels(this));
            //Console.WriteLine(MenuElements.Count);
            if (Callback != null)
            {
                Console.WriteLine(MenuElements[0].Title);
            }
            else
            {
                for (int i = 0; i < MenuElements.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {MenuElements[i].Title}");
                }

                if (BackReference != null)
                {
                    Console.WriteLine("0. Back");
                }
            }
            
            GetUserInput();
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