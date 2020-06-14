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

        public Motorcycle(string i_LicensePlate, eEngineTypes i_EngineType) :
            base(i_LicensePlate, i_EngineType)
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

        public override Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> PropertiesToDictionary()
        {
            Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> properties = base.PropertiesToDictionary();

            Dictionary<Dictionary<string, string>, string[]> properties_motorcycle = new Dictionary<Dictionary<string, string>, string[]>();

            Dictionary<string, string> licenseType = new Dictionary<string, string>();
            licenseType.Add("License Type", m_LicenseType.ToString());

            Dictionary<string, string> engineCapacity = new Dictionary<string, string>();
            engineCapacity.Add("Engine Capacity", m_EngineCapacity.ToString());

            properties_motorcycle.Add(licenseType, Enum.GetNames(typeof(eMotorcycleLicenseTypes)));
            properties_motorcycle.Add(engineCapacity, null);

            properties.Add("Motorcycle", properties_motorcycle);

            return properties;
        }

        public override eVehiclesTypes GetVehicleType()
        {
            return eVehiclesTypes.Motorcycle;
        }
    }
}
