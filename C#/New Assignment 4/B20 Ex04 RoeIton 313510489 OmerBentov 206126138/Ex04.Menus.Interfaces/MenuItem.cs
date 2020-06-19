using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Interfaces
{
    public interface IActionObserver
    {
        void Action();
    }

    public interface IShowMenuObserver
    {
        void Show(MenuItem i_MenuItem);
    }

    public class MenuItem
    {
        private IActionObserver m_ActionObserver;
        private IShowMenuObserver m_OnClickObserver;

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
                return m_ActionObserver != null ? true : false;
            }
        }

        public IActionObserver ActionObserver 
        {
            get
            {
                return m_ActionObserver;
            }

            set
            {
                m_ActionObserver = value;
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
                m_OnClickObserver = m_MainMenu as IShowMenuObserver;

                addBackMenuItem();
            }

            MenuItem menuItem = new MenuItem(m_MainMenu, i_Title);
            m_MenuItems.Add(menuItem);
        }

        public void AddSubMenu(string i_Title, IActionObserver i_ActionObserver)
        {
            AddSubMenu(i_Title);

            m_MenuItems.Find(menuItem => menuItem.Title.Equals(i_Title)).m_ActionObserver = i_ActionObserver;
        }

        private void addBackMenuItem()
        {
            MenuItem backItem = new MenuItem(m_MainMenu, "Back");
            backItem.m_Parent = m_Parent;
            backItem.m_OnClickObserver = new Back();
            m_MenuItems.Add(backItem);
        }

        public void OnClick()
        {
            doWhenClicked();
        }

        private void doWhenClicked()
        {
            if (IsAction)
            {
                m_ActionObserver.Action();
            }
            else
            {
                m_OnClickObserver.Show(this);
            }
        }

        internal class Back : IShowMenuObserver
        {
            public void Show(MenuItem i_MenuItem)
            {
                if (i_MenuItem.m_Parent != null)
                {
                    i_MenuItem.m_Parent.OnClick();
                }
                else
                {
                    i_MenuItem.m_MainMenu.Show();
                }
            }
        }
    }
}
