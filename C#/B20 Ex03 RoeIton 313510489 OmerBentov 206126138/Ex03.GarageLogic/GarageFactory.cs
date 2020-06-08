using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.EnumsModel;

namespace Ex03.GarageLogic
{
	public static class GarageFactory
	{
		//Build Vehicle by arguments, and return it.
		public static Vehicle GetVehicle(string i_VehicleType, string i_EngineType, string i_LicenseNumber)
		{
			Vehicle vehicle = null;

			if (IsVehicleValid(i_VehicleType, i_EngineType, i_LicenseNumber))
			{
				eVehiclesTypes vehicleType = (eVehiclesTypes)Enum.Parse(typeof(eVehiclesTypes), i_VehicleType);
				eEngineTypes engineType = (eEngineTypes) Enum.Parse(typeof(eEngineTypes), i_EngineType);

				switch (vehicleType)
				{
					case eVehiclesTypes.Car:
						vehicle = new Car(i_LicenseNumber, engineType);
						break;
					case eVehiclesTypes.Motorcycle:
						vehicle = new Motorcycle(i_LicenseNumber, engineType);
						break;
					case eVehiclesTypes.Truck:
						vehicle = new Truck(i_LicenseNumber, engineType);
						break;
				}
			}

			return vehicle;
		}

		public static void UpdateVehicle(ref Vehicle i_Vehicle, string i_Model)
		{
			i_Vehicle.Model = i_Model;
		}

		public static void UpdateWheels(ref Vehicle i_Vehicle, string i_Manufacturer, string i_CurrentAirPressure)
		{
			if (!float.TryParse(i_CurrentAirPressure, out _))
			{
				throw new FormatException("Current air pressure in wheels is not valid.\n(Isn't a number or exceed the maximum air pressure of the wheel)");
			}

			i_Vehicle.UpdateWheels(i_Manufacturer, float.Parse(i_CurrentAirPressure));
		}

		public static void UpdateCar(ref Vehicle i_Vehicle, string i_Color, string i_NumOfDoors)
		{
			IsCarPropertiesValid(i_Color, i_NumOfDoors);

			eCarColor carColor = (eCarColor)Enum.Parse(typeof(eCarColor), i_Color);
			eCarNumOfDoors carNumOfDoors = (eCarNumOfDoors)Enum.Parse(typeof(eCarNumOfDoors), i_NumOfDoors);

			((Car)i_Vehicle).Color = carColor;
			((Car)i_Vehicle).NumOfDoors = carNumOfDoors;
		}

		public static void UpdateMotorcycle(ref Vehicle i_Vehicle, string i_LicenseType, string i_EngineCapacity)
		{
			IsMotorcyclePropertiesValid(i_LicenseType, i_EngineCapacity);
			eMotorcycleLicenseTypes motorcycleLicenseType = (eMotorcycleLicenseTypes)Enum.Parse(typeof(eMotorcycleLicenseTypes), i_LicenseType);
			int engineCapacity = int.Parse(i_EngineCapacity);

			((Motorcycle)i_Vehicle).LicenseType = motorcycleLicenseType;
			((Motorcycle)i_Vehicle).EngineCapacity = engineCapacity;
		}

		public static void UpdateTruck(ref Vehicle i_Vehicle, string i_DangerousLoad, string i_LoadVolume)
		{
			IsTruckPropertiesValid(i_DangerousLoad, i_LoadVolume);

			bool dangerousLoad = bool.Parse(i_DangerousLoad);
			float loadVolume = float.Parse(i_LoadVolume);

			((Truck)i_Vehicle).DangerousLoad = dangerousLoad;
			((Truck)i_Vehicle).LoadVolume = loadVolume;
		}

		public static void UpdateFuelEngine(ref Vehicle i_Vehicle, string i_FuelType, string i_CurrentFuelTank)
		{
			IsFuelEnginePropertiesValid(i_FuelType, i_CurrentFuelTank);

			eFuelTypes fuelType = (eFuelTypes)Enum.Parse(typeof(eFuelTypes), i_FuelType);
			float currentFuelTank = float.Parse(i_CurrentFuelTank);

			((Fuel)i_Vehicle.Engine).FuelType = fuelType;
			((Fuel)i_Vehicle.Engine).Refule(currentFuelTank, fuelType);
		}

		public static void UpdateElectricEngine(ref Vehicle i_Vehicle, string i_BatteryDurationLeft)
		{
			IsElectricEnginePropertiesValid(i_BatteryDurationLeft);

			float batteryDurationLeft = float.Parse(i_BatteryDurationLeft);

			((Electric)i_Vehicle.Engine).Recharge(batteryDurationLeft);
		}

		public static bool UpdateVehicle(ref Vehicle i_Vehicle, Dictionary<string, Dictionary<string, string>> i_Data)
		{
			bool success = true;
			try
			{
				foreach (var field in i_Data)
				{
					switch (field.Key)
					{
						case "Vehicle":
							string modelName = string.Empty;

							foreach (var property in field.Value)
							{
								modelName = property.Value;
							}

							UpdateVehicle(ref i_Vehicle, modelName);
							break;
						case "Wheels":
							string manufacturer = string.Empty;
							string currentAirPressure = string.Empty;

							foreach (var property in field.Value)
							{
								if (property.Key.ToLower().Contains("manufacturer"))
								{
									manufacturer = property.Value;
								}
								else
								{
									currentAirPressure = property.Value;
								}
							}

							UpdateWheels(ref i_Vehicle, manufacturer, currentAirPressure);
							break;
						case "Engine":

							switch (i_Vehicle.Engine.EngineType)
							{
								case eEngineTypes.Electric:
									string batteryDurationaLeft = string.Empty;

									foreach (var property in field.Value)
									{
										batteryDurationaLeft = property.Value;
									}

									UpdateElectricEngine(ref i_Vehicle, batteryDurationaLeft);
									break;
								case eEngineTypes.Fuel:
									string fuelType = string.Empty;
									string currentAmount = string.Empty;

									foreach (var property in field.Value)
									{
										if (property.Key.ToLower().Contains("type"))
										{
											fuelType = property.Value;
										}
										else
										{
											currentAmount = property.Value;
										}
									}

									UpdateFuelEngine(ref i_Vehicle, fuelType, currentAmount);
									break;
							}
 							break;
						case "Car":
							string color = string.Empty;
							string numOfDoors = string.Empty;

							foreach (var property in field.Value)
							{
								if (property.Key.ToLower().Contains("color"))
								{
									color = property.Value;
								}
								else
								{
									numOfDoors = property.Value;
								}
							}

							UpdateCar(ref i_Vehicle, color, numOfDoors);
							break;
						case "Truck":
							string dangerousLoad = string.Empty;
							string loadVolume = string.Empty;

							foreach (var property in field.Value)
							{
								if (property.Key.ToLower().Contains("volume"))
								{
									loadVolume = property.Value;
								}
								else
								{
									dangerousLoad = property.Value;
								}
							}

							UpdateTruck(ref i_Vehicle, dangerousLoad, loadVolume);
							break;
						case "Motorcycle":
							string licenseType = string.Empty;
							string engineCapacity = string.Empty;

							foreach (var property in field.Value)
							{
								if (property.Key.ToLower().Contains("license"))
								{
									licenseType = property.Value;
								}
								else
								{
									engineCapacity = property.Value;
								}
							}

							UpdateMotorcycle(ref i_Vehicle, licenseType, engineCapacity);
							break;
					}
				}
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.InnerException);
				success = false;
			}

			return success;
		}

		public static string[] GetVehicleTypes()
		{
			return Enum.GetNames(typeof(eVehiclesTypes));
		}

		public static string[] GetEngineTypes() 
		{
			return Enum.GetNames(typeof(eEngineTypes));
		}

		public static string[] GetStatusTypes()
		{
			return Enum.GetNames(typeof(eVehicleStatus));
		}

		private static bool IsVehicleValid(string i_VehicleType, string i_EngineType)
		{
			return IsValueTypeValid(new eVehiclesTypes(), i_VehicleType) && IsValueTypeValid(new eEngineTypes(), i_EngineType);
		}

		private static void IsCarPropertiesValid(string i_Color, string i_NumOfDoors)
		{
			if (!IsValueTypeValid(new eCarColor(), i_Color) || !IsValueTypeValid(new eCarNumOfDoors(), i_NumOfDoors))
			{
				throw new FormatException("Car properties aren't valid, please try again.");
			}
		}

		private static void IsMotorcyclePropertiesValid(string i_LicenseType, string i_EngineCapacity)
		{
			if (!IsValueTypeValid(new eMotorcycleLicenseTypes(), i_LicenseType) || !int.TryParse(i_EngineCapacity, out _))
			{
				throw new FormatException("Engine capacity needs to be a number.");
			}
		}

		private static void IsTruckPropertiesValid(string i_DangerousLoad, string i_LoadVolume)
		{
			if (!bool.TryParse(i_DangerousLoad, out _) || !float.TryParse(i_LoadVolume, out _))
			{
				throw new FormatException("Dangerous load and Load volume, needs to be a number.");
			}
		}

		private static void IsFuelEnginePropertiesValid(string i_FuelType, string i_CurrentFuelTank)
		{
			if (!IsValueTypeValid(new eFuelTypes(), i_FuelType) || !float.TryParse(i_CurrentFuelTank, out _))
			{
				throw new FormatException("Current fuel amount needs to be a number.");
			}
		}

		private static void IsElectricEnginePropertiesValid(string i_BatteryDurationLeft)
		{
			if (!float.TryParse(i_BatteryDurationLeft, out _))
			{
				throw new FormatException("Battery duration left needs to be a number.");
			}
		}

		private static bool IsVehicleValid(string i_VehicleType, string i_EngineType, string i_LicenseNumber)
		{
			return IsVehicleValid(i_VehicleType, i_EngineType) && IsLicenseNumberValid(i_LicenseNumber);
		}

		public static bool IsValueTypeValid(Enum i_EnumType, string i_Value)
		{
			return Enum.IsDefined(i_EnumType.GetType(), i_Value);
		}

		private static bool IsLicenseNumberValid(string i_LicenseNumber)
		{
			return IsAllDigits(i_LicenseNumber);
		}

		private static bool IsAllDigits(string i_String)
		{
			bool isAllDigits = true;

			foreach (char charInString in i_String)
			{
				if (!char.IsDigit(charInString))
				{
					isAllDigits = false;
					break;
				}
			}

			return isAllDigits;
		}


		/*private static bool IsVehicleTypeValid(string i_VehicleType)
		{
			return Enum.IsDefined(typeof(eVehiclesTypes), i_VehicleType);
		}

		private static bool IsEngineTypeValid(string i_EngineType)
		{
			return Enum.IsDefined(typeof(eEngineTypes), i_EngineType);
		}

		private static bool IsColorTypeValid(string i_Color)
		{
			return Enum.IsDefined(typeof(eCarColor), i_Color);
		}

		private static bool IsNumOfDoorsTypeValid(string i_NumOfDoors)
		{
			return Enum.IsDefined(typeof(eCarNumOfDoors), i_NumOfDoors);
		}*/
	}
}