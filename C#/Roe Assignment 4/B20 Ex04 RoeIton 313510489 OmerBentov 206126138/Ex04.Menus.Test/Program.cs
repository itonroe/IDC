using System;
using System.Collections.Generic;
using System.Text;
using static Ex04.Menus.Test.MenuMethods;
using Ex04.Menus.Delegates;

namespace Ex04.Menus.Test
{
    class Program
    {
        private static MainMenu m_MainMenuDelegate;

        public static void Main(string[] args)
        {
            BuildMenuDelegate("Delegate");

            m_MainMenuDelegate.Show();

            Console.ReadLine();
        }


        public static void BuildMenuDelegate(string i_Title)
        {
            m_MainMenuDelegate = new MainMenu(i_Title);

            m_MainMenuDelegate.AddSubMenu("Exit");
            m_MainMenuDelegate.AddSubMenu("Version and Captials");
            m_MainMenuDelegate.AddSubMenu("Show Date and Time");

            m_MainMenuDelegate.Items[0].MethodAction += Exit;
            m_MainMenuDelegate.Items[1].AddSubMenu("Count Capitals", CountCapitals);
            m_MainMenuDelegate.Items[1].AddSubMenu("Show Version", ShowVersion);
            m_MainMenuDelegate.Items[2].AddSubMenu("Show Time", ShowTime);
            m_MainMenuDelegate.Items[2].AddSubMenu("Show Date", ShowDate);
        }
    }
}
