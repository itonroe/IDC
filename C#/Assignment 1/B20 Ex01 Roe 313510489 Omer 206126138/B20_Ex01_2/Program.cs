using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex01_2
{
    class Program
    {
        static int s_Height;

        static void Main()
        {
            s_Height = 5;

            PrintHourGlass(s_Height);
        }

        static void PrintHourGlass(int i_CurrentHeight)
        {
            if (i_CurrentHeight <= 1)
            {
                if (i_CurrentHeight == 1)
                {
                    Console.WriteLine(Asterisks(1));
                }
                return;
            }

            Console.WriteLine(Asterisks(i_CurrentHeight));

            PrintHourGlass(i_CurrentHeight - 2);

            Console.WriteLine(Asterisks(i_CurrentHeight));
        }

        static string Asterisks(int i_Length)
        {
            StringBuilder asterisks = new StringBuilder();

            asterisks.Append(new string(' ', (s_Height - i_Length) / 2));

            asterisks.Append(new String('*', i_Length));

            return asterisks.ToString();
        }
    }
}
