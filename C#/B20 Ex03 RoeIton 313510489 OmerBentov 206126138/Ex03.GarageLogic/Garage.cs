using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.EnumsModel;
using static Ex03.GarageLogic.GarageFactory;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly List<GarageVehicle> m_Vehicles;

        public Garage()
        {
            m_Vehicles = new List<GarageVehicle>();
        }

        //Gets a vehicle to add, if the vehicle is already in the list then print a suitable message and change the status of vehicle to “InProgress”.
        public Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> AddVehicle(string i_VehicleType, string i_EngineType, string i_LicenseNumber)
        {
            Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> next_information = null;

            if (IsExists(i_LicenseNumber))
            {
                ChangeStatus(i_LicenseNumber, eVehicleStatus.InProgress.ToString());
            }
            else
            {
                GarageVehicle garageVehicle = new GarageVehicle(i_VehicleType, i_EngineType, i_LicenseNumber);
                next_information = garageVehicle.PropertiesToDictionary();

                m_Vehicles.Add(garageVehicle);
            }

            return next_information;
        }

        //Checks weather a vehicle is exists using LicenseNumber
        public bool IsExists(string i_LicenseNumner)
        {
            return m_Vehicles.FindIndex(vehicle => vehicle.LicenseNumber.Equals(i_LicenseNumner)) >= 0 ? true : false;
        }

        /*//Checks whether a vehicle is exists in the garage already.
        private static bool IsExists(Vehicle i_Vehicle)
        {
            return m_Vehicles.FindIndex(vehicle => vehicle.Equals(i_Vehicle)) >= 0 ? true : false;
        }*/

        //Gets the vehicle object by license number
        public GarageVehicle FindVehicle(string i_LicenseNumber)
        {
            return m_Vehicles.Find(vehicle => vehicle.LicenseNumber.Equals(i_LicenseNumber));
        }

        //Filter the List by status
        public List<GarageVehicle> FilterVehicleByStatus(string i_Status)
        {
            return m_Vehicles.FindAll(vehicle => vehicle.Status.Equals((eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), i_Status)));
        }

        public List<string> ListOfLicenseNumbers(string i_Status)
        {
            List<string> licenseNumbers = new List<string>();
            List<GarageVehicle> filteredVehicles = m_Vehicles;

            if (!i_Status.Equals("None"))
            {
                filteredVehicles = FilterVehicleByStatus(i_Status);
            }

            foreach (var garageVehicle in filteredVehicles)
            {
                licenseNumbers.Add(garageVehicle.LicenseNumber);
            }

            return licenseNumbers;
        }

        public void ChangeStatus(string i_LicenseNumber, string i_Status)
        {
            if (IsExists(i_LicenseNumber))
            {
                FindVehicle(i_LicenseNumber).SetVehicleStatus(i_Status);
            }
        }

        //Change the vehicle’s air pressure to maximum
        public void InflateTiresToMax(string i_LicenseNumber)
        {
            if (IsExists(i_LicenseNumber))
            {
                FindVehicle(i_LicenseNumber).InflateToMax();
            }
        }

        //Refuel vehicle that runs on fuel
        public void Refule(string i_LicenseNumber, string i_FuelType, float i_FuleToAdd)
        {
            if (IsExists(i_LicenseNumber))
            {
                FindVehicle(i_LicenseNumber).Refule(i_FuleToAdd, i_FuelType);
            }
        }

        //Recharge vehicle that runs on electric
        public void Recharge(string i_LicenseNumber, float i_DurationToAdd)
        {
            if (IsExists(i_LicenseNumber))
            {
                FindVehicle(i_LicenseNumber).Recharge(i_DurationToAdd);
            }
        }
        
        public Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> GetInformation(string i_LicensePlate)
        {
            GarageVehicle garageVehicle = FindVehicle(i_LicensePlate);

            Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> vehicle_information = garageVehicle.PropertiesToDictionary();

            Dictionary<Dictionary<string, string>, string[]> properties_owner = new Dictionary<Dictionary<string, string>, string[]>();

            Dictionary<string, string> ownerName = new Dictionary<string, string>();
            ownerName.Add("Name", garageVehicle.OwnerName);

            Dictionary<string, string> ownerPhoneNumber = new Dictionary<string, string>();
            ownerPhoneNumber.Add("Phone Number", garageVehicle.OwnerPhoneNumber);

            properties_owner.Add(ownerName, Enum.GetNames(typeof(eCarColor)));
            properties_owner.Add(ownerPhoneNumber, Enum.GetNames(typeof(eCarNumOfDoors)));

            vehicle_information.Add("Owner", properties_owner);

            return vehicle_information;
        }

        public string[] VehicleTypes()
        {
            return GetVehicleTypes();
        }

        public string[] EngineTypes()
        {
            return GetEngineTypes();
        }

        public string[] StatusTypes()
        {
            return GetStatusTypes();
        }

        public int GetNumberOfWheelsBy(string i_LicensePlate)
        {
            return FindVehicle(i_LicensePlate).NumOfWheels;
        }

        public void SetVehicleOwner(string i_LicensePlate, string i_OwnerName, string i_PhoneNumber)
        {
            FindVehicle(i_LicensePlate).OwnerName = i_OwnerName;
            FindVehicle(i_LicensePlate).OwnerPhoneNumber = i_PhoneNumber;
        }

        public bool SetNewVehicleData(string i_LicensePlate, Dictionary<string, Dictionary<string, string>> i_Data)
        {
            Vehicle vehicle = m_Vehicles.Find(ve => ve.LicenseNumber.Equals(i_LicensePlate)).Vehicle;

            return UpdateVehicle(ref vehicle, i_Data);
        }
    }
}
