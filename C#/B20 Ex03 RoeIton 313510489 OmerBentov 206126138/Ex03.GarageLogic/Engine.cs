using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.EnumsModel;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private eEngineTypes m_EngineType;
        private float m_CurrentAmount;
        private float m_MaxAmount;

        public eEngineTypes EngineType
        {
            get
            {
                return m_EngineType;
            }
            set
            {
                m_EngineType = value;
            }
        }

        private void Refill(string i_Value, float i_AmountToAdd)
        {
            if (i_AmountToAdd + m_CurrentAmount > m_MaxAmount)
            {
                throw new ValueOutOfRangeException(0, m_MaxAmount - m_CurrentAmount, i_Value);
            }

            m_CurrentAmount += i_AmountToAdd;
        }

        public abstract Dictionary<string, string[]> PropertiesToDictionary();

        public class Electric : Engine
        {
            public float BatteryDurationLeft
            {
                get
                {
                    return m_CurrentAmount;
                }
                set
                {
                    m_CurrentAmount = value;
                }
            }

            public float MaxBatteryDuration
            {
                get
                {
                    return m_MaxAmount;
                }
                set
                {
                    m_MaxAmount = value;
                }
            }

            public override Dictionary<string, string[]> PropertiesToDictionary()
            {
                Dictionary<string, string[]> properties = new Dictionary<string, string[]>();
                properties.Add("Battery Duration Left", null);

                return properties;
            }

            public void Recharge(float i_DurationToAdd)
            {
                base.Refill("Amount of Time to add", i_DurationToAdd);
            }
        }

        public class Fuel : Engine
        {
            public float CurrentFuelTank
            {
                get
                {
                    return m_CurrentAmount;
                }
                set
                {
                    m_CurrentAmount = value;
                }
            }

            public float MaxFuelTank
            {
                get
                {
                    return m_MaxAmount;
                }
                set
                {
                    m_MaxAmount = value;
                }
            }


            private eFuelTypes m_FuelType;

            public eFuelTypes FuelType 
            {
                get
                {
                    return m_FuelType;
                }

                set
                {
                    m_FuelType = value;
                }
            }

            public override Dictionary<string, string[]> PropertiesToDictionary()
            {
                Dictionary<string, string[]> properties = new Dictionary<string, string[]>();
                properties.Add("Fuel Type", Enum.GetNames(typeof(eFuelTypes)));
                properties.Add("Current amount of Fuel", null);

                return properties;
            }

            public void Refule(float i_FuelToAdd, eFuelTypes i_FuelType)
            {
                if (i_FuelType.Equals(m_FuelType))
                {
                    base.Refill("Amount of Fuel to add", i_FuelToAdd);
                }
                else
                {
                    //Change Error Message
                    throw new ArgumentException("Fuel type is wrong.");
                }
            }
        }
    }
}

