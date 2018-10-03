
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace TollFeeCalculator.Tests
{
    public sealed class Assert
    {
        private static readonly object _gate = new object();
        private static Assert _instance = null;

        private int NumberOfSuccessfullTests { get; set; }
        private int NumberOfTotalTests { get; set; }

        private Assert ()
        {
        }

        public static Assert Instance 
        {
            get 
            {
                //Note: "dubble check"-lock for performance.
                if (_instance == null)
                {
                    lock(_gate)
                    {
                        if (_instance == null)
                            _instance = new Assert();
                    }
                }

                return _instance;
            }
        }

        public void AreEqual(object a, object b)
        {
            var assertingMethod = new StackTrace()
                .GetFrame(1)
                .GetMethod()
                .Name;

            Console.Write(assertingMethod + " [");

            this.NumberOfTotalTests += 1;
            
            if (a.Equals(b))
            {
                this.NumberOfSuccessfullTests += 1;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Success");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Failure");
            }
            
            Console.ResetColor();
            Console.WriteLine("]");
        }

        public override string ToString()
            => $"{NumberOfSuccessfullTests}/{NumberOfTotalTests} successfull tests";
    }
}