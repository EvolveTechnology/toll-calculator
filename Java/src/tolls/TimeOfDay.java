package tolls;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

class TimeOfDay {
    final int hour;
    final int minute;

    TimeOfDay(Date date) {
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.setTime(date);
        this.hour = calendar.get(Calendar.HOUR_OF_DAY);
        this.minute = calendar.get(Calendar.MINUTE);
    }

    private TimeOfDay(int hour, int minute) {
        this.hour = hour;
        this.minute = minute;
    }
}
