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

    public TimeOfDay(int hour, int minute) {
        this.hour = hour;
        this.minute = minute;
    }

    int getFee() {
        if (hour == 6 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30) return 13;
        else if (hour == 7) return 18;
        else if (hour == 8 && minute <= 29) return 13;
        else if (hour == 8 && minute >= 30) return 8;
        else if (hour > 8 && hour <= 14) return 8;
        else if (hour == 15 && minute <= 29) return 13;
        else if (hour == 15 || hour == 16) return 18;
        else if (hour == 17) return 13;
        else if (hour == 18 && minute <= 29) return 8;
        else return 0;
    }
}
