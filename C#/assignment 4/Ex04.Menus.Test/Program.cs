using System;
using System.Collections.Generic;
using System.Text;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    class Program
    {
        private static MainMenu m_MainMenuDelegate;

        public static void Main(string[] args)
        {
            BuildMenuDelegate("זין");

            m_MainMenuDelegate.Show();
        }


        public static void BuildMenuDelegate(string i_Title)
        {
            m_MainMenuDelegate = new MainMenu(i_Title);

            m_MainMenuDelegate.AddSubMenu("Exit");
            m_MainMenuDelegate.AddSubMenu("Version and Capitals");
            m_MainMenuDelegate.AddSubMenu("Show Date and Time");

            m_MainMenuDelegate.Items[1].AddSubMenu("Count Capitals");
            m_MainMenuDelegate.Items[1].AddSubMenu("Show Version");

            m_MainMenuDelegate.Items[2].AddSubMenu("Show Time");
            m_MainMenuDelegate.Items[2].AddSubMenu("Show Date");
        }
    }
}
