using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class GarageUI
    {
        private enum eViews
        {
            Menu = 0,

            AddVehicle = 1,
            ListVehicles = 1,
            ChangeVehicleStatus = 1,
            InflateVehicleTires = 1,
            RefuleVehicle = 1,
            RechargeVehicle = 1,
            VehicleInfo = 1,
        }

        private static Garage m_Garage;
        private static int m_LevelOperation;
        private static bool m_ApplicationRunning;

        public GarageUI()
        {
            m_Garage = new Garage();
        }

        public void StartApplication()
        {
            m_LevelOperation = 0;
            m_ApplicationRunning = true;

            while (m_ApplicationRunning)
            {
                ManageMenuOperations();
            }
        }

        private static bool AgainOrBack(string i_OperationName)
        {
            Console.WriteLine($"\nPlease choose what's next:\nA - {i_OperationName}, B - Back to menu.");
            string userInput = Console.ReadLine();

            while (!userInput.Equals("A") && !userInput.Equals("B"))
            {
                Console.WriteLine($"Wrong input, try again... A - {i_OperationName}, B - Back to menu.");
                userInput = Console.ReadLine();
            }

            bool again = true;

            if (userInput.Equals("B"))
            {
                again = false;
            }

            Console.Clear();

            return again;
        }

        private static void ManageMenuOperations()
        {
            string[] opertaions =  new string[] { "Add a vehicle", 
                "Get a list of vehicles in the garage", "Change a vehicle status", 
                "Inflate vehicle's tires to max", "Refule vehicle", "Recharge vehicle", "Vehicle Information", "Exit"};

            PrintOpertaions("Menu", opertaions, (int)eViews.Menu, ':');

            int operationSelected = ReadUserSelection(opertaions.Length);

            Console.Clear();

            switch (operationSelected)
            {
                case 1:
                    while (ManageAddVehicle()) ;
                    break;
                case 2:
                    while (ManageListVehicles());
                    break;
                case 3:
                    while (ManageChangeStatus()) ;
                    break;
                case 4:
                    while (ManageInflateToMax()) ;
                    break;
                case 5:
                    while (ManageRefule()) ;
                    break;
                case 6:
                    while (ManageRecharge()) ;
                    break;
                case 7:
                    while (ManageInformation()) ;
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }


        }

        private static bool ManageAddVehicle()
        {
            Console.WriteLine("Vehicle's License Plate:");
            Console.Write("\t");
            string licensePlate = Console.ReadLine();

            if (m_Garage.IsExists(licensePlate))
            {
                Console.WriteLine("The vehicle is already in the garage.");
                m_Garage.ChangeStatus(licensePlate, "InProgress");

                PrintVehicleInformation(licensePlate);

                Console.WriteLine("\nJust letting you know, vehicle's status change to 'In Progress'.");
            }
            else
            {
                PrintHeadLine("Basic Information", false);
                Console.WriteLine();
                string vehicleSelection = GetSelectionOf("Vehicle Type", m_Garage.VehicleTypes(), 0);
                Console.WriteLine();
                string engineSelection = GetSelectionOf("Engine Type", m_Garage.EngineTypes(), 0);

                PrintHeadLine($"{engineSelection} {vehicleSelection} - {licensePlate}", false);
                PrintHeadLine("More Information", true);

                Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> nextInfo = m_Garage.AddVehicle(vehicleSelection, engineSelection, licensePlate);
                Dictionary<string, Dictionary<string, string>> nextInfo_answers = new Dictionary<string, Dictionary<string, string>>();

                Console.WriteLine($"\nOwner:");
                string ownerName = GetFreeInputOf("Vehicle's Owner Name");
                Console.WriteLine();
                string ownerPhoneNumber = GetPhoneNumber();

                m_Garage.SetVehicleOwner(licensePlate, ownerName, ownerPhoneNumber);

                try
                {
                    foreach (var generalProperty in nextInfo)
                    {
                        Console.WriteLine($"\n{generalProperty.Key}:");

                        Dictionary<string, string> newAnswerProperty = new Dictionary<string, string>();

                        foreach (var subProperty in generalProperty.Value)
                        {

                            foreach (var oneQuestion in subProperty.Key)
                            {
                                string question = oneQuestion.Key;
                                string answer;
                                if (subProperty.Value == null)
                                {
                                    //That's a free writing
                                    answer = GetFreeInputOf(question);
                                }
                                else
                                {
                                    //We have some options to select from
                                    answer = GetSelectionOf(question, subProperty.Value, 1);
                                }

                                newAnswerProperty.Add(question, answer);
                            }

                        }

                        nextInfo_answers.Add(generalProperty.Key, newAnswerProperty);
                    }

                    m_Garage.SetNewVehicleData(licensePlate, nextInfo_answers);

                    Console.WriteLine("Vehicle added successfully");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Adding vehicle has failed\nError: {e.Message}");
                }

            }

            return AgainOrBack("Add a different vehicle");
        }

        private static bool ManageListVehicles()
        {
            string[] filters = new string[4];
            m_Garage.StatusTypes().CopyTo(filters, 0);
            filters[3] = "None";

            string filter = GetSelectionOf("Filter by current status", filters, 0);

            List<string> licensePlates = m_Garage.ListOfLicenseNumbers(filter);

            if (licensePlates.Count == 0)
            {
                Console.WriteLine("Sorry, there are no vehicles in here.");
            }

            foreach (string licensePlayer in m_Garage.ListOfLicenseNumbers(filter))
            {
                Console.WriteLine(licensePlayer);
            }

            return AgainOrBack("Get a different list of vehicles");
        }

        private static bool ManageChangeStatus()
        {
            Console.WriteLine("Vehicle License:");
            string licensePlate = Console.ReadLine();

            if (m_Garage.IsExists(licensePlate))
            {
                m_Garage.ChangeStatus(licensePlate, GetSelectionOf("Change status to", m_Garage.StatusTypes(), 0));

                Console.WriteLine("Changed vehicle status successfully.");
            }
            else
            {
                Console.WriteLine("Vehicle is not exist.");
            }

            return AgainOrBack("Change the status the a different vehicle");
        }

        private static bool ManageInflateToMax()
        {
            Console.WriteLine("Vehicle License:");
            string licensePlate = Console.ReadLine();

            if (m_Garage.IsExists(licensePlate))
            {
                m_Garage.InflateTiresToMax(licensePlate);

                Console.WriteLine("Seted the tires pressure to maximum successfully.");
            }
            else
            {
                Console.WriteLine("Vehicle is not exist.");
            }

            return AgainOrBack("Set tire's pressure to max in a different vehicle");
        }
        
        private static bool ManageRefule()
        {
            Console.WriteLine("Vehicle License:");
            string licensePlate = Console.ReadLine();

            if (m_Garage.IsExists(licensePlate))
            {
                string fuelType = GetSelectionOf("Fuel type", m_Garage.StatusTypes(), 0);
                float fuelAmount = GetFreeInputFloat("amount of fuel (in liters)");

                m_Garage.Refule(licensePlate, fuelType, fuelAmount);

                Console.WriteLine("Refuled successfully.");
            }
            else
            {
                Console.WriteLine("Vehicle is not exist.");
            }

            return AgainOrBack("Refule a different vehicle");
        }

        private static bool ManageRecharge()
        {
            Console.WriteLine("Vehicle License:");
            string licensePlate = Console.ReadLine();

            if (m_Garage.IsExists(licensePlate))
            {
                float batteryDuration = GetFreeInputFloat("how many hours to add to the battery");

                m_Garage.Recharge(licensePlate, batteryDuration);

                Console.WriteLine("Recharged successfully.");
            }
            else
            {
                Console.WriteLine("Vehicle is not exist.");
            }

            return AgainOrBack("Refule a different vehicle");
        }

        private static bool ManageInformation()
        {
            Console.WriteLine("Vehicle License:");
            string licensePlate = Console.ReadLine();

            if (m_Garage.IsExists(licensePlate))
            {
                PrintVehicleInformation(licensePlate);

                Console.WriteLine("\nDisplayed information successfully.");
            }
            else
            {
                Console.WriteLine("Vehicle is not exist.");
            }

            return AgainOrBack("Refule a different vehicle");
        }

        private static void PrintVehicleInformation(string i_LicensePlate)
        {
            foreach (var generalProperty in m_Garage.GetInformation(i_LicensePlate))
            {
                Console.WriteLine($"\n{generalProperty.Key}:");

                foreach (var subProperty in generalProperty.Value)
                {

                    foreach (var oneQuestion in subProperty.Key)
                    {
                        Console.WriteLine($"\t{oneQuestion.Key}: {oneQuestion.Value}");
                    }
                }
            }
        }

        private static void PrintHeadLine(string i_HeadLine, bool i_NewLine)
        {
            const int lineLength = 32;

            int length = i_HeadLine.Length;
            int countOfHyphenSide = (lineLength - (length + 2)) / 2;

            StringBuilder line = new StringBuilder();

            line.Append(new String('-', countOfHyphenSide));
            line.Append($" {i_HeadLine} ");
            line.Append(new String('-', countOfHyphenSide));

            line.Append(i_NewLine ? '\n' : '\0');

            Console.WriteLine($"\n" + line.ToString());
        }

        private static string GetFreeInputOf(string i_Title)
        {
            Console.WriteLine($"\tEnter {i_Title}:");
            Console.Write("\t");
            string userInput = Console.ReadLine();

            while (Regex.Matches(userInput, @"[0-9a-zA-Z]").Count == 0)
            {
                Console.WriteLine("Please enter some value.");
                Console.Write("\t");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private static string GetPhoneNumber()
        {
            Console.WriteLine($"\tEnter Phone Number: (XXX-XXXXXXX)");
            Console.Write("\t");
            string userInput = Console.ReadLine();

            Regex phoneNumberRegex = new Regex(@"[0-9]{3}-[0-9]{7}");

            while (!phoneNumberRegex.Match(userInput).Success)
            {
                Console.WriteLine("Input invalid, check your phone number again.");
                Console.Write("\t");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private static string GetSelectionOf(string i_Title, string[] i_Selections, int i_Tabs)
        {
            PrintOpertaions(i_Title, i_Selections, i_Tabs, ':');
            return i_Selections[ReadUserSelection(i_Selections.Length) - 1];
        }

        private static int ReadUserSelection(int i_MaxOption)
        {
            Console.Write("\nSelect Opertaion, ");

            char keyEntered = Console.ReadKey().KeyChar;
            while (!InputOptionIsValid(keyEntered, i_MaxOption)) 
            {
                Console.WriteLine("\nWrong operation input, please try again...\n(look at the values next to the wanted operation)");
                keyEntered = Console.ReadKey().KeyChar;
            }

            Console.WriteLine("\n");

            return int.Parse(keyEntered.ToString());
        }

        private static float GetFreeInputFloat(string i_Title)
        {
            Console.WriteLine($"\tEnter {i_Title}:");
            Console.Write("\t");
            string userInput = Console.ReadLine();

            while (!float.TryParse(userInput, out _))
            {
                Console.WriteLine("Input is not valid, please try again.");
                Console.Write("\t");
                userInput = Console.ReadLine();
            }

            return float.Parse(userInput);
        }

        private static bool InputOptionIsValid(char i_KeyEntered, int i_MaxOption)
        {
            return int.TryParse(i_KeyEntered.ToString(), out _) && int.Parse(i_KeyEntered.ToString()) >= 1 && int.Parse(i_KeyEntered.ToString()) <= i_MaxOption;
        }

        public static void PrintOpertaions(string i_Title, string[] i_Operations, int i_CountOfTabs, char i_Seperator)
        {
            string tabsStart = new string('\t', i_CountOfTabs);

            Console.WriteLine($"{tabsStart}{i_Title}{i_Seperator}");

            if (i_Operations != null)
            {
                for (int i = 0; i < i_Operations.Length; i++)
                {
                    Console.WriteLine($"{tabsStart}\t{i + 1}. {i_Operations[i]}");
                }
            }
        }

        public static void PrintDictionary(Dictionary<string, Dictionary<string, string[]>> i_Dictionary)
        {
            foreach (var item in i_Dictionary)
            {
                Console.WriteLine("\n" + item.Key + ":");

                foreach (var item2 in item.Value)
                {
                    //PrintOpertaions(item2.Key, item2.Value);
                }
            }
        }
    }
}
