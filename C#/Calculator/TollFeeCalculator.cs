using System;
using System.Collections.Generic;
using System.Linq;

namespace TollFeeCalculator
{

    public enum VehicleType {
        Car = 0,
        Motorbike = 1,
        Tractor = 2,
        Emergency = 3,
        Diplomat = 4,
        Foreign = 5,
        Military = 6
    };

    public struct TollGatePassageData {
        public VehicleType Vehicle;
        public DateTime Date;
        public List<TimeSpan> Times;
    };

    public class TollFeeCalc
    {

        private readonly TollGatePassageData VehicleData;

        public static void Main() {}

        public TollFeeCalc(TollGatePassageData d)
        {
            if (d.Times.Any())
            {
                d.Times.Sort(); // Sort timestamps in ascending order
                this.VehicleData = d;
            }
            else
            {
                throw new ArgumentException("The list of times provided in the argument is empty");
            }

        }

        public int Calc()
        {
            if (this.VehicleData.Vehicle == VehicleType.Car)
            {
                if (Calendar.IsTollFreeDate(this.VehicleData.Date))
                {
                    return 0;
                }
                else
                {
                    int sum = 0;
                    TimeSpan prevTime = this.VehicleData.Times.First() - new TimeSpan(1, 0, 0); // Offset so that it "enters the loop"
                    foreach (TimeSpan time in this.VehicleData.Times)
                    {
                        if ((time - prevTime).Hours >= 1)
                        {
                            sum += TollFeeLookup.Fee(time);
                        }
                        prevTime = time;
                    }
                    return Math.Min(sum, Settings.MAXIMUM_FEE);
                }
            }
            else
            {
                // All other vehicle types are toll free
                return 0;
            }
        }

    }

}
