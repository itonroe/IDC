using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Test
{
    public class MenuMethods
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
            Console.WriteLine($"Current date is: {DateTime.Now.ToShortDateString()}");
            Freeze(3000);
        }

        internal static void Exit()
        {
            Environment.Exit(0);
        }

        internal static void Freeze(int i_Time)
        {
            System.Threading.Thread.Sleep(i_Time);
        }
    }
}
