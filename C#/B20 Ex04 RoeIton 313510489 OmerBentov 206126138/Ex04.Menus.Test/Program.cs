using System;
using System.Collections.Generic;
using System.Text;
using Ex04.Menus.Delegates;

namespace Ex04.Menus.Test
{
    class Program
    {
        private static MainMenu m_MainMenuDelegate;

        public static void Main(string[] args)
        {
            BuildMenuDelegate("ZAIN");

            m_MainMenuDelegate.Show();

            Console.ReadLine();
        }


        public static void BuildMenuDelegate(string i_Title)
        {
            m_MainMenuDelegate = new MainMenu(i_Title);

            MenuItem versionandcapitals = new MenuItem(m_MainMenuDelegate, "Version and Captials");
            MenuItem countCapitals = new MenuItem(m_MainMenuDelegate, "Count Capitals");
            MenuItem showVersion = new MenuItem(m_MainMenuDelegate, "Show Version");
            countCapitals.m_Parent = versionandcapitals;
            showVersion.m_Parent = versionandcapitals;
            versionandcapitals.m_MenuItems.Add(countCapitals);
            versionandcapitals.m_MenuItems.Add(showVersion);

            countCapitals.m_ClickedMenuItemMethods += OnClickMethods.CountCapitals;

            versionandcapitals.m_ClickedMenuItemMethods += Versionandcapitals_m_ClickedMenuItemMethods;

            m_MainMenuDelegate.r_MenuItems.Add(versionandcapitals);


            m_MainMenuDelegate.AddSubMenu("Exit");
            /*m_MainMenuDelegate.AddSubMenu("Show Date and Time");

            m_MainMenuDelegate.Items[1].AddSubMenu("Count Capitals");
            m_MainMenuDelegate.Items[1].AddSubMenu("Show Version");

            m_MainMenuDelegate.Items[2].AddSubMenu("Show Time");
            m_MainMenuDelegate.Items[2].AddSubMenu("Show Date");*/
        }

        public static class OnClickMethods
        {
            public static void CountCapitals(string i_string)
            {
                int numOfCapitals = 0;

                for (int i = 0; i < i_string.Length; i++)
                {
                    if (i_string[i] >= 'A' && i_string[i] <= 'Z')
                        numOfCapitals++;
                }

                Console.WriteLine(numOfCapitals);
            }

            public static void ShowVersion()
            {
                Console.WriteLine("Version: 20.2.4.30620");
            }

            public static void ShowDate()
            {
                Console.WriteLine(new DateTime().Date);
            }

            public static void ShowTime()
            {
                Console.WriteLine(new DateTime().TimeOfDay);
            }

            public static void Exit()
            {
                Console.WriteLine("Bye bye");
            }
        }
    }
}
