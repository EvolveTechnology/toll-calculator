import java.time.LocalDate;
import java.util.HashSet;
import java.util.Set;

public class SwedishHolidays {
	
	private Set<LocalDate> holidays = new HashSet<>();
	
	SwedishHolidays(int year) {
		LocalDate easterSunday = getEasterSundayDate(year);

		holidays.add(LocalDate.of(year, 1, 1)); //newYearsDay
		holidays.add(LocalDate.of(year, 1, 6)); //epiphany
		holidays.add(easterSunday.minusDays(2)); //goodFriday
		holidays.add(easterSunday.plusDays(1)); //easterMonday
		holidays.add(LocalDate.of(year, 5, 1)); //mayDay
		holidays.add(easterSunday.plusDays(39)); //ascension
		holidays.add(LocalDate.of(year, 6, 6)); //nationalDay
		holidays.add(LocalDate.of(year, 12, 25)); //christmasDay
		holidays.add(LocalDate.of(year, 12, 26)); //boxingDay
	}
	
	public boolean isHoliday(LocalDate date) {
		return holidays.contains(date);
	}
	
    private LocalDate getEasterSundayDate(int year) {
        int a = year % 19,
            b = year / 100,
            c = year % 100,
            d = b / 4,
            e = b % 4,
            g = (8 * b + 13) / 25,
            h = (19 * a + b - d - g + 15) % 30,
            j = c / 4,
            k = c % 4,
            m = (a + 11 * h) / 319,
            r = (2 * e + 2 * j - k - h + m + 32) % 7,
            n = (h - m + r + 90) / 25,
            p = (h - m + r + n + 19) % 32;
        
        return LocalDate.of(year, n, p);
    }
}
