using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public string Manufacturer 
        {
            get
            {
                return m_Manufacturer;
            }

            set
            {
                m_Manufacturer = value;
            }
        }
        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }
        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }

            set
            {
                m_MaxAirPressure = value;
            }
        }

        public void InflateTire(float i_AirPressureToAdd)
        {
            if (i_AirPressureToAdd + m_CurrentAirPressure > m_MaxAirPressure || i_AirPressureToAdd < 0)
            {
                throw new ValueOutOfRangeException(0, m_MaxAirPressure - m_CurrentAirPressure, "Amount of Pressure to add");
            }

            m_CurrentAirPressure += i_AirPressureToAdd;
        }
    }
}
