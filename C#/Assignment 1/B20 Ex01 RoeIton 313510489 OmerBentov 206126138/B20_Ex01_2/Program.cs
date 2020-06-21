using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex01_02
{
    public class Program
    {
        static int s_Height;

        static void Main()
        {
            InitHourGlass(5);

            CloseApplication();
        }

        static void PrintHourGlass(int i_CurrentHeight)
        {
            if (i_CurrentHeight <= 1)
            {
                if (i_CurrentHeight == 1)
                {
                    Console.WriteLine(CreateAsterisksLine(1));
                }
                return;
            }

            Console.WriteLine(CreateAsterisksLine(i_CurrentHeight));

            PrintHourGlass(i_CurrentHeight - 2);

            Console.WriteLine(CreateAsterisksLine(i_CurrentHeight));
        }

        static string CreateAsterisksLine(int i_Length)
        {
            StringBuilder asterisks = new StringBuilder();

            asterisks.Append(new string(' ', (s_Height - i_Length) / 2));

            asterisks.Append(new string('*', i_Length));

            return asterisks.ToString();
        }

        public static void InitHourGlass(int i_Height)
        {
            s_Height = i_Height;
            PrintHourGlass(i_Height);
        }

        static void CloseApplication()
        {
            Console.WriteLine("\nThat's it, press any letter to exit");
            Console.ReadKey();
        }
    }
}
