using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.GarageFactory;
using static Ex03.GarageLogic.GarageFactory.EnumsModel;
using static Ex03.GarageLogic.Engine;

namespace Ex03.GarageLogic
{
    public class GarageVehicle
    {
        private string m_OwnerName;
        private string m_PhoneNumber;
        private eVehicleStatus m_Status;
        private Vehicle m_Vehicle;

        public string OwnerName
        {
            get 
            { 
                return m_OwnerName; 
            }

            set
            {
                m_OwnerName = value;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return m_PhoneNumber;
            }

            set
            {
                m_PhoneNumber = value;
            }
        }

        public eVehicleStatus Status
        {
            get
            {
                return m_Status;
            }
        }

        public string LicensePlate
        {
            get
            {
                return m_Vehicle.LicensePlate;
            }
        }

        public int NumOfWheels
        {
            get
            {
                return m_Vehicle.NumOfWheels;
            }
        }

        public Vehicle Vehicle 
        {
            get
            {
                return m_Vehicle;
            }
        }

        public GarageVehicle(string i_VehicleType, string i_EngineType, string i_LicensePlate)
        {
            m_Vehicle = GetVehicle(i_VehicleType, i_EngineType, i_LicensePlate);
            m_Status = eVehicleStatus.InProgress;
        }

        public void SetVehicleStatus(string i_Status)
        {
            if (IsValueTypeValid(new eVehicleStatus(), i_Status))
            {
                m_Status = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), i_Status);
            }
        }

        public void InflateToMax()
        {
            m_Vehicle.InflateTiresToMax();
        }

        public void Refule(float i_FuelToAdd, string i_FuelType)
        {
            if (m_Vehicle.Engine.EngineType.Equals(eEngineTypes.Fuel))
            {
                if (IsValueTypeValid(new eFuelTypes(), i_FuelType))
                {
                    eFuelTypes fuelType = (eFuelTypes)Enum.Parse(typeof(eFuelTypes), i_FuelType);
                    ((Fuel)m_Vehicle.Engine).Refule(i_FuelToAdd, fuelType);
                }
            }
            else
            {
                throw new ArgumentException("It is a vehicle that runs on batteries...");
            }
        }

        public void Recharge(float i_DurationToAdd)
        {
            if (m_Vehicle.Engine.EngineType.Equals(eEngineTypes.Electric))
            {
                ((Electric)m_Vehicle.Engine).Recharge(i_DurationToAdd);
            }
            else
            {
                throw new ArgumentException("It is a vehicle that runs on fuel...");
            }
        }

        public Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> PropertiesToDictionary()
        {
            Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> properties = null;

            switch (m_Vehicle.GetVehicleType())
            {
                case eVehiclesTypes.Car:
                    properties = ((Car)m_Vehicle).PropertiesToDictionary();
                    break;
                case eVehiclesTypes.Motorcycle:
                    properties = ((Motorcycle)m_Vehicle).PropertiesToDictionary();
                    break;
                case eVehiclesTypes.Truck:
                    properties = ((Truck)m_Vehicle).PropertiesToDictionary();
                    break;
            }

            return properties;
        }
    }
}
