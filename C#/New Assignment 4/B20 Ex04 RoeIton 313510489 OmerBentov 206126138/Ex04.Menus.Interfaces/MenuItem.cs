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
        private readonly List<IActionObserver> r_ActionObservers = new List<IActionObserver>();
        private readonly List<IShowMenuObserver> r_OnClickObservers = new List<IShowMenuObserver>();

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
                return r_ActionObservers.Count != 0 ? true : false;
            }
        }

        public void AttachActionObserver(IActionObserver i_ActionObserver)
        {
            r_ActionObservers.Add(i_ActionObserver);
        }

        public void DetachActionObserver(IActionObserver i_ActionObserver)
        {
            r_ActionObservers.Remove(i_ActionObserver);
        }

        public void AttachShowMenuObserver(IShowMenuObserver i_ShowMenuObserver)
        {
            r_OnClickObservers.Add(i_ShowMenuObserver);
        }
        public void DetachShowMenuObserver(IShowMenuObserver i_ShowMenuObserver)
        {
            r_OnClickObservers.Remove(i_ShowMenuObserver);
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
                AttachShowMenuObserver(m_MainMenu as IShowMenuObserver);

                addBackMenuItem();
            }

            MenuItem menuItem = new MenuItem(m_MainMenu, i_Title);
            m_MenuItems.Add(menuItem);
        }

        public void AddSubMenu(string i_Title, IActionObserver i_ActionObserver)
        {
            AddSubMenu(i_Title);

            m_MenuItems.Find(menuItem => menuItem.Title.Equals(i_Title)).AttachActionObserver(i_ActionObserver);
        }

        private void addBackMenuItem()
        {
            MenuItem backItem = new MenuItem(m_MainMenu, "Back");
            backItem.m_Parent = m_Parent;
            backItem.AttachShowMenuObserver(new Back());
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
                foreach (IActionObserver observer in r_ActionObservers)
                {
                    observer.Action();
                }
            }
            else
            {
                foreach (IShowMenuObserver observer in r_OnClickObservers)
                {
                    observer.Show(this);
                }
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
