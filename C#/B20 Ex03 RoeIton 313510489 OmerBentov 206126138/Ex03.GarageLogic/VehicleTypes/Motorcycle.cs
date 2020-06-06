using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.EnumsModel;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eMotorcycleLicenseTypes m_LicenseType;
        private int m_EngineCapacity;

        public eMotorcycleLicenseTypes LicenseType
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                m_LicenseType = value;
            }
        }
        public int EngineCapacity 
        {
            get
            {
                return m_EngineCapacity;
            }
            set
            {
                m_EngineCapacity = value;
            }
        }

        public Motorcycle(string i_LicenseNumber, eEngineTypes i_EngineType) :
            base(i_LicenseNumber, i_EngineType)
        {

            switch (i_EngineType)
            {
                case eEngineTypes.Fuel:
                    SetWheels((int)eFuelMotorcycle.Wheels, (float)eFuelMotorcycle.MaxAirPressure);
                    ((Fuel)Engine).FuelType = (eFuelTypes)eFuelMotorcycle.FuelType;
                    ((Fuel)Engine).MaxFuelTank = (float)eFuelMotorcycle.FuelCapacity;
                    break;
                case eEngineTypes.Electric:
                    SetWheels((int)eElectricMotorcycle.Wheels, (float)eElectricMotorcycle.MaxAirPressure);
                    ((Electric)Engine).MaxBatteryDuration = (float)eElectricMotorcycle.MaxBatteryDuration;
                    break;
                default:
                    break;
            }
        }

        public override Dictionary<string, Dictionary<string, string[]>> PropertiesToDictionary()
        {
            Dictionary<string, Dictionary<string, string[]>> properties = base.PropertiesToDictionary();

            Dictionary<string, string[]> properties_motorcycle = new Dictionary<string, string[]>();
            properties_motorcycle.Add("License Type", Enum.GetNames(typeof(eMotorcycleLicenseTypes)));
            properties_motorcycle.Add("Engine Capacity", null);

            properties.Add("Motorcycle", properties_motorcycle);

            return properties;
        }


        public override eVehiclesTypes GetVehicleType()
        {
            return eVehiclesTypes.Motorcycle;
        }
    }
}
