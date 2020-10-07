using System;
using System.Collections.Generic;
using System.Linq;
using DL.Models;
using DL.Queries;
using Nager.Date;

namespace BL
{
    public class TollCalculator
    {
        public string ReturnTotalTollFeeForToday(string typedRegistrationNumber)
        {
            DateTime vehiclePassedAt = DateTime.Now;

            //Try to get vehicle from database
            Vehicle vehicle = new Vehicle();
            vehicle = GetVehicle(typedRegistrationNumber.ToUpper());

            if(vehicle != null)
            {
                //In case the vehicle is a toll free vehicle, return a message saying so.
                //(And the only vehicle that is not a toll free vehicle is the type is the: CAR.)
                if(vehicle.VehicleType != "Car")
                { 
                    return "The vehicle with registration number " + vehicle.RegistrationNumber + 
                        " is a " + vehicle.VehicleType + " vehicle and it is a toll free vehicle."; 
                }


                //Is it weekend or a holiday today? If yes, Yay! NO TOLL FEE!!
                if (DateSystem.IsWeekend(vehiclePassedAt, CountryCode.SE) | DateSystem.IsPublicHoliday(vehiclePassedAt, CountryCode.SE))
                {
                    return "It's weekend or holiday today. So " + vehicle.RegistrationNumber + " does not have to pay any toll fee today!";
                }


                //Has the CAR passed at least once in the last 60 min period?
                //Then the passage with the highest price is the only one that counts.
                List<DriveBy> lastHoursDriveBys = new List<DriveBy>();
                lastHoursDriveBys = GetDriveBys1HourBack(vehicle.Id, vehiclePassedAt);

                //Get current Toll fee
                int tollFeeForCurrentDriveBy = GetTollFee(vehiclePassedAt);
                
                //Mapp driveBy
                DriveBy curentDriveBy = new DriveBy();
                curentDriveBy.PassageCost = tollFeeForCurrentDriveBy;
                curentDriveBy.PassedAt = vehiclePassedAt;
                curentDriveBy.VehicleId = vehicle.Id;

                if (lastHoursDriveBys != null)
                {
                    //Check if the latest passage is the more expensive than the other ones.
                    bool currentTollFeeIsMoreExpensive = false;
                    List<int> tollFeeCostsTheLastHour = new List<int>();
                    for (int i = 0; i < lastHoursDriveBys.Count(); i++)
                    {
                        tollFeeCostsTheLastHour.Add(lastHoursDriveBys[i].PassageCost);
                    }
                    if(tollFeeForCurrentDriveBy > tollFeeCostsTheLastHour.Max()) { currentTollFeeIsMoreExpensive = true; }

                    if(currentTollFeeIsMoreExpensive)
                    {
                        //If yes, okay, get the diffrence and add that difference to the CostPerDay to update the total toll fee for the day.
                        int costDiff = tollFeeForCurrentDriveBy - tollFeeCostsTheLastHour.Max();

                        //Add current passing to DriveBys table
                        bool driveByAdded = false;
                        curentDriveBy.PassageCost = costDiff;
                        driveByAdded = AddDriveBy(curentDriveBy);

                        //Update the CostPerDays table with the diff
                        bool costPerDayWasAddedOrUpdated = false;
                        costPerDayWasAddedOrUpdated = UpdateOrAddCostPerDay(curentDriveBy);

                        if (driveByAdded && costPerDayWasAddedOrUpdated)
                        {
                            CostPerDay cost = new CostPerDay();
                            cost = GetTodaysTotalCost(vehicle.Id, curentDriveBy.PassedAt);
                            if (cost == null) { return "Could not get the total cost for today but the current toll fee replaced the previous highest toll fee for the past hour for " + vehicle.RegistrationNumber + "."; }
                            else { return "Current Toll fee (" + tollFeeForCurrentDriveBy.ToString() + " kr) replaced the previous highest toll fee for the past hour for " + vehicle.RegistrationNumber + ".  Now the total cost for " + cost.Date.ToShortDateString() + " is " + cost.CostThisDay + " kr."; }
                        }
                        else
                        {
                            return "Could not Add the current driveby and/or could not Add/Update the total cost for today with the current toll fee for: " + vehicle.RegistrationNumber + ".";
                        }
                    }
                    else
                    {
                        //If NO, good. No need to add anything to the CostPerDays table. Only add to the DriveBys table.

                        //Add current passaing to DriveBys table
                        bool driveByAdded = false;
                        driveByAdded = AddDriveBy(curentDriveBy);

                        if(driveByAdded)
                        {
                            CostPerDay cost = new CostPerDay();
                            cost = GetTodaysTotalCost(vehicle.Id, curentDriveBy.PassedAt);
                            if (cost == null) { return "Could not get the total cost for today. Current toll fee did not need to be added to the DB for: " + vehicle.RegistrationNumber + "."; }
                            else { return "Toll fee (" + curentDriveBy.PassageCost.ToString() + " kr) did not need to be added for " + vehicle.RegistrationNumber + ". Total cost for " + cost.Date.ToShortDateString() + " is " + cost.CostThisDay + " kr."; }
                        }
                        else { return "Someting went wrong when adding DriveBy to the DB when one or more already exist in the table for " + vehicle.RegistrationNumber + "."; }
                    }
                }
                else
                {
                    // No Drivebys the last hour for this car. Just add the latest passage to the  CostPerDay & DriveBys  tables.
                    bool driveByAdded = false;
                    driveByAdded = AddDriveBy(curentDriveBy);

                    if(driveByAdded)
                    {
                        //Update or add total cost for the day.
                        bool costPerDayWasAddedOrUpdated = false;
                        costPerDayWasAddedOrUpdated = UpdateOrAddCostPerDay(curentDriveBy);

                        if(costPerDayWasAddedOrUpdated)
                        {
                            CostPerDay cost = new CostPerDay();
                            cost = GetTodaysTotalCost(vehicle.Id, curentDriveBy.PassedAt);
                            if(cost == null) { return "Could not get the total cost for today but the current toll fee was added for " + vehicle.RegistrationNumber + "."; }
                            else { return "Toll fee (" + curentDriveBy.PassageCost.ToString() + " kr) was added for " + vehicle.RegistrationNumber + ". Total cost for " + cost.Date.ToShortDateString() + " is " + cost.CostThisDay + " kr."; }
                        }
                        else
                        {
                            return "Could not Add or Update the total cost for the day with the current toll fee for: " + vehicle.RegistrationNumber + ".";
                        }
                    }
                    else
                    {
                        //Something went wrong
                        return "Something went wrong when trying to add the passage to the DB for " + vehicle.RegistrationNumber + ".";
                    }
                }
            }

            //Vehicle not found.
            return "Vehicle not found. Try a different registration number.";
        }


        private int GetTollFee(DateTime vehiclePassedAt)
        {
            int hour = vehiclePassedAt.Hour;
            int minute = vehiclePassedAt.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29) return 8; // 06:00 - 06:29
            else if (hour == 6 && minute >= 30 && minute <= 59) return 13; // 06:30 - 06:59
            else if (hour == 7 && minute >= 0 && minute <= 59) return 18; // 07:00 - 07-59
            else if (hour == 8 && minute >= 0 && minute <= 29) return 13; // 08:00 - 08:29

            else if (hour == 8 && minute >= 30 && minute <= 59) return 8; // 08:30 - 08:59
            else if (hour >= 9 && hour <= 14) return 8; // 09:00 - 14:59.

            else if (hour == 15 && minute >= 0 && minute <= 29) return 13; // 15:00 - 15:29
            else if (hour == 15 && minute >= 30 && minute <= 59) return 18; // 15:30 - 15:59
            else if (hour == 16 && minute >= 0 && minute <= 59) return 18; // 16:00 - 16:59
            else if (hour == 17 && minute >= 0 && minute <= 59) return 13; // 17:00 - 17:59
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8; // 18:00 - 18:29
            else return 0;
        }

        private CostPerDay GetTodaysTotalCost(int vehicleId, DateTime todaysDate)
        {
            VehicleQueries vehicleQueries = new VehicleQueries();
            CostPerDay cost = new CostPerDay();

            cost = vehicleQueries.GetTodaysCost(vehicleId, todaysDate);

            return cost;
        }

        private Vehicle GetVehicle(string regNumber)
        {
            VehicleQueries vehicleQueries = new VehicleQueries();
            Vehicle vehicle = new Vehicle();

            vehicle = vehicleQueries.GetVehicle(regNumber);

            return vehicle;
        }

        private List<DriveBy> GetDriveBys1HourBack(int vehicleId, DateTime passedAt)
        {
            VehicleQueries vehicleQueries = new VehicleQueries();
            List<DriveBy> driveByList = new List<DriveBy>();

            driveByList = vehicleQueries.GetDriveBysForTheLastHourByVehicleId(vehicleId, passedAt);

            return driveByList;
        }

        private bool AddDriveBy(DriveBy driveBy)
        {
            VehicleQueries vehicleQueries = new VehicleQueries();
            bool result = false;

            result = vehicleQueries.AddCurrentDriveBy(driveBy);

            return result;
        }

        private bool UpdateOrAddCostPerDay(DriveBy driveBy)
        {
            VehicleQueries vehicleQueries = new VehicleQueries();
            bool result = false;

            result = vehicleQueries.UpdateOrAddCostPerDayByVehicleId(driveBy);

            return result;
        }
    }
}
