using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex01_1
{
    class Program
    {
        const int k_NumOfInputs = 3;
        const int k_LengthOfNumber = 9;

        static string s_BinaryNumber1, s_BinaryNumber2, s_BinaryNumber3;

        static void Main()
        {

            Console.WriteLine("Enter 3 positive 9 length binary numbers follow by ENTER");

            Console.WriteLine("\nFirst number:");
            s_BinaryNumber1 = ReadNumber();

            Console.WriteLine("\nSecond number:");
            s_BinaryNumber2 = ReadNumber();

            Console.WriteLine("\nThird number:");
            s_BinaryNumber3 = ReadNumber();

            Console.WriteLine("\nYour numbers are: '{0}', '{1}' and '{2}'", BinaryToDecimal(s_BinaryNumber1),
                                                                 BinaryToDecimal(s_BinaryNumber2), BinaryToDecimal(s_BinaryNumber3));

            PrintStatics();


            Console.ReadLine();
        }

        static string ReadNumber()
        {
            string userNumber = Console.ReadLine();

            while (!IsNumValid(userNumber))
            {
                Console.WriteLine("Number is not valid, Please try again.");
                userNumber = Console.ReadLine();
            }

            return userNumber;
        }
        
        static bool IsNumValid(string i_Number)
        {
            bool valid = true;

            if (!IsNumBinary(i_Number) || !IsNumInLength(i_Number, k_LengthOfNumber))
            {
                valid = false;
            }

            return valid;
        }

        static bool IsNumBinary(string i_Number)
        {
            bool isBinary = true;
            
            for (int i = 0; i < i_Number.Length; i++)
            {
                if (i_Number[i] != '0' && i_Number[i] != '1')
                {
                    isBinary = false;
                    break;
                }
            }

            return isBinary;
        }

        static bool IsNumInLength(string i_Number, int i_Length)
        {
            bool validLength = true;

            if (i_Number.Length != i_Length)
            {
                validLength = false;
            }

            return validLength;
        }

        static int BinaryToDecimal(string i_BinaryNumber)
        {
            int binaryNumber = Convert.ToInt32(i_BinaryNumber, 2);

            return binaryNumber;
        }

        static void PrintStatics()
        {
            Console.WriteLine("\n********\nStatics:\n********\n");

            AverageNumberOfZeros();

            Console.WriteLine("\nThere are '{0}' numbers which is power of 2", PowOfTwoNumber());

            Console.WriteLine("\nThere are '{0}' numbers which is increasing serires in decimal", SumOfIncreasingSeries());

            Console.WriteLine("\nMaximum number is: {0}", MaxNumber());

            Console.WriteLine("Minimum number is: {0}", MinNumber());
        }

        static void AverageNumberOfZeros()
        {
            double sumOfZeros = CountZerosInNum(s_BinaryNumber1) + CountZerosInNum(s_BinaryNumber2) + CountZerosInNum(s_BinaryNumber3);
            double sumOfOnes = (k_NumOfInputs * k_LengthOfNumber) - sumOfZeros;

            Console.WriteLine("Average zeros is: {0}", sumOfZeros / k_NumOfInputs);

            Console.WriteLine("Average ones is: {0}", sumOfOnes / k_NumOfInputs);
        }

        static int CountZerosInNum(string i_Number)
        {
            int count = 0;

            for (int i = 0; i < i_Number.Length; i++)
            {
                if (i_Number[i] == '0')
                {
                    count++;
                }
            }

            return count;
        }

        static int PowOfTwoNumber()
        {
            int sumOfPow = 0;

            if (Math.Log(BinaryToDecimal(s_BinaryNumber1), 2) == Math.Round(Math.Log(BinaryToDecimal(s_BinaryNumber1), 2)))
            {
                sumOfPow++;
            }

            if (Math.Log(BinaryToDecimal(s_BinaryNumber2), 2) == Math.Round(Math.Log(BinaryToDecimal(s_BinaryNumber2), 2)))
            {
                sumOfPow++;
            }

            if (Math.Log(BinaryToDecimal(s_BinaryNumber3), 2) == Math.Round(Math.Log(BinaryToDecimal(s_BinaryNumber3), 2)))
            {
                sumOfPow++;
            }

            return sumOfPow;
        }

        static int SumOfIncreasingSeries()
        {
            int sumOfIncreasing = 0;

            sumOfIncreasing += IsDecimalIncreasingSeries(BinaryToDecimal(s_BinaryNumber1));
            sumOfIncreasing += IsDecimalIncreasingSeries(BinaryToDecimal(s_BinaryNumber2));
            sumOfIncreasing += IsDecimalIncreasingSeries(BinaryToDecimal(s_BinaryNumber3));

            return sumOfIncreasing;
        }

        static int IsDecimalIncreasingSeries(int i_Number)
        {
            string number = "" + i_Number;

            int result = 1;

            for (int i = 1; i < number.Length; i++)
            {
                if (number[i] <= number[i - 1])
                {
                    result = 0;
                    break;
                }
            }

            return result;
        }

        static int MaxNumber()
        {
            int decimalNumber1 = BinaryToDecimal(s_BinaryNumber1);
            int decimalNumber2 = BinaryToDecimal(s_BinaryNumber2);
            int decimalNumber3 = BinaryToDecimal(s_BinaryNumber3);

            int maxNumber;

            if (decimalNumber1 >= decimalNumber2 && decimalNumber1 >= decimalNumber3)
            {
                maxNumber = decimalNumber1;
            } 
            else if (decimalNumber2 >= decimalNumber1 && decimalNumber2 >= decimalNumber3)
            {
                maxNumber = decimalNumber2;
            } 
            else
            {
                maxNumber = decimalNumber3;
            }

            return maxNumber;
        }

        static int MinNumber()
        {
            int decimalNumber1 = BinaryToDecimal(s_BinaryNumber1);
            int decimalNumber2 = BinaryToDecimal(s_BinaryNumber2);
            int decimalNumber3 = BinaryToDecimal(s_BinaryNumber3);

            int minNumber;

            if (decimalNumber1 <= decimalNumber2 && decimalNumber1 <= decimalNumber3)
            {
                minNumber = decimalNumber1;
            }
            else if (decimalNumber2 <= decimalNumber1 && decimalNumber2 <= decimalNumber3)
            {
                minNumber = decimalNumber2;
            }
            else
            {
                minNumber = decimalNumber3;
            }

            return minNumber;
        }
    }
}
