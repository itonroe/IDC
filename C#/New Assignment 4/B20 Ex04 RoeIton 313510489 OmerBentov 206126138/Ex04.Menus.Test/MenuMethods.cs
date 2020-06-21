using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Test
{
    public class MenuMethods
    {
        internal static void Freeze(int i_Time)
        {
            System.Threading.Thread.Sleep(i_Time);
        }

        internal class DelegateMethods
        {
            internal static void CountCapitals()
            {
                Console.Clear();

                Console.WriteLine("Enter a sentence:");
                string userInput = Console.ReadLine();

                int countCapitals = 0;

                foreach (char letter in userInput)
                {
                    countCapitals += char.IsUpper(letter) ? 1 : 0;
                }

                Console.WriteLine($"Number of capital letters: {countCapitals}");

                Freeze(3000);
            }

            internal static void ShowVersion()
            {
                Console.Clear();
                Console.WriteLine("Version: 20.2.4.30620");
                Freeze(3000);
            }

            internal static void ShowTime()
            {
                Console.Clear();
                Console.WriteLine($"Current time is: {DateTime.Now.ToShortTimeString()}");
                Freeze(3000);
            }

            internal static void ShowDate()
            {
                Console.Clear();
                Console.WriteLine($"Current date is: {DateTime.Now.ToString("dd/MM/yyyy")}");
                Freeze(3000);
            }
        }

        internal class InterfaceMethods
        {
            internal struct CountCapitals : Interfaces.IActionObserver
            {
                public void Action()
                {
                    DelegateMethods.CountCapitals();
                }
            }

            internal struct ShowVersion : Interfaces.IActionObserver
            {
                public void Action()
                {
                    DelegateMethods.ShowVersion();
                }
            }

            internal struct ShowTime : Interfaces.IActionObserver
            {
                public void Action()
                {
                    DelegateMethods.ShowTime();
                }
            }

            internal struct ShowDate : Interfaces.IActionObserver
            {
                public void Action()
                {
                    DelegateMethods.ShowDate();
                }
            }
        }
    }
}
