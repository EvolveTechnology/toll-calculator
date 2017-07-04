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
        if (isBefore(6, 0)) {
            return 0;
        } else if (isBefore(6, 30)) {
            return 8;
        } else if (isBefore(7, 0)) {
            return 13;
        } else if (isBefore(8, 0)) {
            return 18;
        } else if (isBefore(8, 30)) {
            return 13;
        } else if (isBefore(15, 0)) {
            return 8;
        } else if (isBefore(15, 30)) {
            return 13;
        } else if (isBefore(17, 0)) {
            return 18;
        } else if (isBefore(18, 0)) {
            return 13;
        } else if (isBefore(18, 30)) {
            return 8;
        } else {
            return 0;
        }
    }

    private boolean isBefore(int hour, int minute) {
        return this.hour < hour || (this.hour == hour && this.minute < minute);
    }
}
