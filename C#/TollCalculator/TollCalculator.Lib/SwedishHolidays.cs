using TollCalculator.Lib.Models;

namespace TollCalculator.Lib
{
    public class SwedishHolidays
    {
        public static YearlyDate[] StaticPublicHolidays { get; } =
        {
            new YearlyDate(1, 1), //Nyårsdagen
            new YearlyDate(1, 5), //Trettondagsafton
            new YearlyDate(1, 6), //Trettondedag jul
            new YearlyDate(4, 30), //Valborgsmässoafton
            new YearlyDate(5, 1), //Första maj
            new YearlyDate(6, 6), //Sveriges nationaldag
            new YearlyDate(12, 24), //Julafton
            new YearlyDate(12, 25), //Juldagen
            new YearlyDate(12, 26), //Annandag jul
            new YearlyDate(12, 31), //Nyårsafton
        };
    }
}