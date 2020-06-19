using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public class MenuItem
    {
        public delegate void Action();

        private event Action m_Action;
        private event Action<MenuItem> m_ReportMenuItemClicked;

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
                return m_Action != null ? true : false;
            }
        }

        public Action MethodAction 
        { 
            get
            {
                return m_Action;
            }
            set
            {
                m_Action += value;
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
                m_ReportMenuItemClicked += showSubMenu;

                addBackMenuItem();
            }

            MenuItem menuItem = new MenuItem(m_MainMenu, i_Title);
            m_MenuItems.Add(menuItem);
        }

        public void AddSubMenu(string i_Title, Action i_Action)
        {
            AddSubMenu(i_Title);

            m_MenuItems.Find(menuItem => menuItem.Title.Equals(i_Title)).m_Action += i_Action;
        }

        private void addBackMenuItem()
        {
            MenuItem backItem = new MenuItem(m_MainMenu, "Back");
            backItem.m_Parent = m_Parent;
            backItem.m_ReportMenuItemClicked += backClicked;

            m_MenuItems.Add(backItem);
        }

        private void backClicked(MenuItem i_MenuItem)
        {
            if (i_MenuItem.m_Parent!= null)
            {
                i_MenuItem.m_Parent.OnClick();
            }
            else
            {
                m_MainMenu.Show();
            }
        }

        private void showSubMenu(MenuItem i_MenuItem)
        {
            m_MainMenu.Show(i_MenuItem.m_Title, i_MenuItem.m_MenuItems);
        }

        public void OnClick()
        {
            doWhenClicked();
        }

        private void doWhenClicked()
        {
            if (IsAction)
            {
                m_Action.Invoke();
            }
            else
            {
                m_ReportMenuItemClicked.Invoke(this);
            }
        }
    }
}
