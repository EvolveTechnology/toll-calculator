package util;

public final class TimeOfDay {
    public final int hour;
    public final int minute;
    public final int second;

    public TimeOfDay(int hour, int minute, int second)
    {
        this.hour = hour;
        this.minute = minute;
        this.second = second;
    }

    public static TimeOfDay midnight()
    {
        return new TimeOfDay(0, 0, 0);
    }
}
