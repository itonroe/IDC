using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        public event Action<MenuItem> m_ClickedMenuItemMethods;
        private List<MenuItem> m_MenuItems;
        private MainMenu m_MainMenu;
        private MenuItem m_Parent;
        private string m_Title;

        public string Title
        {
            get
            {
                return m_Title;
            }
        }

        public bool IsAction
        {
            get
            {
                return m_MenuItems.Count == 0 ? true : false;
            }
        }

        public MenuItem(MainMenu i_MainMenu, string i_Title)
        {
            m_Title = i_Title;
            m_MainMenu = i_MainMenu;
            m_MenuItems = new List<MenuItem>();
        }

        public void AddSubMenu(string i_Title)
        {
            MenuItem menuItem = new MenuItem(m_MainMenu, i_Title);

            menuItem.m_Parent = this;
            menuItem.m_ClickedMenuItemMethods += this.showAndPick;

            m_MenuItems.Add(menuItem);
        }

        private void doWhenClicked()
        {
            if (IsAction)
            {
                //Do Action
            }
            else
            {
                notifyMethodObservers();
            }
        }

        private void showAndPick(MenuItem i_MenuItem)
        {
            if (i_MenuItem.m_Parent == null)
            {
                m_MainMenu.Show();
            }
            else
            {
                Console.WriteLine("----{0} Menu----", i_MenuItem.m_Parent.Title);

                foreach (MenuItem menuItem in i_MenuItem.m_Parent.m_MenuItems)
                {
                    Console.WriteLine("\t{0}", menuItem.Title);
                }

                pickFromOptions();
            }
        }

        private void pickFromOptions()
        {
            Console.WriteLine("Enter option number");
            int pick = int.Parse(Console.ReadLine());

            m_MenuItems[pick - 1].m_ClickedMenuItemMethods.Invoke(this);
        }

        private void notifyMethodObservers()
        {
            if (m_ClickedMenuItemMethods != null)
            {
                m_ClickedMenuItemMethods.Invoke(m_Parent);
            }
        }
    }
}
