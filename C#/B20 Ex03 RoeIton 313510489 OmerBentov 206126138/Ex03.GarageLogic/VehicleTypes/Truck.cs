using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.EnumsModel;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_DanagerousLoad;
        private float m_LoadVolume;

        public bool DangerousLoad
        { 
            get
            {
                return m_DanagerousLoad;
            }
            set
            {
                m_DanagerousLoad = value;
            }
        }

        public float LoadVolume
        {
            get
            {
                return m_LoadVolume;
            }

            set
            {
                m_LoadVolume = value;
            }
        }

        public Truck(string i_LicenseNumber, eEngineTypes i_EngineType) :
            base(i_LicenseNumber, i_EngineType)
        {

            switch (i_EngineType)
            {
                case eEngineTypes.Fuel:
                    SetWheels((int)eRegularTruck.Wheels, (float)eRegularTruck.MaxAirPressure);
                    ((Fuel)Engine).FuelType = (eFuelTypes)eRegularTruck.FuelType;
                    ((Fuel)Engine).MaxFuelTank = (float)eRegularTruck.FuelCapacity;
                    break;
                default:
                    break;
            }
        }

        public override Dictionary<string, Dictionary<string, string[]>> PropertiesToDictionary()
        {
            Dictionary<string, Dictionary<string, string[]>> properties = base.PropertiesToDictionary();

            Dictionary<string, string[]> properties_truck = new Dictionary<string, string[]>();
            properties_truck.Add("Does it carry dangerous load?", new string[] { "Yes", "No" });
            properties_truck.Add("Load Volume", null);

            properties.Add("Truck", properties_truck);

            return properties;
        }

        public override eVehiclesTypes GetVehicleType()
        {
            return eVehiclesTypes.Truck;
        }
    }
}
