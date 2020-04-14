using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex01_5
{
    class Program
    {
        static string s_StringUserInput;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter a 9 digit positive number follow by ENTER:");

            s_StringUserInput = ReadLine();

            Console.WriteLine("\nThank you!");

            PrintStatics();

            CloseApplication();
        }

        static string ReadLine()
        {
            string userNumber = Console.ReadLine();

            while(!NumberValidation(userNumber))
            {
                Console.WriteLine("Wrong input, please try again:");

                userNumber = Console.ReadLine();
            }

            return userNumber;
        }

        static bool NumberValidation(string i_Number)
        {
            return int.TryParse(i_Number, out int _) && i_Number.Length == 9;
        }

        static void PrintStatics()
        {
            Console.WriteLine("\n********\nStatics:\n********");

            Console.WriteLine("\nThe biggest digit in the number is: {0}", BiggestDigitInNumber());

            Console.WriteLine("\nThe smallest digit in the number is: {0}", SmallestDigitInNumber());

            Console.WriteLine("\nThe number of digits that divisible by 3 is: {0}", NumOfDigitsDivideBy3());

            Console.WriteLine("\nThe number of digits which grater then last digit is: {0}", NumOfDigitsGraterThanLastDigit());
        }

        static int BiggestDigitInNumber()
        {
            int maxDigit = 0;

            for(int i = 0; i < s_StringUserInput.Length; i++)
            {
                if(CharToInt(s_StringUserInput[i]) > maxDigit)
                {
                    maxDigit = CharToInt(s_StringUserInput[i]);
                    if (maxDigit == 9)
                        break;
                }
            }

            return maxDigit;
        }

        static int SmallestDigitInNumber()
        {
            int minDigit = 9;

            for (int i = 0; i < s_StringUserInput.Length; i++)
            {
                if (CharToInt(s_StringUserInput[i]) < minDigit)
                {
                    minDigit = CharToInt(s_StringUserInput[i]);
                    if (minDigit == 0)
                        break;
                }
            }

            return minDigit;
        }

        static int NumOfDigitsDivideBy3()
        {
            int numOfDigitsDivideBy3 = 0;

            for(int i = 0; i < s_StringUserInput.Length; i++)
            {
                if (CharToInt(s_StringUserInput[i]) % 3 == 0)
                    numOfDigitsDivideBy3++;
            }

            return numOfDigitsDivideBy3;
        }

        static int NumOfDigitsGraterThanLastDigit()
        {
            char lastDigit = s_StringUserInput[s_StringUserInput.Length - 1];
            int numOfDigitsGraterThanLastDigit = 0;

            for (int i = 0; i < s_StringUserInput.Length - 1; i++)
            {
                if (s_StringUserInput[i] >= lastDigit)
                    numOfDigitsGraterThanLastDigit++;
            }

            return numOfDigitsGraterThanLastDigit;
        }

        static int CharToInt(char i_ToDigit)
        {
            return i_ToDigit - 48;
        }

        static void CloseApplication()
        {
            Console.WriteLine("\nThat's it, press any letter to exit");
            Console.ReadKey();
        }
    }
}
