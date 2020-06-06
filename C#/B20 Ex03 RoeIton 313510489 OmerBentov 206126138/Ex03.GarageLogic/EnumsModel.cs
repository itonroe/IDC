using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
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
}
