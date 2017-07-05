package tolls;

public interface HolidayCalendar {
    CalendarDay easterDay(int year);
    boolean isMidsummerEve(CalendarDay calendarDay);
}
