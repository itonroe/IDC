using System;
using System.Collections.Generic;
using System.Text;
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

		public static string[] GetVehicleTypes()
		{
			return Enum.GetNames(typeof(eVehiclesTypes));
		}

		private static bool IsVehicleValid(string i_VehicleType, string i_EngineType)
		{
			return IsVehicleTypeValid(i_VehicleType) && IsEngineTypeValid(i_EngineType);
		}

		private static bool IsVehicleValid(string i_VehicleType, string i_EngineType, string i_LicenseNumber)
		{
			return IsVehicleValid(i_VehicleType, i_EngineType) && IsLicenseNumberValid(i_LicenseNumber);
		}

		private static bool IsVehicleTypeValid(string i_VehicleType)
		{
			return Enum.IsDefined(typeof(eVehiclesTypes), i_VehicleType);
		}

		private static bool IsEngineTypeValid(string i_EngineType)
		{
			return Enum.IsDefined(typeof(eEngineTypes), i_EngineType);
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
	}
}