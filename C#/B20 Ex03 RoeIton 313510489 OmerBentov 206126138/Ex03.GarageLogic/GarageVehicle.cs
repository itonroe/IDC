using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.GarageFactory;
using static Ex03.GarageLogic.EnumsModel;
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

        public string LicenseNumber
        {
            get
            {
                return m_Vehicle.LicenseNumber;
            }
        }

        public GarageVehicle(string i_VehicleType, string i_EngineType, string i_LicenseNumber)
        {
            m_Vehicle = GetVehicle(i_VehicleType, i_EngineType, i_LicenseNumber);
            m_Status = eVehicleStatus.InProgress;
        }

        public void SetVehicleStatus(string i_Status)
        {
            if (IsVehicleStatusTypeValid(i_Status))
            {
                eVehicleStatus vehicleStatus = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), i_Status);
                m_Status = vehicleStatus;
            }
        }

        private bool IsVehicleStatusTypeValid(string i_VehicleStatus)
        {
            return Enum.IsDefined(typeof(eVehicleStatus), i_VehicleStatus);
        }

        public void InflateToMax()
        {
            m_Vehicle.InflateTiresToMax();
        }

        public void Refule(float i_FuelToAdd, string i_FuelType)
        {
            if (m_Vehicle.Engine.EngineType.Equals(eEngineTypes.Fuel))
            {
                if (IsFuelTypeValid(i_FuelType))
                {
                    eFuelTypes fuelType = (eFuelTypes)Enum.Parse(typeof(eFuelTypes), i_FuelType);
                    ((Fuel)m_Vehicle.Engine).Refule(i_FuelToAdd, fuelType);
                }
            }
        }

        public void Recharge(float i_DurationToAdd)
        {
            if (m_Vehicle.Engine.EngineType.Equals(eEngineTypes.Electric))
            {
                ((Electric)m_Vehicle.Engine).Recharge(i_DurationToAdd);
            }
        }

        private bool IsFuelTypeValid(string i_FuelType)
        {
            return Enum.IsDefined(typeof(eFuelTypes), i_FuelType);
        }

        public Dictionary<string, Dictionary<string, string[]>> PropertiesToDictionary()
        {
            Dictionary<string, Dictionary<string, string[]>> properties = null;

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
