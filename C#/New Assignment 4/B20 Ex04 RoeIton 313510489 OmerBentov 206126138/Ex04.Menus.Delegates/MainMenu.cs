using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public class MainMenu
    {
        private List<MenuItem> r_MenuItems;
        private string m_Title;
        private bool m_AppIsOn;

        public string Title
        {
            get
            {
                return m_Title;
            }
        }

        public List<MenuItem> Items
        {
            get
            {
                return r_MenuItems;
            }
        }

        public MainMenu(string i_Title)
        {
            m_Title = i_Title;
            r_MenuItems = new List<MenuItem>();
        }

        public void Show() 
        {
            m_AppIsOn = true;

            while (m_AppIsOn)
            {
                Show(m_Title, Items);
            }
        }

        public void Show(string i_Title, List<MenuItem> i_Items)
        {
            Console.Clear();

            string titleMenu = i_Title.Equals(m_Title) ? "Menu" : "Submenu";

            printHeadLine($"{i_Title} {titleMenu}");

            for (int i = 1; i < i_Items.Count; i++)
            {
                Console.WriteLine($"\t{i}. {i_Items[i].Title}");
            }

            Console.WriteLine($"\t0. {i_Items[0].Title}");

            int pick = pickFromOptions(i_Items.Count);

            if (pick == 0 && i_Items[0].Title.Equals("Exit"))
            {
                m_AppIsOn = false;
            }
            else
            {
                i_Items[pick].OnClick();
            }
        }

        private int pickFromOptions(int i_MaxValue)
        {
            Console.WriteLine("\nEnter option index:");
            string userInput = Console.ReadLine();

            while (!isValidSelection(userInput, 0, i_MaxValue))
            {
                Console.WriteLine("Input invalid, please try again...");
                userInput = Console.ReadLine();
            }

            return int.Parse(userInput);
        }

        private bool isValidSelection(string i_Selection, int i_MinValue, int i_MaxValue)
        {
            return int.TryParse(i_Selection, out _) && int.Parse(i_Selection) >= i_MinValue && int.Parse(i_Selection) <= i_MaxValue;
        }

        public void AddSubMenu(string i_Title)
        {
            if (r_MenuItems.Count == 0)
            {
                r_MenuItems.Add(new MenuItem(this, "Exit"));
            }

            MenuItem menuItem = new MenuItem(this, i_Title);

            r_MenuItems.Add(menuItem);
        }

        private void printHeadLine(string i_HeadLine)
        {
            const int lineLength = 40;

            int length = i_HeadLine.Length;
            int countOfHyphenSide = (lineLength - (length + 2)) / 2;

            StringBuilder line = new StringBuilder();

            line.Append(new string('-', countOfHyphenSide));
            line.Append($" {i_HeadLine} ");
            line.Append(new string('-', countOfHyphenSide));
            line.Append("\n");

            Console.WriteLine(line.ToString());
        }
    }
}
