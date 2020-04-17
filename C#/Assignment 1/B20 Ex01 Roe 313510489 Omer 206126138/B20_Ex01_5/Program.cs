using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex01_05
{
    class Program
    {
        const int k_LengthOfNumber = 9;

        static string s_StringUserInput;

        static void Main(string[] args)
        {
            Console.WriteLine($"Enter a {k_LengthOfNumber} digit positive number follow by ENTER");

            s_StringUserInput = ReadLine();

            PrintStatics();

            CloseApplication();
        }

        static string ReadLine()
        {
            string userNumber = Console.ReadLine();

            while(!IsNumValid(userNumber))
            {
                Console.WriteLine("Number is not valid, Please try again.");
                userNumber = Console.ReadLine();
            }

            return userNumber;
        }

        static bool IsNumValid(string i_Number)
        {
            return (IsNumDecimal(i_Number) && IsNumInLength(i_Number, k_LengthOfNumber));
        }

        static bool IsNumDecimal(string i_Number)
        {
            return int.TryParse(i_Number, out int _);
        }

        static bool IsNumInLength(string i_Number, int i_Length)
        {
            return (i_Number.Length == i_Length);
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
                if (s_StringUserInput[i] > lastDigit)
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
