using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        private enum eViews
        {
            Menu = 0,
            Add = 1,
            List = 2,
            Status = 3,
            InflateMax = 4,
            Refule = 5,
            Recharge = 6,
            Information = 7,
            Exit = 8
        }

        private static Garage m_Garage;
        private static eViews m_CurrentView;

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

        public GarageUI()
        {
            m_Garage = new Garage();
        }

        public void StartApplication()
        {
            while (true)
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
            m_CurrentView = eViews.Menu;

            string[] opertaions = new string[] 
            { 
                "Add a vehicle", 
                "Get a list of vehicles in the garage", "Change a vehicle status", 
                "Inflate vehicle's tires to max", "Refule vehicle", "Recharge vehicle", "Vehicle Information", "Exit" 
            };

            m_CurrentView = (eViews)(Array.IndexOf(opertaions, GetSelectionOf("Menu", opertaions, 0)) + 1);

            Console.Clear();

            ManageViewForm();
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

                Console.WriteLine("\nJust letting you know, vehicle's status changed to 'In Progress'.");
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
                string ownerPhoneNumber = GetFreeInputOf("Phone Number: (XXX-XXXXXXX)", "phone");

                m_Garage.SetVehicleOwner(licensePlate, ownerName, ownerPhoneNumber);

                bool wantToAddVehicle = true;

                while (wantToAddVehicle) 
                {
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

                                    if (nextInfo_answers.TryGetValue(generalProperty.Key, out _))
                                    {
                                        Console.WriteLine($"\t{oneQuestion.Key}: {nextInfo_answers[generalProperty.Key][question]}");
                                    }
                                    else
                                    {
                                        if (subProperty.Value == null)
                                        {
                                            // That's a free writing
                                            answer = GetFreeInputOf(question);
                                        }
                                        else
                                        {
                                            // We have some options to select from
                                            answer = GetSelectionOf(question, subProperty.Value, 1);
                                        }

                                        newAnswerProperty.Add(question, answer);
                                    }
                                }
                            }

                            if (!nextInfo_answers.TryGetValue(generalProperty.Key, out _))
                            {
                                nextInfo_answers.Add(generalProperty.Key, newAnswerProperty);
                            }
                            else
                            {
                                nextInfo_answers.Remove(generalProperty.Key);
                            }
                        }

                        m_Garage.SetNewVehicleData(licensePlate, ref nextInfo_answers);

                        Console.WriteLine("Vehicle added successfully");
                        wantToAddVehicle = false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        wantToAddVehicle = AgainOrBack("Try again to add this vehicle");
                    }
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

            List<string> licensePlates = m_Garage.ListOfLicensePlates(filter);

            if (licensePlates.Count == 0)
            {
                Console.WriteLine("Sorry, there are no vehicles in here.");
            }

            foreach (string licensePlayer in m_Garage.ListOfLicensePlates(filter))
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
                try
                {
                    string fuelType = GetSelectionOf("Fuel type", m_Garage.FuelTypes(), 0);
                    float fuelAmount = float.Parse(GetFreeInputOf("amount of fuel (in liters)", "float"));

                    m_Garage.Refule(licensePlate, fuelType, fuelAmount);

                    Console.WriteLine("Refuled successfully.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Vehicle is not exist.");
            }

            return AgainOrBack("Refule a vehicle");
        }

        private static bool ManageRecharge()
        {
            Console.WriteLine("Vehicle License:");
            string licensePlate = Console.ReadLine();

            if (m_Garage.IsExists(licensePlate))
            {
                try
                {
                    float batteryDuration = float.Parse(GetFreeInputOf("how many hours to add to the battery", "float"));

                    m_Garage.Recharge(licensePlate, batteryDuration);

                    Console.WriteLine("Recharged successfully.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Vehicle is not exist.");
            }

            return AgainOrBack("Recharge a vehicle");
        }

        private static void ManageViewForm()
        {
            switch (m_CurrentView)
            {
                case eViews.Add:
                    while (ManageAddVehicle());
                    break;
                case eViews.List:
                    while (ManageListVehicles());
                    break;
                case eViews.Status:
                    while (ManageChangeStatus());
                    break;
                case eViews.InflateMax:
                    while (ManageInflateToMax());
                    break;
                case eViews.Refule:
                    while (ManageRefule());
                    break;
                case eViews.Recharge:
                    while (ManageRecharge());
                    break;
                case eViews.Information:
                    while (ManageInformation());
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
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
            return GetFreeInputOf(i_Title, String.Empty);
        }

        private static string GetFreeInputOf(string i_Title, string i_ValueType)
        {
            Console.WriteLine($"\tEnter {i_Title}:");
            Console.Write("\t");

            string userInput = Console.ReadLine();
            bool inputValid = InputValidation(i_ValueType, userInput);

            while (!inputValid)
            {
                string userAgainMessage;

                switch (i_ValueType)
                {
                    case "float":
                        userAgainMessage = "numbers only";
                        break;
                    case "phone":
                        userAgainMessage = "check your phone number again.";
                        break;
                    default:
                        userAgainMessage = "Please enter some value.";
                        break;
                }

                Console.WriteLine($"Input invalid {userAgainMessage}");
                Console.Write("\t");
                userInput = Console.ReadLine();
                inputValid = InputValidation(i_ValueType, userInput);
            }

            return userInput;
        }

        private static bool InputValidation(string i_ValueType, string i_Value)
        {
            bool isValid = true;

            switch (i_ValueType.ToLower())
            {
                case "float":
                    isValid = float.TryParse(i_Value, out _);
                    break;
                case "phone":
                    Regex phoneNumberRegex = new Regex(@"[0-9]{3}-[0-9]{7}");

                    isValid = phoneNumberRegex.Match(i_Value).Success;
                    break;
                default:
                    isValid = Regex.Matches(i_Value, @"[0-9a-zA-Z]").Count != 0;
                    break;
            }

            return isValid;
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

        private static bool InputOptionIsValid(char i_KeyEntered, int i_MaxOption)
        {
            return int.TryParse(i_KeyEntered.ToString(), out _) && int.Parse(i_KeyEntered.ToString()) >= 1 && int.Parse(i_KeyEntered.ToString()) <= i_MaxOption;
        }
    }
}
