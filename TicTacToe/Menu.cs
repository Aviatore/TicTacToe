using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class Menu
    {
        private List<Menu> MenuElements;
        private string Title { get; set; }
        public Menu BackReference { get; set; }
        private Action<string> Callback;
        
        public Menu()
        {
            MenuElements = new List<Menu>();
        }

        public Menu(string title)
        {
            MenuElements = new List<Menu>();
            Title = title;
        }

        public void SetCallBack(Action<string> callback)
        {
            Callback = callback;
        }

        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Menu:");
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
                    Callback(userInput);
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

        private void ClearPreviousLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth)); 
            Console.SetCursorPosition(0, currentLineCursor - 1);
        }

        public void AddElement(Menu element)
        {
            element.BackReference = this;
            MenuElements.Add(element);
            
            BackReference = this;
        }
        
        public void AddElement(params Menu[] elements)
        {
            foreach (Menu element in elements)
            {
                element.BackReference = this;
                MenuElements.Add(element);                
            }
        }
    }
}