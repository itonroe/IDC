﻿using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex01_04
{
    class Program
    {
        const int k_LengthOfString = 8;

        static string s_UserString;
        static bool s_StringIsNumber;

        static void Main()
        {
            Console.WriteLine($"Enter {k_LengthOfString} length string made up of numbers or english letters follow by ENTER");
            s_UserString = ReadLine();

            Console.WriteLine("\nIs input is a polyndrom: {0}", IsPolyndrom(s_UserString));

            if (s_StringIsNumber)
            {
                int divisibleNumber = 5;
                Console.WriteLine("\nIs number divisble with {0}: {1}", divisibleNumber, IsDivisibleWithNum(divisibleNumber));
            }
            else
            {
                Console.WriteLine("\nSum of capital letters in input: {0}", SumOfCapitalLetters());
            }

            CloseApplication();
        }

        static string ReadLine()
        {
            string userString = Console.ReadLine();

            while (!IsStringValid(userString))
            {
                Console.WriteLine("Input is not valid, Please try again.");
                userString = Console.ReadLine();
            }

            return userString;
        }

        static bool IsStringValid(string i_UserString)
        {
            return !((!IsStringNumery(i_UserString) && !IsStringAlphabetic(i_UserString)) || !IsStringInLength(i_UserString, k_LengthOfString));
        }

        static bool IsStringNumery(string i_UserString)
        {
            return s_StringIsNumber = int.TryParse(i_UserString, out int _);
        }

        static bool IsStringAlphabetic(string i_UserString)
        {
            bool isAlphabetic = true;

            for (int i = 0; i < i_UserString.Length; i++)
            {
                if (i_UserString[i] > 'z' || (i_UserString[i] < 'a' && i_UserString[i] > 'Z') || i_UserString[i] < 'A')
                {
                    isAlphabetic = false;
                    break;
                }
            }

            return isAlphabetic;
        }

        static bool IsStringInLength(string i_UserString, int i_Length)
        {
            return i_UserString.Length == i_Length;
        }

        static bool IsPolyndrom(string i_StringToCheck)
        {
            int stringLength = i_StringToCheck.Length;
            bool returnValue;

            if (stringLength <= 1)
            {
                returnValue = true;

            } else if (i_StringToCheck[0] != i_StringToCheck[stringLength - 1])
            {
                returnValue = false;
            } else
            {
                returnValue = IsPolyndrom(i_StringToCheck.Substring(1, stringLength - 2));
            }

            return returnValue;
        }

        static bool IsDivisibleWithNum(int i_NumToDivdeWith)
        {
            return (int.Parse(s_UserString) % i_NumToDivdeWith == 0);
        }

        static int SumOfCapitalLetters()
        {
            int sumOfCapitalLetters = 0;

            for (int i = 0; i < s_UserString.Length; i++)
            {
                if (s_UserString[i] >= 'A' && s_UserString[i] <= 'Z')
                {
                    sumOfCapitalLetters++;
                }
            }

            return sumOfCapitalLetters;
        }

        static void CloseApplication()
        {
            Console.WriteLine("\nThat's it, press any letter to exit");
            Console.ReadKey();
        }
    }
}
