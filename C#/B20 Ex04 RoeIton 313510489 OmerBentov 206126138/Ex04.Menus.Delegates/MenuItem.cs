using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public class MenuItem
    {
        public event Action<string> m_ClickedMenuItemMethods;
        public List<MenuItem> m_MenuItems;
        private MainMenu m_MainMenu;
        public MenuItem m_Parent;
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
            if (m_MenuItems.Count == 0)
            {
                MenuItem backItem = new MenuItem(m_MainMenu, "Back");
                backItem.m_Parent = this;

                m_MenuItems.Add(backItem);
            }

            MenuItem menuItem = new MenuItem(m_MainMenu, i_Title);
            menuItem.m_ClickedMenuItemMethods += this.showAndPick;

            m_MenuItems.Add(menuItem);
        }

        public void Clicked()
        {
            doWhenClicked();
        }

        private void doWhenClicked()
        {
            if (IsAction)
            {
                //Do Action
            }
            else
            {

                //Back
                //
                //if (i_MenuItem.m_Parent == null)
                //{
                  //  m_MainMenu.Show();
                //}
                notifyMethodObservers(this);
            }
        }

        private void notifyMethodObservers(MenuItem i_Item)
        {
            if (m_ClickedMenuItemMethods != null)
            {
                Console.Clear();

                m_ClickedMenuItemMethods.Invoke(i_Item);
            }
        }
    }
}
