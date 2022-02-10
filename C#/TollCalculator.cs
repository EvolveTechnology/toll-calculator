using System;
using System.Globalization;
using TollFeeCalculator;

public class TollCalculator
{
    //Comments explaining why I did certain changes is not something I usually do. So it might look a bit more cluttered than it would normally. 
    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    //I'm assuming the main function calls this at the end of each day when all toll moments(all dates) have been registered. 
    public int GetTollFee(Vehicle vehicle, DateTime[] timeStamps)
    {
        DateTime intervalStart = timeStamps[0];
        int totalFee = 0;
        
        //Moved to this state, makes more sense to not make calculations on a toll free vehicle or date at all.
        if (IsTollFreeDate(intervalStart) || IsTollFreeVehicle(vehicle)) return totalFee;    

        int hour = intervalStart.Hour;
        int tempFee = GetTollFee(intervalStart);
        
        //TimeStamps is more logical than dates since. Will always compare intervalStart to intervalStart the first iteration. 
        foreach (DateTime time in timeStamps)   
        {
            int nextFee = GetTollFee(time);
      
            //long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            //long minutes = diffInMillies/1000/60;
            
            //if multiple date in the same hour.
            if (hour == time.Hour) 
            {               
                if (nextFee >= tempFee)
                {   
                    //remove the lower fee from total. Only relevant if fee change for the hour. 
                    if (totalFee > 0) totalFee -= tempFee;
                    tempFee = nextFee;
                } 
                totalFee += tempFee;    //add the higher fee.
            }
            else
            {
                totalFee += nextFee;    //new fee to add.
                hour = time.Hour;       //update hour
                tempFee = GetTollFee(time); //update new temp fee for the new hour.
            }

            if (totalFee > 60) //if exceeding 60SEK, no need to continue checking toll fees.
            {
                totalFee = 60;
                return totalFee;
            }
        }
        return totalFee;
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;  //if no vehicle is specified, for some reason. ?
        
        string vehicleType = vehicle.GetVehicleType();      
        return Enum.IsDefined(typeof(TollFreeVehicles), vehicleType); 
        
        
        /*return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());*/
    }

    public int GetTollFee(DateTime date)        //Struggled with this one
    {
        //if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0; moved to GetTollFee.

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }

    //I would change this to support an automated format that reads from a calendar file which can be changed instead of the dates being hard coded into the software. 
    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;
        //if(year == 2013)      only works the year 2013..
        
        if (month == 1 && day == 1 ||
            month == 3 && (day == 28 || day == 29) ||
            month == 4 && (day == 1 || day == 30) ||
            month == 5 && (day == 1 || day == 8 || day == 9) ||
            month == 6 && (day == 5 || day == 6 || day == 21) ||
            month == 7 ||
            month == 11 && day == 1 ||
            month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
        {
            return true;
        }
        else
            return false;

        return false;
    }

    private enum TollFreeVehicles
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }

}
