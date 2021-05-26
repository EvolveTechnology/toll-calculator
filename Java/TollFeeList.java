import java.util.*;

public class TollFeeList {
    private HashMap<Integer, Integer> TollFeesPerMinute;

    public TollFeeList(){
    setDefaultTollFees();
    }

    private void setDefaultTollFees(){
        setTollFeeForTimeSpan(0, 0, 6, 0, 0); //Set fee to 0 from 00:00 to 06:00
        setTollFeeForTimeSpan(6, 0, 6, 29, 8); //Set fee to 8 from 06:00 to 06:29
        setTollFeeForTimeSpan(6, 30, 6, 59, 13); //Set fee to 13 from 06:30 to 06:59
        setTollFeeForTimeSpan(7, 0, 7, 59, 18); //Set fee to 18 from 07:00 to 07:59
        setTollFeeForTimeSpan(8, 0, 8, 29, 13); //Set fee to 13 from 08:00 to 08:30
        setTollFeeForTimeSpan(8, 30, 14, 59, 8); //Set fee to 8 from 08:30 to 14:59
        setTollFeeForTimeSpan(15, 0, 15, 29, 13); //Set fee to 13 from 15:00 to 15:29
        setTollFeeForTimeSpan(15, 30, 16, 59, 18); //Set fee to 18 from 15:30 to 16:59
        setTollFeeForTimeSpan(17, 0, 17, 59, 13); //Set fee to 18 from 17:00 to 17:59
        setTollFeeForTimeSpan(18, 0, 18, 29, 8); //Set fee to 8 from 18:00 to 18:29
        setTollFeeForTimeSpan(18, 30, 24, 0, 0); //Set fee to 0 from 18:30 to 24:00
    }

    //TODO expose as api or UI if user needs to redefine TollFees during runtime
    public void setTollFeeForTimeSpan(int startHour, int startMinute, int endHour, int endMinute, int fee){
        int startMinutes = startHour*60+startMinute;
        int endMinutes = endHour*60+endMinute;
        for (int i = startMinutes; i <= endMinutes; i++){
            TollFeesPerMinute.put(i, fee);
        }
    }

    public int getTollFee(int hour, int minute){
        int minutes = hour*60+minute;
        return TollFeesPerMinute.get(minutes);

    }
    
}
