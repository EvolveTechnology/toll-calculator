using System;
using System.Collections.Generic;
using System.Linq;

namespace TollFeeCalculator.Toll
{
    public class TollCalculator
    {
        private List<DateTime> HolidayDates = new List<DateTime>();
        public int CurrentFee { get; set; }
        private DateTime entranceDate;
        private int[] FeeRanges =  { 8, 13, 18 };
        private int  MaxFee = 60;
        private readonly IVehicleType vehicleType;
        private  int startRushHourH;
        private  int startRushHourM;
        private  int endRushHourH;
        private  int endRushHourM;
        public DateTime LastAddedFeeDateTime { get; set; }

        
        public TollCalculator(IVehicleType VehicleType, 
            int startRushHourH, int startRushHourM, int endRushHourH, int endRushHourM, 
            DateTime entranceDate,
            List<DateTime> HolidayDates
            )
        {

            vehicleType = VehicleType;
            this.startRushHourH = startRushHourH;
            this.startRushHourM = startRushHourM;
            this.endRushHourH = endRushHourH;
            this.endRushHourM = endRushHourM;
            this.entranceDate = entranceDate;
            this.HolidayDates = HolidayDates;
        }


        private bool isWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        public bool IsFeeFree(DateTime date)
        {
            if (isWeekend(date)) return true;

            bool feeFreeCategory = this.vehicleType.IsFeeFree(this.vehicleType);
            if (feeFreeCategory) return feeFreeCategory;

            return HolidayDates.Any(x => x.Date == date.Date);
        }


        public int GetTollFee()
        {
            //Check if entrancedate is between range of rush hours
            if (
                this.entranceDate.Hour == this.startRushHourH && this.entranceDate.Minute <= this.startRushHourM
                &&
                this.entranceDate.Hour == this.endRushHourH && this.entranceDate.Minute >= this.endRushHourM
                )
            {
                this.LastAddedFeeDateTime = entranceDate;
                this.CurrentFee = this.MaxFee;
                return this.CurrentFee;
            }


            //If vehicle is charged with fee more then once inside same hour
            if (this.LastAddedFeeDateTime.Hour == entranceDate.Hour)
            {
                Array.Sort(this.FeeRanges);
                Array.Reverse(this.FeeRanges);
                this.CurrentFee += this.FeeRanges[0];
                return this.CurrentFee;
            }

            //generate random fee based on fee list , or based on specific entrance then apply specific fee rate from the list
            int randomIndex = new Random().Next(0, this.FeeRanges.Length - 1);
            this.CurrentFee += this.FeeRanges[randomIndex];
            this.LastAddedFeeDateTime = entranceDate;

            return this.CurrentFee;
        }
             
    }
}
