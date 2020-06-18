using System;
using System.Collections.Generic;
using System.Text;

namespace Ex4
{
    class MainManu
    {
        private string m_MenuName;
        private List<MenuItem> m_MainMenu = new List<MenuItem>();

        public MainManu()
        {
            //exit
            MenuItem exit = new MenuItem();
            exit.Text = "Exit";

            m_MainMenu.Add(exit);
            //create first top bar
            MenuItem versionAndCapitals = new MenuItem();
            versionAndCapitals.Text = "Version and Capitals";
            versionAndCapitals.Parent = null;

            MenuItem countCapitals = new MenuItem();
            countCapitals.Text = "Count capitalss";
            countCapitals.Parent = m_MainMenu;

            MenuItem showVersion = new MenuItem();
            showVersion.Text = "Show version";
            showVersion.Parent = m_MainMenu;

            versionAndCapitals.AddSubManuItem(countCapitals);
            versionAndCapitals.AddSubManuItem(showVersion);

            m_MainMenu.Add(versionAndCapitals);

            //create second top bar
            MenuItem showDateAndTime = new MenuItem();
            showDateAndTime.Text = "Show date and time";

            MenuItem showDate = new MenuItem();
            showDate.Text = "Show date";
            showVersion.Parent = m_MainMenu;

            MenuItem showTime = new MenuItem();
            showTime.Text = "Show time";
            showVersion.Parent = m_MainMenu;

            versionAndCapitals.AddSubManuItem(showDate);
            versionAndCapitals.AddSubManuItem(showTime);

            m_MainMenu.Add(showDateAndTime);

        }

        public void Show()
        {
            PrintMenu(m_MainMenu);
        }

        public List<MenuItem> PrintMenu(List<MenuItem> i_SubMenu, List<MenuItem> i_ParentManu)
        {
            PrintTitle("Title : ");
            PrintSubMenu(subMenu);
            int UserChoice = AskUser(subMenu.Count);
            if (UserChoice == 0)
                return subMenu[0].Parent;
            else
                return subMenu[UserChoice];
        }

        public void PrintTitle(string i_Title)
        {
            Console.WriteLine("Menu : {}", i_Title);
        }

        public void PrintSubMenu(List<MenuItem> i_MenuList)
        {
            foreach (MenuItem menuItem in i_MenuList)
            {
                Console.WriteLine(menuItem);
            }
        }

        public int AskUser(int i_CountOfSubItem)
        {
            int goodChoice = 0;

            Console.WriteLine("please choos...");
            string choich = Console.ReadLine();
            while(int.TryParse(choich,out goodChoice) && goodChoice >= 0 && goodChoice < i_CountOfSubItem)
            {
                Console.WriteLine("please choose...");
                choich = Console.ReadLine();
            }

            return goodChoice;
        }

    }
}
