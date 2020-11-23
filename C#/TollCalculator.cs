using System;
using System.Collections.Generic;
using System.Text;

namespace TollFeeCalculator
{

    public class TollCalculator
    {

        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */

        public int GetTollFee(Vehicle vehicle, DateTime[] sortedDates)
        {
            int[] extraDates = new int[sortedDates.Length];
            int startIndex = 0;

            foreach (DateTime date in sortedDates)
            {
                DateTime startDate = date;
                int addIndex = 0;
                bool endReached = false;
                while (!endReached)
                {
                    if (sortedDates.Length > (startIndex + addIndex + 1) && startDate.AddHours(1) > sortedDates[startIndex + addIndex + 1])
                    {
                        addIndex++;
                    }
                    else
                    {
                        endReached = true;
                        extraDates[startIndex] = addIndex; //startIndex,addIndex   
                    }

                }
                startIndex++;
            }
            List<dijkstraNode> nodeList = new List<dijkstraNode>();
            dijkstraNode startNode = new dijkstraNode();
            startNode._cost = 0;
            startNode._index = -1;
            nodeList.Add(startNode);
            DateTime intervalStart = sortedDates[0];
            int totalFee = 0;
            int index = 0;
            foreach (DateTime date in sortedDates)
            {
                dijkstraNode[] tempList = new dijkstraNode[nodeList.Count];
                nodeList.CopyTo(tempList);
                foreach (var node in tempList)
                {
                    if ((node._index + 1) == index)
                    {

                        int nrOfPassthroughs = extraDates[index];
                        int initialCost = GetTollFee(sortedDates[index], vehicle);
                        dijkstraNode indexNode = new dijkstraNode();
                        indexNode._cost = initialCost + node._cost;
                        indexNode._index = index;
                        nodeList.Add(indexNode);
                        int max = initialCost;
                        for (int x = 1; x <= nrOfPassthroughs; x++)
                        {
                            int compFee = GetTollFee(sortedDates[index + x], vehicle);
                            if (compFee > max)
                            {
                                max = compFee; //max within hour period
                            }
                            dijkstraNode newNode = new dijkstraNode();
                            newNode._cost = max + node._cost;
                            newNode._index = index + x;
                            nodeList.Add(newNode);
                        }
                    }
                }
                //Remove old indexes and resort?
                index++;
            }
            int min = 100;
            foreach (var node in nodeList)
            {
                //Console.WriteLine(node._index.ToString() + " Cost:" + node._cost.ToString() );
                if (min > node._cost && node._index == (sortedDates.Length - 1))  //Lowest total
                {
                    min = node._cost;

                }
            }
            totalFee = min;

            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        private bool IsTollFreeVehicle(Vehicle vehicle)
        {
            if (vehicle == null) return false;
            String vehicleType = vehicle.GetVehicleType();
            return vehicleType.Equals(VehicleTypes.Motorbike.ToString()) ||
                   vehicleType.Equals(VehicleTypes.Tractor.ToString()) ||
                   vehicleType.Equals(VehicleTypes.Emergency.ToString()) ||
                   vehicleType.Equals(VehicleTypes.Diplomat.ToString()) ||
                   vehicleType.Equals(VehicleTypes.Foreign.ToString()) ||
                   vehicleType.Equals(VehicleTypes.Military.ToString());
        }

        public int GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            int hour = date.Hour;
            int minute = date.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            else if (hour == 7) return 18;
            else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            else if ((hour == 8 && minute >= 30 && minute <= 59) || hour >= 9 && hour <= 14) return 8;
            else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            else if (hour == 15 && minute >= 30 || hour == 16 && minute <= 59) return 18;
            else if (hour == 17) return 13;
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            else return 0;
        }

        private Boolean IsTollFreeDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            if (year == 2020)
            {
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
            }
            return false;
        }

        private enum VehicleTypes
        {
            Motorbike = 0,
            Tractor = 1,
            Emergency = 2,
            Diplomat = 3,
            Foreign = 4,
            Military = 5,
            OrdinaryCar = 10

        }

        class dijkstraNode
        {
            public int _cost { get; set; }
            public int _index { get; set; }
        }
    }
}
