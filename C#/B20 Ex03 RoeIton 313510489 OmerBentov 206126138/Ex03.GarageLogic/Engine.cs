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

        public float CurrentAmount
        {
            get
            {
                return m_CurrentAmount / m_MaxAmount;
            }
        }

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

        public abstract Dictionary<Dictionary<string, string>, string[]> PropertiesToDictionary();

        public class Electric : Engine
        {
            // It's the same thing like Current amount
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

            public override Dictionary<Dictionary<string, string>, string[]> PropertiesToDictionary()
            {
                Dictionary<Dictionary<string, string>, string[]> properties = new Dictionary<Dictionary<string, string>, string[]>();

                Dictionary<string, string> batteryDuration = new Dictionary<string, string>();
                batteryDuration.Add("Battery Duration Left", String.Format("{0:0.00}", m_CurrentAmount));
                properties.Add(batteryDuration, null);

                return properties;
            }

            public void Recharge(float i_DurationToAdd)
            {
                base.Refill("Amount of Time to add", i_DurationToAdd);
            }
        }

        public class Fuel : Engine
        {
            // It's the same thing like Current amount
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

            public override Dictionary<Dictionary<string, string>, string[]> PropertiesToDictionary()
            {
                Dictionary<Dictionary<string, string>, string[]> properties = new Dictionary<Dictionary<string, string>, string[]>();

                Dictionary<string, string> fuelType = new Dictionary<string, string>();
                fuelType.Add("Fuel Type", m_FuelType.ToString());

                Dictionary<string, string> currentAmount = new Dictionary<string, string>();
                currentAmount.Add("Current amount of Fuel", String.Format("{0:0.00}", m_CurrentAmount));

                properties.Add(fuelType, Enum.GetNames(typeof(eFuelTypes)));
                properties.Add(currentAmount, null);

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
                    throw new ArgumentException("This car don't use this kind of fuel, Be aware...");
                }
            }
        }
    }
}
