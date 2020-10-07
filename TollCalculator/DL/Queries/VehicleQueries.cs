using System.Data.Linq;
using System.Linq;
using DL.Tables;
using DL.Models;
using System;
using System.Collections.Generic;

namespace DL.Queries
{
    public partial class VehicleDB : DataContext
    {
        public Table<Vehicles> Vehicles;
        public Table<DriveBys> DriveBys;
        public Table<CostPerDays> CostPerDays;
        public VehicleDB(string connection) : base(connection) { }

        //Obs: View the Table: Vehicle, for more registration numbers.
    }

    public class VehicleQueries
    {
        private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\VehicleDB.mdf";

        public Vehicle GetVehicle(string regNr)
        {
            Vehicle vehicle = new Vehicle();

            try
            {
                // Establish DB connection
                VehicleDB db = new VehicleDB(connectionString);
                db.Connection.Open();

                //Query
                var queryResult =
                        (from v in db.Vehicles
                        where v.RegistrationNumber == regNr
                        select v).SingleOrDefault();

                //Close DB Connection
                db.Connection.Close();

                //Handle result
                if (queryResult != null)
                {
                    vehicle.Id = queryResult.Id;
                    vehicle.RegistrationNumber = queryResult.RegistrationNumber;
                    vehicle.VehicleType = queryResult.VehicleType;
                }
                else
                {
                    //Duplicate exist in the database. Can't have two vehicles with the same registration number.
                    //Or nothing was found.
                    vehicle = null;
                }
            }
            catch(Exception ex)
            {
                vehicle = null;
            }
            
            return vehicle;
        }

        public List<DriveBy> GetDriveBysForTheLastHourByVehicleId(int vehichleId, DateTime vehiclePassedAt)
        {
            List<DriveBy> driveBysList = new List<DriveBy>();

            try
            {
                DateTime date60MinutesBackInTime = vehiclePassedAt.AddMinutes(-60);

                // Establish DB connection
                VehicleDB db = new VehicleDB(connectionString);
                db.Connection.Open();

                //Query
                var queryResult =
                        from v in db.DriveBys
                        where v.VehicleId == vehichleId && v.PassedAt >= date60MinutesBackInTime
                        select v;

                //Close DB Connection
                db.Connection.Close();

                //Handle result
                if (queryResult == null || queryResult.Count() == 0)
                {
                    //Nothing was found.
                    return driveBysList = null;
                }

                foreach (var item in queryResult)
                {
                    DriveBy driveBy = new DriveBy();

                    driveBy.Id = item.Id;
                    driveBy.VehicleId = item.VehicleId;
                    driveBy.PassedAt = item.PassedAt;
                    driveBy.PassageCost = item.PassageCost;

                    driveBysList.Add(driveBy);
                }
            }
            catch(Exception ex)
            {
                driveBysList = null;
            }

            return driveBysList;
        }

        public bool AddCurrentDriveBy(DriveBy driveBy)
        {
            bool driveByAdded = false;

            try
            {
                // Establish DB connection
                VehicleDB db = new VehicleDB(connectionString);
                db.Connection.Open();

                //Query
                DriveBys driveBys = new DriveBys
                {
                    VehicleId = driveBy.VehicleId,
                    PassageCost = driveBy.PassageCost,
                    PassedAt = driveBy.PassedAt
                };

                // Add the new object.
                db.DriveBys.InsertOnSubmit(driveBys);

                // Submit the change to the database.
                db.SubmitChanges();
                
                //Close DB Connection
                db.Connection.Close();

                driveByAdded = true;
            }
            catch(Exception ex)
            {
                driveByAdded = false;
            }

            return driveByAdded;
        }

        public bool UpdateOrAddCostPerDayByVehicleId(DriveBy driveBy)
        {
            bool success = false;

            try
            {
                // Establish DB connection
                VehicleDB db = new VehicleDB(connectionString);
                db.Connection.Open();

                //Query. Check if there is any post for the date.
                var queryResult =
                        (from v in db.CostPerDays
                         where v.VehicleId == driveBy.VehicleId && v.Date.Date == driveBy.PassedAt.Date
                         select v).SingleOrDefault();

                //Handle result
                if (queryResult == null)
                {
                    //Add.
                    CostPerDays costPerdays = new CostPerDays
                    {
                        VehicleId = driveBy.VehicleId,
                        Date = driveBy.PassedAt,
                        CostThisDay = driveBy.PassageCost
                    };

                    db.CostPerDays.InsertOnSubmit(costPerdays);

                    db.SubmitChanges();

                    db.Connection.Close();

                    success = true;
                }
                else
                {
                    //Update.
                    int newCostForTheDay = queryResult.CostThisDay + driveBy.PassageCost;
                    queryResult.CostThisDay = newCostForTheDay;

                    db.SubmitChanges();

                    db.Connection.Close();

                    success = true;
                }
            }
            catch(Exception ex)
            {
                success = false;
            }

            return success;
        }

        public CostPerDay GetTodaysCost(int vehicleId, DateTime todaysDate)
        {
            CostPerDay cost = new CostPerDay();

            try
            {
                // Establish DB connection
                VehicleDB db = new VehicleDB(connectionString);
                db.Connection.Open();

                //Query. Check if there is any post for the date.
                var queryResult =
                        (from v in db.CostPerDays
                         where v.VehicleId == vehicleId && v.Date.Date == todaysDate.Date
                         select v).SingleOrDefault();

                //Close connection
                db.Connection.Close();

                if (queryResult != null)
                {
                    cost.Id = queryResult.Id;
                    cost.VehicleId = queryResult.VehicleId;
                    cost.Date = queryResult.Date;
                    cost.CostThisDay = queryResult.CostThisDay;
                }
                else
                {
                    cost = null;
                }
            }
            catch(Exception ex)
            {
                cost = null;
            }

            return cost;
        }
    }
}
