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
            Read();
            BiggestDigitInNumber();
            SmallestDigitInNumber();
            NumOfDigitsDivideBy3();
            NumOfDigitsGraterThanLastDigit();
            //Missing close function
            Console.ReadLine();
        }

        public static void Read()
        {
            Console.WriteLine("Please enter a positive number 9 digit length :");
            m_StringUserInput = Console.ReadLine();
            while(!NumberValidation())
            {
                Console.WriteLine("Wrong input, please try again :");
                m_StringUserInput = Console.ReadLine();
            }
            Console.WriteLine("Thnak you!");
        }

        public static Boolean NumberValidation()
        {
            return int.TryParse(m_StringUserInput, out m_IntegerUserInput) && m_StringUserInput.Length == 9;
        }

        public static void BiggestDigitInNumber()
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

        public static void SmallestDigitInNumber()
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

        public static void NumOfDigitsDivideBy3()
        {
            int numOfDigitsDivideBy3 = 0;

            for(int i=0; i< m_StringUserInput.Length; i++)
            {
                if (Int32.Parse("" + m_StringUserInput[i]) % 3 == 0)
                    numOfDigitsDivideBy3++;
            }

            Console.WriteLine("The number of digits which can be divide by 3 is : {0}", numOfDigitsDivideBy3);
        }
        
        public static void NumOfDigitsGraterThanLastDigit()
        {
            char lastDigit = m_StringUserInput[m_StringUserInput.Length - 1];
            int numOfDigitsGraterThanLastDigit = 0;

            for (int i = 0; i < m_StringUserInput.Length - 1; i++)
            {
                // >= or >?
                if (m_StringUserInput[i] >= lastDigit)
                    numOfDigitsGraterThanLastDigit++;
            }

            Console.WriteLine("The number of digits which grater then last digit is : {0}", numOfDigitsGraterThanLastDigit);
        }
    }
}
