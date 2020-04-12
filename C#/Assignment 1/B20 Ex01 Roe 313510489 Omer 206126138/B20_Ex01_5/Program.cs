using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex01_5
{
    class Program
    {
        static int m_IntegerUserInput;
        static string m_StringUserInput;

        public static void Main(string[] args)
        {
            read();
            biggestDigitInNumber();
            smallestDigitInNumber();
            numOfDigitsDivideBy3();
            numOfDigitsGraterThanLastDigit();
            Console.ReadLine();
        }

        public static void read()
        {
            Console.WriteLine("Please enter a positive number 9 digit length :");
            m_StringUserInput = Console.ReadLine();
            while(!numberValidation())
            {
                Console.WriteLine("Wrong input, please try again :");
                m_StringUserInput = Console.ReadLine();
            }
            Console.WriteLine("Thnak you!");
        }

        public static Boolean numberValidation()
        {
            return int.TryParse(m_StringUserInput, out m_IntegerUserInput);
        }

        public static void biggestDigitInNumber()
        {
            int maxDigit = 0;

            for(int i=0; i<m_StringUserInput.Length; i++)
            {
                if(m_StringUserInput[i] > maxDigit)
                {
                    maxDigit = Int32.Parse("" + m_StringUserInput[i]);
                    if (maxDigit == 9)
                        break;
                }
            }

            Console.WriteLine("The biggest digit in the number is : {0}", maxDigit);
        }

        public static void smallestDigitInNumber()
        {
            int minDigit = 9;

            for (int i = 0; i < m_StringUserInput.Length; i++)
            {
                if (Int32.Parse("" + m_StringUserInput[i]) < minDigit)
                {
                    minDigit = Int32.Parse("" + m_StringUserInput[i]); ;
                    if (minDigit == 0)
                        break;
                }
            }

            Console.WriteLine("The smallest digit in the number is : {0}", minDigit);
        }

        public static void numOfDigitsDivideBy3()
        {
            int numOfDigitsDivideBy3 = 0;

            for(int i=0; i< m_StringUserInput.Length; i++)
            {
                if (Int32.Parse("" + m_StringUserInput[i]) % 3 == 0)
                    numOfDigitsDivideBy3++;
            }

            Console.WriteLine("The number of digits which can be divide by 3 is : {0}", numOfDigitsDivideBy3);
        }
        
        public static void numOfDigitsGraterThanLastDigit()
        {
            int lastDigit = m_StringUserInput[m_StringUserInput.Length - 1];
            int numOfDigitsGraterThanLastDigit = 0;

            for (int i = 0; i < m_StringUserInput.Length - 1; i++)
            {
                // >= or >?
                if (Int32.Parse("" + m_StringUserInput[i]) >= lastDigit)
                    numOfDigitsGraterThanLastDigit++;
            }

            Console.WriteLine("The number of digits which grater then last digit is : {0}", numOfDigitsGraterThanLastDigit);
        }
    }
}
