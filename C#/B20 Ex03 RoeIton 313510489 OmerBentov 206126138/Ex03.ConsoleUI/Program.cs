using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class Program
    {
        public static void Main(string[] args)
        {
            GarageUI garageUI = new GarageUI();

            garageUI.StartApplication();

            CloseApplication();
        }

        public static void PrintArray(string title, string[] arr)
        {
            Console.WriteLine($"\t{title}-");

            if (arr != null)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.WriteLine($"\t\t{i + 1}. {arr[i]}");
                }
            }
        }

        public static void PrintDictionary(Dictionary<string, Dictionary<string, string[]>> i_Dictionary)
        {
            foreach (var item in i_Dictionary)
            {
                Console.WriteLine("\n" + item.Key + ":");

                foreach(var item2 in item.Value)
                {
                    PrintArray(item2.Key, item2.Value);
                }
            }
        }

        public static void CloseApplication()
        {
            Console.WriteLine("Press on any key to exit.");
            Console.ReadKey();
        }
    }
}
