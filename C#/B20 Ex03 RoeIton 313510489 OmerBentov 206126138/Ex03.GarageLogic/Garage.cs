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

        // Gets a vehicle to add, if the vehicle is already in the list then print a suitable message and change the status of vehicle to “InProgress”.
        public Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> AddVehicle(string i_VehicleType, string i_EngineType, string i_LicensePlate)
        {
            Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> nextInformation = null;

            if (IsExists(i_LicensePlate))
            {
                ChangeStatus(i_LicensePlate, eVehicleStatus.InProgress.ToString());
            }
            else
            {
                GarageVehicle garageVehicle = new GarageVehicle(i_VehicleType, i_EngineType, i_LicensePlate);
                nextInformation = garageVehicle.PropertiesToDictionary();

                m_Vehicles.Add(garageVehicle);
            }

            return nextInformation;
        }

        // Checks weather a vehicle is exists using LicensePlate
        public bool IsExists(string i_LicenseNumner)
        {
            return m_Vehicles.FindIndex(vehicle => vehicle.LicensePlate.Equals(i_LicenseNumner)) >= 0 ? true : false;
        }

        /*//Checks whether a vehicle is exists in the garage already.
        private static bool IsExists(Vehicle i_Vehicle)
        {
            return m_Vehicles.FindIndex(vehicle => vehicle.Equals(i_Vehicle)) >= 0 ? true : false;
        }*/

        // Gets the vehicle object by license number
        public GarageVehicle FindVehicle(string i_LicensePlate)
        {
            return m_Vehicles.Find(vehicle => vehicle.LicensePlate.Equals(i_LicensePlate));
        }

        // Filter the List by status
        public List<GarageVehicle> FilterVehicleByStatus(string i_Status)
        {
            return m_Vehicles.FindAll(vehicle => vehicle.Status.Equals((eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), i_Status)));
        }

        public List<string> ListOfLicensePlates(string i_Status)
        {
            List<string> licenseNumbers = new List<string>();
            List<GarageVehicle> filteredVehicles = m_Vehicles;

            if (!i_Status.Equals("None"))
            {
                filteredVehicles = FilterVehicleByStatus(i_Status);
            }

            foreach (var garageVehicle in filteredVehicles)
            {
                licenseNumbers.Add(garageVehicle.LicensePlate);
            }

            return licenseNumbers;
        }

        public void ChangeStatus(string i_LicensePlate, string i_Status)
        {
            if (IsExists(i_LicensePlate))
            {
                FindVehicle(i_LicensePlate).SetVehicleStatus(i_Status);
            }
        }

        // Change the vehicle’s air pressure to maximum
        public void InflateTiresToMax(string i_LicensePlate)
        {
            if (IsExists(i_LicensePlate))
            {
                FindVehicle(i_LicensePlate).InflateToMax();
            }
        }

        // Refuel vehicle that runs on fuel
        public void Refule(string i_LicensePlate, string i_FuelType, float i_FuleToAdd)
        {
            if (IsExists(i_LicensePlate))
            {
                FindVehicle(i_LicensePlate).Refule(i_FuleToAdd, i_FuelType);
            }
        }

        // Recharge vehicle that runs on electric
        public void Recharge(string i_LicensePlate, float i_DurationToAdd)
        {
            if (IsExists(i_LicensePlate))
            {
                FindVehicle(i_LicensePlate).Recharge(i_DurationToAdd);
            }
        }
        
        public Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> GetInformation(string i_LicensePlate)
        {
            GarageVehicle garageVehicle = FindVehicle(i_LicensePlate);

            Dictionary<string, Dictionary<Dictionary<string, string>, string[]>> vehicle_information = garageVehicle.PropertiesToDictionary();

            Dictionary<string, string> vehicleType = new Dictionary<string, string>();
            vehicleType.Add("Type", $"{garageVehicle.Vehicle.Engine.EngineType} {garageVehicle.Vehicle.GetVehicleType()}");

            Dictionary<string, string> vehicleLicensePlate = new Dictionary<string, string>();
            vehicleLicensePlate.Add("License Plate", garageVehicle.Vehicle.LicensePlate);

            Dictionary<string, string> vehicleStatus = new Dictionary<string, string>();
            vehicleType.Add("Status", garageVehicle.Status.ToString());

            vehicle_information["Vehicle"].Add(vehicleType, null);
            vehicle_information["Vehicle"].Add(vehicleLicensePlate, null);
            vehicle_information["Vehicle"].Add(vehicleStatus, null);

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
            return GetEnumTypes(new eVehiclesTypes());
        }

        public string[] EngineTypes()
        {
            return GetEnumTypes(new eEngineTypes());
        }

        public string[] StatusTypes()
        {
            return GetEnumTypes(new eVehicleStatus());
        }

        public string[] FuelTypes()
        {
            return GetEnumTypes(new eFuelTypes());
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

        public Dictionary<string, Dictionary<string, string>> SetNewVehicleData(string i_LicensePlate, ref Dictionary<string, Dictionary<string, string>> i_Data)
        {
            Vehicle vehicle = m_Vehicles.Find(tempVehicle => tempVehicle.LicensePlate.Equals(i_LicensePlate)).Vehicle;

            return UpdateVehicle(ref vehicle, ref i_Data);
        }
    }
}
