using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.GarageFactory.EnumsModel;

namespace Ex03.GarageLogic
{
	public static class GarageFactory
	{
		public class EnumsModel
		{
			public enum eVehiclesTypes
			{
				Car,
				Motorcycle,
				Truck
			}

			public enum eEngineTypes
			{
				Fuel,
				Electric
			}

			public enum eVehicleStatus
			{
				InProgress,
				Fixed,
				Payed
			}

			public enum eFuelTypes
			{
				Octan95,
				Octan98,
				Octan96,
				Soler
			}

			public enum eFuelMotorcycle
			{
				Wheels = 2,
				MaxAirPressure = 30,
				FuelType = eFuelTypes.Octan95,
				FuelCapacity = 7,
			}

			public enum eElectricMotorcycle
			{
				Wheels = 2,
				MaxAirPressure = 30,
				MaxBatteryDuration = 72,
			}

			public enum eFuelCar
			{
				Wheels = 4,
				MaxAirPressure = 32,
				FuelType = eFuelTypes.Octan96,
				FuelCapacity = 60,
			}

			public enum eElectricCar
			{
				Wheels = 2,
				MaxAirPressure = 32,
				MaxBatteryDuration = 126,
			}

			public enum eRegularTruck
			{
				Wheels = 16,
				MaxAirPressure = 28,
				FuelType = eFuelTypes.Soler,
				FuelCapacity = 120,
			}

			public enum eCarColor
			{
				Red,
				White,
				Black,
				Silver
			}

			public enum eCarNumOfDoors
			{
				Two = 2,
				Three = 3,
				Four = 4,
				Five = 5
			}

			public enum eMotorcycleLicenseTypes
			{
				A,
				A1,
				AA,
				B
			}

		}

		public static Vehicle GetVehicle(string i_VehicleType, string i_EngineType, string i_LicensePlate)
		{
			Vehicle vehicle = null;

			eVehiclesTypes vehicleType = (eVehiclesTypes)Enum.Parse(typeof(eVehiclesTypes), i_VehicleType);
			eEngineTypes engineType = (eEngineTypes)Enum.Parse(typeof(eEngineTypes), i_EngineType);

			switch (vehicleType)
			{
				case eVehiclesTypes.Car:
					vehicle = new Car(i_LicensePlate, engineType);
					break;
				case eVehiclesTypes.Motorcycle:
					vehicle = new Motorcycle(i_LicensePlate, engineType);
					break;
				case eVehiclesTypes.Truck:
					vehicle = new Truck(i_LicensePlate, engineType);
					break;
			}

			return vehicle;
		}

		public static bool IsValueTypeValid(Enum i_EnumType, string i_Value)
		{
			return Enum.IsDefined(i_EnumType.GetType(), i_Value);
		}

		public static string[] GetEnumTypes(Enum i_Enum)
		{
			return Enum.GetNames(i_Enum.GetType());
		}

		public static void UpdateVehicle(ref Vehicle i_Vehicle, string i_Model)
		{
			i_Vehicle.Model = i_Model;
		}

		public static void UpdateWheels(ref Vehicle i_Vehicle, string i_Manufacturer, string i_CurrentAirPressure)
		{
			ParserValidator("float", "Current air pressure in wheels is not valid.\n(Isn't a number or exceed the maximum air pressure of the wheel)", i_CurrentAirPressure);

			i_Vehicle.UpdateWheels(i_Manufacturer, float.Parse(i_CurrentAirPressure));
		}

		public static void UpdateCar(ref Vehicle i_Vehicle, string i_Color, string i_NumOfDoors)
		{
			eCarColor carColor = (eCarColor)Enum.Parse(typeof(eCarColor), i_Color);
			eCarNumOfDoors carNumOfDoors = (eCarNumOfDoors)Enum.Parse(typeof(eCarNumOfDoors), i_NumOfDoors);

			((Car)i_Vehicle).Color = carColor;
			((Car)i_Vehicle).NumOfDoors = carNumOfDoors;
		}

		public static void UpdateMotorcycle(ref Vehicle i_Vehicle, string i_LicenseType, string i_EngineCapacity)
		{
			ParserValidator("float", "Engine capacity needs to be a number.", i_EngineCapacity);
			eMotorcycleLicenseTypes motorcycleLicenseType = (eMotorcycleLicenseTypes)Enum.Parse(typeof(eMotorcycleLicenseTypes), i_LicenseType);
			int engineCapacity = int.Parse(i_EngineCapacity);

			if (engineCapacity < 0)
			{
				throw new ValueOutOfRangeException("Engine capacity can't be negative.");
			}

			((Motorcycle)i_Vehicle).LicenseType = motorcycleLicenseType;
			((Motorcycle)i_Vehicle).EngineCapacity = engineCapacity;
		}

		public static void UpdateTruck(ref Vehicle i_Vehicle, bool i_DangerousLoad, string i_LoadVolume)
		{
			ParserValidator("float", "Load volume needs to be a number.", i_LoadVolume);

			float loadVolume = float.Parse(i_LoadVolume);

			((Truck)i_Vehicle).DangerousLoad = i_DangerousLoad;
			((Truck)i_Vehicle).LoadVolume = loadVolume;
		}

		public static void UpdateFuelEngine(ref Vehicle i_Vehicle, string i_FuelType, string i_CurrentFuelTank)
		{
			ParserValidator("float", "Current fuel amount needs to be a number.", i_CurrentFuelTank);

			eFuelTypes fuelType = (eFuelTypes)Enum.Parse(typeof(eFuelTypes), i_FuelType);
			float currentFuelTank = float.Parse(i_CurrentFuelTank);

			((Fuel)i_Vehicle.Engine).FuelType = fuelType;
			((Fuel)i_Vehicle.Engine).Refule(currentFuelTank, fuelType);
		}

		public static void UpdateElectricEngine(ref Vehicle i_Vehicle, string i_BatteryDurationLeft)
		{
			ParserValidator("float", "Battery duration left needs to be a number.", i_BatteryDurationLeft);

			float batteryDurationLeft = float.Parse(i_BatteryDurationLeft);

			((Electric)i_Vehicle.Engine).Recharge(batteryDurationLeft);
		}

		private static void ParserValidator(string i_Type, string i_ErrorMessage, string i_Value)
		{
			switch (i_Type.ToLower())
			{
				case "int":
					if (!int.TryParse(i_Value, out _))
					{
						throw new FormatException(i_ErrorMessage);
					}

					break;
				case "bool":
					if (!bool.TryParse(i_Value, out _))
					{
						throw new FormatException(i_ErrorMessage);
					}

					break;
				case "float":
					if (!float.TryParse(i_Value, out _))
					{
						throw new FormatException(i_ErrorMessage);
					}

					break;
			}
		}

		public static Dictionary<string, Dictionary<string, string>> UpdateVehicleData(ref Vehicle i_Vehicle, ref Dictionary<string, Dictionary<string, string>> i_Data)
		{
			Dictionary<string, Dictionary<string, string>> responseData = new Dictionary<string, Dictionary<string, string>>();

			StringBuilder errorMessage = new StringBuilder();

			foreach (var field in i_Data)
			{
				switch (field.Key)
				{
					case "Vehicle":
						string modelName = field.Value["Model Name"];

						try
						{
							UpdateVehicle(ref i_Vehicle, modelName);
							responseData.Add("Vehicle", field.Value);
						}
						catch (Exception e)
						{
							errorMessage.AppendLine(e.Message);
						}

						break;
					case "Wheels":
						string manufacturer = field.Value["Manufacturer Name"];
						string currentAirPressure = field.Value["Current Wheel Air Pressure"];
						try
						{
							UpdateWheels(ref i_Vehicle, manufacturer, currentAirPressure);
							responseData.Add("Wheels", field.Value);
						}
						catch (Exception e)
						{
							errorMessage.AppendLine(e.Message);
						}

						break;
					case "Engine":
						try
						{
							switch (i_Vehicle.Engine.EngineType)
							{
								case eEngineTypes.Electric:
									string batteryDurationaLeft = field.Value["Battery Duration Left"];

									UpdateElectricEngine(ref i_Vehicle, batteryDurationaLeft);
									responseData.Add("Engine", field.Value);

									break;
								case eEngineTypes.Fuel:
									string fuelType = field.Value["Fuel Type"];
									string currentAmount = field.Value["Current amount of Fuel"];

									UpdateFuelEngine(ref i_Vehicle, fuelType, currentAmount);
									responseData.Add("Engine", field.Value);
									break;
							}
						}
						catch (Exception e)
						{
							errorMessage.AppendLine(e.Message);
						}
						break;
					case "Car":
						string color = field.Value["Color"];
						string numOfDoors = field.Value["Number of Doors"];

						try
						{
							UpdateCar(ref i_Vehicle, color, numOfDoors);
							responseData.Add("Car", field.Value);
						}
						catch (Exception e)
						{
							errorMessage.AppendLine(e.Message);
						}

						break;
					case "Truck":
						bool dangerousLoad = field.Value["Does it carry dangerous load?"].Equals("Yes") ? true : false;
						string loadVolume = field.Value["Load Volume"];

						try
						{
							UpdateTruck(ref i_Vehicle, dangerousLoad, loadVolume);
							responseData.Add("Truck", field.Value);
						}
						catch (Exception e)
						{
							errorMessage.AppendLine(e.Message);
						}

						break;
					case "Motorcycle":
						string licenseType = field.Value["License Type"];
						string engineCapacity = field.Value["Engine Capacity"];

						try
						{
							UpdateMotorcycle(ref i_Vehicle, licenseType, engineCapacity);
							responseData.Add("Motorcycle", field.Value);
						}
						catch (Exception e)
						{
							errorMessage.AppendLine(e.Message);
						}

						break;
				}
			}

			i_Data = responseData;

			if (errorMessage.Length != 0)
			{
				Console.WriteLine($"Errors:\n{errorMessage.ToString()}");
				throw new Exception("Adding vehicle has Failed");
			}

			return responseData;
		}
	}
}