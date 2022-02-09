import java.util.*;

// Update: Added class to make less code in the TollCalculator
public class MyCalendar {

    private Calendar calendar;

    public MyCalendar(Date date){
        // Changing date to GregorianCalendar
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.setTime(date);
        this.calendar = calendar;
    }

    // Get the current value from the GregorianCalendar
    public int get(int timeType){
        // Try/Catch in cases its not a valid 'Calendar.X' input
        try{
            return calendar.get(timeType);
        }
        catch(Exception ignored){
            return 0;
        }
    }
}