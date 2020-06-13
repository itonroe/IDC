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
        private string m_LicensePlate;
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

        public int NumOfWheels
        {
            get
            {
                return m_Wheels.Length;
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

        public string LicensePlate
        {
            get
            {
                return m_LicensePlate;
            }

            set
            {
                m_LicensePlate = value;
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

        public Vehicle(string i_LicensePlate, eEngineTypes i_Engine)
        {
            m_LicensePlate = i_LicensePlate;

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

        public void UpdateWheels(string i_Manufacturer, float i_CurrentAirPressure)
        {
            for (int i = 0; i < m_Wheels.Length; i++)
            {
                m_Wheels[i].Manufacturer = i_Manufacturer;
                InflateTire(i, i_CurrentAirPressure);
            }
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
                equals = (m_LicensePlate.Equals(vehicle.LicensePlate));
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public abstract eVehiclesTypes GetVehicleType();

        public virtual Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> PropertiesToDictionary()
        {
            Dictionary<Dictionary<string, string>, string[]> properties_vehicle = new Dictionary<Dictionary<string, string>, string[]>();
            Dictionary<string, string> modelName = new Dictionary<string, string>();
            modelName.Add("Model Name", m_Model);
            properties_vehicle.Add(modelName, null);

            Dictionary<Dictionary<string, string>, string[]> properties_wheels = new Dictionary<Dictionary<string, string>, string[]>();

            Dictionary<string, string> manuFacturerName = new Dictionary<string, string>();
            manuFacturerName.Add("Manufacturer Name", m_Wheels[0].Manufacturer);

            Dictionary<string, string> currentWhellPressure = new Dictionary<string, string>();
            currentWhellPressure.Add("Current Wheel Air Pressure", String.Format("{0:0.00}", m_Wheels[0].CurrentAirPressure));

            properties_wheels.Add(manuFacturerName, null);
            properties_wheels.Add(currentWhellPressure, null);

            Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> properties = new Dictionary<string, Dictionary<Dictionary<string, string>, string[]>>();

            properties.Add("Vehicle", properties_vehicle);
            properties.Add("Wheels", properties_wheels);
            properties.Add("Engine", m_Engine.PropertiesToDictionary());

            return properties;
        }
    }
}
