using System;
using System.Collections.Generic;
using System.Text;


namespace B20_Ex01_3
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter height of hourglass follow by ENTER");

            B20_Ex01_2.Program.s_Height = ReadNumber();
            B20_Ex01_2.Program.PrintHourGlass(B20_Ex01_2.Program.s_Height);


            CloseApplication();
        }

        static int ReadNumber()
        {
            string userNumber = Console.ReadLine();

            while (!IsNumValid(userNumber))
            {
                Console.WriteLine("Number is not valid, Please try again.");
                userNumber = Console.ReadLine();
            }

            return int.Parse(userNumber);
        }

        static bool IsNumValid(string i_Number)
        {
            return int.TryParse(i_Number, out int _);
        }

        static void CloseApplication()
        {
            Console.WriteLine("\nThat's it, press any letter to exit");
            Console.ReadKey();
        }
    }
}
