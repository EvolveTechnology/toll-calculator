using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollCalculatorApp.Models
{
    internal class TollFreeDays
    {
        public int Month { get; set; }
        public int[] Day { get; set; }

        private static readonly List<TollFreeDays> _tollFreeDays = new List<TollFreeDays>
        {
            new TollFreeDays {Month = 1, Day = new[] {1}},
            new TollFreeDays {Month = 3, Day = new[] {28, 29}},
            new TollFreeDays {Month = 4, Day = new[] {1, 30}},
            new TollFreeDays {Month = 5, Day = new[] {1, 8, 9}},
            new TollFreeDays {Month = 6, Day = new[] {5, 6, 21}},
            new TollFreeDays {Month = 11, Day = new[] {1}},
            new TollFreeDays {Month = 12, Day = new[] {24,25,26,31}}
        };

        public static List<TollFreeDays> GetTollFreeDays()
        {
            return _tollFreeDays;
        }
    }
}