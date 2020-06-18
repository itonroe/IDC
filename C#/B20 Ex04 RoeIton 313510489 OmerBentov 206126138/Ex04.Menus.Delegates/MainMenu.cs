using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public class MainMenu
    {
        public List<MenuItem> r_MenuItems;
        private string m_Title;

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
            Console.WriteLine("----{0} Menu----", m_Title);

            int indexSelection = 0;

            foreach (MenuItem menuItem in r_MenuItems)
            {
                Console.WriteLine($"\t{indexSelection}. {menuItem.Title}");
                indexSelection++;
            }

            pickFromOptions();
        }

        private void pickFromOptions()
        {
            Console.WriteLine("Enter option number");
            int pick = int.Parse(Console.ReadLine());

            r_MenuItems[pick].Clicked();
        }

        public void AddSubMenu(string i_Title)
        {
            MenuItem menuItem = new MenuItem(this, i_Title);

            r_MenuItems.Add(menuItem);
        }
    }
}
