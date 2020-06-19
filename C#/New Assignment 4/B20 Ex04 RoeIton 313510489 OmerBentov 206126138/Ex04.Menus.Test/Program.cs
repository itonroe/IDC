using System;
using System.Collections.Generic;
using System.Text;
using static Ex04.Menus.Test.MenuMethods;
using Ex04.Menus.Delegates;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    class Program
    {
        private static Delegates.MainMenu m_MainMenuDelegate;
        private static Interfaces.MainMenu m_MainMenuInterface;

        public static void Main(string[] args)
        {
            BuildMenuDelegate("Delegate");
            m_MainMenuDelegate.Show();

            BuildMenuInterface("Interface");
            m_MainMenuInterface.Show();
        }

        public static void BuildMenuDelegate(string i_Title)
        {
            m_MainMenuDelegate = new Delegates.MainMenu(i_Title);

            m_MainMenuDelegate.AddSubMenu("Exit");
            m_MainMenuDelegate.AddSubMenu("Version and Captials");
            m_MainMenuDelegate.AddSubMenu("Show Date/Time");

            m_MainMenuDelegate.Items[1].AddSubMenu("Count Capitals", DelegateMethods.CountCapitals);
            m_MainMenuDelegate.Items[1].AddSubMenu("Show Version", DelegateMethods.ShowVersion);
            m_MainMenuDelegate.Items[2].AddSubMenu("Show Time", DelegateMethods.ShowTime);
            m_MainMenuDelegate.Items[2].AddSubMenu("Show Date", DelegateMethods.ShowDate);
        }

        public static void BuildMenuInterface(string i_Title)
        {
            m_MainMenuInterface = new Interfaces.MainMenu(i_Title);

            m_MainMenuInterface.AddSubMenu("Exit");
            m_MainMenuInterface.AddSubMenu("Version and Captials");
            m_MainMenuInterface.AddSubMenu("Show Date/Time");

            m_MainMenuInterface.Items[1].AddSubMenu("Count Capitals", new InterfaceMethods.CountCapitals() as IActionObserver);
            m_MainMenuInterface.Items[1].AddSubMenu("Show Version", new InterfaceMethods.ShowVersion() as IActionObserver);
            m_MainMenuInterface.Items[2].AddSubMenu("Show Time", new InterfaceMethods.ShowTime() as IActionObserver);
            m_MainMenuInterface.Items[2].AddSubMenu("Show Date", new InterfaceMethods.ShowDate() as IActionObserver);
        }
    }
}
