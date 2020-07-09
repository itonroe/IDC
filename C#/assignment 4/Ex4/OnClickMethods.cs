using System;
using System.Collections.Generic;
using System.Text;

namespace Ex4
{
    public static class OnClickMethods
    {
        public static int CountCapitals(string i_string)
        {
            int numOfCapitals = 0;

            for(int i=0; i<i_string.Length; i++)
            {
                if (i_string[i] >= 'A' && i_string[i] <= 'Z')
                    numOfCapitals++;
            }

            return numOfCapitals;
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
