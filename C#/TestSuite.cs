using System;
using System.Diagnostics;

namespace TollCalc.C_
{
    class TestSuite
    {

        public static void Main()
        {

            TollCalculator tc = new TollCalculator();



            // Testing one time, two different vehicle types
            DateTime[] testDate1 = new DateTime[]
            {
                new DateTime(2013, 10, 1, 7, 50, 0)
            };
            Debug.Assert(tc.GetTollFee(VehicleType.Default, testDate1) == 18);
            Debug.Assert(tc.GetTollFee(VehicleType.Diplomat, testDate1) == 0);



            // Testing two times
            DateTime[] testDate2 = new DateTime[]
            {
                new DateTime(2013, 10, 1, 7, 50, 0),
                new DateTime(2013, 10, 1, 9, 50, 0)
            };
            Debug.Assert(tc.GetTollFee(VehicleType.Default, testDate2) == 26);



            // Testing multiple times, but some are too close in time to be charged
            DateTime[] testDate3 = new DateTime[]
            {
                new DateTime(2013, 10, 1, 7, 50, 0),
                new DateTime(2013, 10, 1, 9, 50, 0),
                new DateTime(2013, 10, 1, 9, 50, 0),
                new DateTime(2013, 10, 1, 9, 50, 0),
            };
            Debug.Assert(tc.GetTollFee(VehicleType.Default, testDate3) == 26);


            // Testing multiple times
            DateTime[] testDate4 = new DateTime[]
            {
                new DateTime(2013, 10, 1, 7, 50, 0),
                new DateTime(2013, 10, 1, 9, 50, 0),
                new DateTime(2013, 10, 1, 11, 50, 0),
                new DateTime(2013, 10, 1, 13, 50, 0),
            };
            Debug.Assert(tc.GetTollFee(VehicleType.Default, testDate4) == 42);



            // Testing Maximum toll fee
            DateTime[] testDate5 = new DateTime[]
            {
                new DateTime(2013, 10, 1, 7, 50, 0),
                new DateTime(2013, 10, 1, 9, 50, 0),
                new DateTime(2013, 10, 1, 11, 50, 0),
                new DateTime(2013, 10, 1, 13, 50, 0),
                new DateTime(2013, 10, 1, 16, 50, 0),
                new DateTime(2013, 10, 1, 17, 50, 0),
            };
            Debug.Assert(tc.GetTollFee(VehicleType.Default, testDate5) == 60);



            // Testing toll free day
            DateTime[] testDate6 = new DateTime[]
            {
                new DateTime(2013, 3, 29, 7, 50, 0),
                new DateTime(2013, 3, 29, 9, 50, 0),
                new DateTime(2013, 3, 29, 11, 50, 0),
                new DateTime(2013, 3, 29, 13, 50, 0),
                new DateTime(2013, 3, 29, 16, 50, 0),
                new DateTime(2013, 3, 29, 17, 50, 0),
            };
            Debug.Assert(tc.GetTollFee(VehicleType.Default, testDate6) == 0);



            // Testing unordered times
            DateTime[] testDate7 = new DateTime[]
            {
                new DateTime(2013, 10, 1, 11, 50, 0),
                new DateTime(2013, 10, 1, 9, 50, 0),
                new DateTime(2013, 10, 1, 7, 50, 0),
                new DateTime(2013, 10, 1, 16, 50, 0),
                new DateTime(2013, 10, 1, 13, 50, 0),
                new DateTime(2013, 10, 1, 17, 50, 0),
            };
            Debug.Assert(tc.GetTollFee(VehicleType.Default, testDate7) == 60);



            // Testing two times at different dates, should throw an exception
            try
            {
                DateTime[] testDate8 = new DateTime[]
                {
                    new DateTime(2013, 10, 1, 7, 50, 0),
                    new DateTime(2013, 10, 2, 9, 50, 0)
                };
                tc.GetTollFee(VehicleType.Default, testDate8);
                Debug.Assert(false);
            }
            catch (Exception e)
            {
                Debug.Assert(e.Message == "All timestamps should be on the same day.");
            }



            Console.WriteLine("All test passed.");
            Console.Read();
        }

    }
}
