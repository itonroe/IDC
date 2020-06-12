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

        public Car(string i_LicensePlate, eEngineTypes i_EngineType) :
            base(i_LicensePlate, i_EngineType)
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

        public override Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> PropertiesToDictionary()
        {
            Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> properties = base.PropertiesToDictionary();

            Dictionary<Dictionary<string, string>, string[]> properties_car = new Dictionary<Dictionary<string, string>, string[]>();

            Dictionary<string, string> carColor = new Dictionary<string, string>();
            carColor.Add("Color", m_Color.ToString());

            Dictionary<string, string> numOfDoors = new Dictionary<string, string>();
            numOfDoors.Add("Number of Doors", m_NumOfDoors.ToString());

            properties_car.Add(carColor, Enum.GetNames(typeof(eCarColor)));
            properties_car.Add(numOfDoors, Enum.GetNames(typeof(eCarNumOfDoors)));

            properties.Add("Car", properties_car);

            return properties;
        }

        public override eVehiclesTypes GetVehicleType()
        {
            return eVehiclesTypes.Car;
        }
    }
}
