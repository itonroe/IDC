using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu
    {
        private readonly List<MenuItem> r_MenuItems;
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

            foreach (MenuItem menuItem in r_MenuItems)
            {
                Console.WriteLine("\t{0}", menuItem.Title);
            }
        }

        public void AddSubMenu(string i_Title)
        {
            MenuItem menuItem = new MenuItem(this, i_Title);

            r_MenuItems.Add(menuItem);
        }
    }
}
