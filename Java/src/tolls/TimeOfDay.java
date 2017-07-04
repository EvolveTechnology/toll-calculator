package tolls;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

class TimeOfDay {
    private final int hour;
    private final int minute;

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

    int getFee() {
        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30) return 13;
        else if (hour == 7 && minute >= 0) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14 && minute >= 30) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 0 || hour == 16) return 18;
        else if (hour == 17 && minute >= 0) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }
}
