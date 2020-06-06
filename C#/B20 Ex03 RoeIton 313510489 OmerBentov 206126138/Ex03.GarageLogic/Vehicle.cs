using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.EnumsModel;

namespace Ex03.GarageLogic
{
     public abstract class Vehicle
     {
        private string m_Model;
        private string m_LicenseNumber;
        private float m_EnergyLeft;
        private Wheel[] m_Wheels;
        private Engine m_Engine;

        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
            set
            {
                m_Engine = value;
            }
        }

        public Vehicle (string i_LicenseNumber, eEngineTypes i_Engine)
        {
            m_LicenseNumber = i_LicenseNumber;

            if (i_Engine.Equals(eEngineTypes.Fuel))
            {
                m_Engine = new Fuel();
                m_Engine.EngineType = eEngineTypes.Fuel;
            }
            else
            {
                m_Engine = new Electric();
                m_Engine.EngineType = eEngineTypes.Electric;
            }
        }

        public string Model
        {
            get
            {
                return m_Model;
            }

            set
            {
                m_Model = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            set
            {
                m_LicenseNumber = value;
            }
        }

        public float EnergyLeft
        {
            get
            {
                return m_EnergyLeft;
            }

            set
            {
                m_EnergyLeft = value;
            }
        }

        public void SetWheels(int i_NumOfWheels, float i_MaxAirPressure) 
        {
            m_Wheels = new Wheel[i_NumOfWheels];

            for (int i = 0; i < i_NumOfWheels; i++)
            {
                m_Wheels[i] = new Wheel();
                m_Wheels[i].CurrentAirPressure = 0;
                m_Wheels[i].MaxAirPressure = i_MaxAirPressure;
            }
        }

        public void InflateTiresToMax()
        {
            for (int i = 0; i < m_Wheels.Length; i++)
            {
                float leftToAdd = m_Wheels[i].MaxAirPressure - m_Wheels[i].CurrentAirPressure;

                InflateTire(i, leftToAdd);
            }
        }

        public void InflateTire(int i_WheelIndex, float i_AirPressureToAdd)
        {
            m_Wheels[i_WheelIndex].InflateTire(i_AirPressureToAdd);
        }

        public override bool Equals(object obj)
        {
            bool equals = true;

            if (obj == null)
            {
                equals = false;
            }
            else
            {
                Vehicle vehicle = (Vehicle)obj;
                equals = (m_LicenseNumber.Equals(vehicle.LicenseNumber));
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public abstract eVehiclesTypes GetVehicleType();

        public virtual Dictionary<string, Dictionary<string, string[]>> PropertiesToDictionary()
        {
            Dictionary<string, string[]> properties_vehicle = new Dictionary<string, string[]>();
            properties_vehicle.Add("Model Name", null);

            Dictionary<string, string[]> properties_wheels = new Dictionary<string, string[]>();
            properties_wheels.Add("Manufacturer Name", null);
            properties_wheels.Add("Current Wheel Air Pressure Name", null);

            Dictionary<string, Dictionary<string, string[]>> properties = new Dictionary<string, Dictionary<string, string[]>>();

            properties.Add("Vehicle", properties_vehicle);
            properties.Add("Wheels", properties_wheels);
            properties.Add("Engine", m_Engine.PropertiesToDictionary());

            return properties;
        }
    }
}
