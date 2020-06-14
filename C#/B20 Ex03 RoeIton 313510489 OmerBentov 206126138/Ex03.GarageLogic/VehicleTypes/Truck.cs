using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.GarageFactory;
using static Ex03.GarageLogic.GarageFactory.EnumsModel;

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

        public Truck(string i_LicensePlate, eEngineTypes i_EngineType) :
            base(i_LicensePlate, i_EngineType)
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

        public override Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> PropertiesToDictionary()
        {
            Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> properties = base.PropertiesToDictionary();

            Dictionary<Dictionary<string, string>, string[]> properties_truck = new Dictionary<Dictionary<string, string>, string[]>();

            Dictionary<string, string> dangerousLoad = new Dictionary<string, string>();
            dangerousLoad.Add("Does it carry dangerous load?", m_DanagerousLoad.ToString());

            Dictionary<string, string> loadVolume = new Dictionary<string, string>();
            loadVolume.Add("Load Volume", m_LoadVolume.ToString());

            properties_truck.Add(dangerousLoad, new string[] { "Yes", "No" });
            properties_truck.Add(loadVolume, null);

            properties.Add("Truck", properties_truck);

            return properties;
        }

        public override eVehiclesTypes GetVehicleType()
        {
            return eVehiclesTypes.Truck;
        }
    }
}
