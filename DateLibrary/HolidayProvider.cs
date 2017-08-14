using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateLibrary
{
    public class HolidayProvider
    {
        private static Dictionary<int, List<DateTime>> holidays = new Dictionary<int, List<DateTime>>();

        public static bool IsHoliday(DateTime date)
        {
            return GetHolidays(date.Year).Contains(date.Date);
        }

        private static List<DateTime> GetHolidays(int year)
        {
            if (holidays.ContainsKey(year)) return holidays[year];
            return holidays[year] = GetListOfHolidays(year);
        }

        public static List<DateTime> GetListOfHolidays(int year)
        {
            var easter = GetEaster(year);
            var midsummer = FindDay(year, 6, 20, DayOfWeek.Saturday);
            var allSaints = FindDay(year, 10, 31, DayOfWeek.Saturday);

            return new List<DateTime> {
                new DateTime(year, 1, 1), //nyårsdag
                new DateTime(year, 1, 6), //trettondedag
                easter.AddDays(-2), //långfredag
                easter, //påskdag
                easter.AddDays(1), //annandag påsk
                new DateTime(year, 5, 1), //1a maj
                easter.AddDays(39), //kristi himmelfärd
                easter.AddDays(49), //pingst
                new DateTime(year, 6, 6), //nationaldag
                midsummer, //midsommar
                allSaints,//allhellgona
                new DateTime(year, 12, 25), //juldag
                new DateTime(year, 12, 26), //annandag jul
            };

        }

        //Function is taken from nager but is refactored for ease of read and optimization
        private static DateTime GetEaster(int year)
        {
            int g = year % 19;
            int c = year / 100;
            int h = h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25)
                                                + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) *
                        (int)(29 / (h + 1)) * (int)((21 - g) / 11));

            var day = i - ((year + (int)(year / 4) +
                          i + 2 - c + (int)(c / 4)) % 7) + 28;
            var month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }
            return new DateTime(year, month, day);
        }


        //Function is taken from nager but is refactored for optimization
        public static DateTime FindDay(int year, int month, int day, DayOfWeek dayOfWeek)
        {
            var start = new DateTime(year, month, day);
            return start.AddDays((7 + dayOfWeek - start.DayOfWeek) % 7);
        }
    }
}
