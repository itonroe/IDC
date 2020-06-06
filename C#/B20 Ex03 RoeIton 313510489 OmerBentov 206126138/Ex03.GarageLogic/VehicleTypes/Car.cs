using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.EnumsModel;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eCarColor m_Color;
        private eCarNumOfDoors m_NumOfDoors;

        public eCarColor Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }
        }

        public eCarNumOfDoors NumOfDoors
        {
            get
            {
                return m_NumOfDoors;
            }
            set
            {
                m_NumOfDoors = value;
            }
        }

        public Car(string i_LicenseNumber, eEngineTypes i_EngineType) :
            base(i_LicenseNumber, i_EngineType)
        {

            switch (i_EngineType)
            {
                case eEngineTypes.Fuel:
                    SetWheels((int)eFuelCar.Wheels, (float)eFuelCar.MaxAirPressure);
                    ((Fuel)Engine).FuelType = (eFuelTypes)eFuelCar.FuelType;
                    ((Fuel)Engine).MaxFuelTank = (float)eFuelCar.FuelCapacity;
                    break;
                case eEngineTypes.Electric:
                    SetWheels((int)eElectricCar.Wheels, (float)eElectricCar.MaxAirPressure);
                    ((Electric)Engine).MaxBatteryDuration = (float)eElectricCar.MaxBatteryDuration;
                    break;
                default:
                    break;
            }
        }

        public override Dictionary<string, Dictionary<string, string[]>> PropertiesToDictionary()
        {
            Dictionary<string, Dictionary<string, string[]>> properties = base.PropertiesToDictionary();

            Dictionary<string, string[]> properties_car = new Dictionary<string, string[]>();
            properties_car.Add("Color", Enum.GetNames(typeof(eCarColor)));
            properties_car.Add("Number of Doors", Enum.GetNames(typeof(eCarNumOfDoors)));

            properties.Add("Car", properties_car);

            return properties;
        }

        public override eVehiclesTypes GetVehicleType()
        {
            return eVehiclesTypes.Car;
        }
    }
}
