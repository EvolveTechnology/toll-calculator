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
        switch (hour) {
            case 6:
                return minute < 30 ? 8 : 13;
            case 7:
                return 18;
            case 8:
                return minute < 30 ? 13 : 8;
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
                return 8;
            case 15:
                return minute < 30 ? 13 : 18;
            case 16:
                return 18;
            case 17:
                return 13;
            case 18:
                return minute < 30 ? 8 : 0;
            default:
                return 0;
        }
    }
}
