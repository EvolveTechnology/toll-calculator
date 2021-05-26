import java.util.*;

//List containing unique days
public class TollFreeDateList {

    private List<Date> tollFreeDates;

    public TollFreeDateList(){
    //Instanciate the List of all tollfreeDates
    //TODO 2021-26-05 Extract date information into either
    // a) A text file a user can add dates to 
    // b) A database, with the possibility of adding dates to
    tollFreeDates = new ArrayList<Date>(){};
    addTollFreeDate(new GregorianCalendar(2013, Calendar.JANUARY, 1).getTime());
    addTollFreeDate(new GregorianCalendar(2013, Calendar.MARCH, 28).getTime());
    addTollFreeDate(new GregorianCalendar(2013, Calendar.MARCH, 29).getTime());
    addTollFreeDate(new GregorianCalendar(2013, Calendar.APRIL, 1).getTime());
    addTollFreeDate(new GregorianCalendar(2013, Calendar.APRIL, 30).getTime());
    addTollFreeDate(new GregorianCalendar(2013, Calendar.MAY, 1).getTime());
    addTollFreeDate(new GregorianCalendar(2013, Calendar.MAY, 8).getTime());
    addTollFreeDate(new GregorianCalendar(2013, Calendar.MAY, 9).getTime());
    addTollFreeDate(new GregorianCalendar(2013, Calendar.JUNE, 5).getTime());
    addTollFreeDate(new GregorianCalendar(2013, Calendar.JUNE, 6).getTime());
    addTollFreeDate(new GregorianCalendar(2013, Calendar.JUNE, 21).getTime());
    //We add all days of July
    for (int i=1; i <= 31; i++){
        addTollFreeDate(new GregorianCalendar(2013, Calendar.JULY, i).getTime());
    }
    addTollFreeDate(new GregorianCalendar(2013, Calendar.NOVEMBER, 1).getTime());
    addTollFreeDate(new GregorianCalendar(2013, Calendar.DECEMBER, 24).getTime());
    addTollFreeDate(new GregorianCalendar(2013, Calendar.DECEMBER, 25).getTime());
    addTollFreeDate(new GregorianCalendar(2013, Calendar.DECEMBER, 26).getTime());
    addTollFreeDate(new GregorianCalendar(2013, Calendar.DECEMBER, 31).getTime());
    //TODO 2021-26-05 expose the tears in the same way as we do specific dates
    // 
    addWeekEndsOfYear(2013);
  }
      //TODO 2021-26-05 expose method as api for users if tollFreeDates are moved to a database
    public void addTollFreeDate(Date date){
        tollFreeDates.add(date);
    }
    //TODO 2021-26-05 expose method as api for users if tollFreeDates are moved to a database
    //Adds all saturdays and sundays for a specific year to the tollFreeDates list
    private void addWeekEndsOfYear(int year){
        Calendar calendar = GregorianCalendar.getInstance();
        //Loop through 12 months
        for (int month = 0; month < 12; month++){
            calendar.set(year, month, 1);
            int daysInMonth = calendar.getActualMaximum(Calendar.DAY_OF_MONTH);
            //Loop through all days for each month
            for (int d = 1;  d <= daysInMonth;  d++) {
                calendar.set(Calendar.DAY_OF_MONTH, d);
                int dayOfWeek = calendar.get(Calendar.DAY_OF_WEEK);
                if (dayOfWeek==Calendar.SATURDAY || dayOfWeek==Calendar.SUNDAY) {
                    addTollFreeDate(new GregorianCalendar(year, month, dayOfWeek).getTime());
        }

    public boolean contains(Date date){
        return tollFreeDates.contains(date);
    }
    
}
