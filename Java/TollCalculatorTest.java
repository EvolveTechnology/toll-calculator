import org.junit.After;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.JUnit4;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

@RunWith(JUnit4.class)
public class TollCalculatorTest {

  TollCalculator tollCalculator = new TollCalculator();

  @Test
  public void testTollFreeVehicle() throws NoSuchMethodException,
          InvocationTargetException, IllegalAccessException, ClassNotFoundException {

    Class<?> base = Class.forName("TollCalculator");
    Method method = base.getDeclaredMethod("isTollFreeVehicle", Vehicle.class);
    method.setAccessible(true);

    Boolean isCarTollFree = (Boolean) method.invoke(tollCalculator, (Vehicle) () -> "Car");
    Assert.assertFalse("Test failed", isCarTollFree);

    Boolean isDiplomatVehicleTollFree = (Boolean) method.invoke(tollCalculator, (Vehicle) () -> "Diplomat");
    Assert.assertTrue("Test failed", isDiplomatVehicleTollFree);
  }

  @Test
  public void testTollFreeDate() throws NoSuchMethodException,
          InvocationTargetException, IllegalAccessException, ClassNotFoundException{

    Class<?> base = Class.forName("TollCalculator");
    Method method = base.getDeclaredMethod("isTollFreeDate", Date.class);
    method.setAccessible(true);

    GregorianCalendar saturday = new GregorianCalendar(2021, Calendar.OCTOBER, 30);
    GregorianCalendar sunday = new GregorianCalendar(2021, Calendar.OCTOBER, 31);
    GregorianCalendar monday = new GregorianCalendar(2021, Calendar.NOVEMBER, 2);

    //test for Saturday
    Boolean isSaturdayTollFreeDay = (Boolean) method.invoke(tollCalculator, saturday.getTime());
    Assert.assertTrue("Test failed", isSaturdayTollFreeDay);

    //test for Sunday
    Boolean isSundayTollFreeDay = (Boolean) method.invoke(tollCalculator, sunday.getTime());
    Assert.assertTrue("Test failed", isSundayTollFreeDay);

    //test for Tuesday
    Boolean isMondayTollFreeDay = (Boolean) method.invoke(tollCalculator, monday.getTime());
    Assert.assertFalse("Test failed", isMondayTollFreeDay);
  }

  @Test
  public void testMaxTollFeeInADay(){
    GregorianCalendar dateMorning1 =
            new GregorianCalendar(2021, Calendar.NOVEMBER, 2, 6, 30);

    GregorianCalendar dateMorning2 =
            new GregorianCalendar(2021, Calendar.NOVEMBER, 2, 7, 15);

    GregorianCalendar dateAfternoon1 =
            new GregorianCalendar(2021, Calendar.NOVEMBER, 2, 12, 30);

    GregorianCalendar dateAfternoon2 =
            new GregorianCalendar(2021, Calendar.NOVEMBER, 2, 12, 30);

    GregorianCalendar dateEvening1 =
            new GregorianCalendar(2021, Calendar.NOVEMBER, 2, 15, 30);

    GregorianCalendar dateEvening2 =
            new GregorianCalendar(2021, Calendar.NOVEMBER, 2, 17, 30);

    Date[] dates = {dateMorning1.getTime(), dateAfternoon1.getTime(), dateEvening1.getTime(),
            dateMorning2.getTime(), dateAfternoon2.getTime(), dateEvening2.getTime()};

    int totalTollFees = tollCalculator.getTollFee((Vehicle) () -> "Car", dates);

    // max toll amount for a vehicle for a day cannot exceed 60
    Assert.assertEquals(60, totalTollFees);
  }

  @Test
  public void testChargeOncePerHour(){
    GregorianCalendar dateMorning1 =
            new GregorianCalendar(2021, Calendar.NOVEMBER, 2, 6, 10);

    GregorianCalendar dateMorning2 =
            new GregorianCalendar(2021, Calendar.NOVEMBER, 2, 6, 45);

    Date[] dates = {dateMorning1.getTime(), dateMorning2.getTime()};
    int totalTollFees = tollCalculator.getTollFee((Vehicle) () -> "Car", dates);

    // max toll amount for a vehicle within an hour will be charged only once and
    // will take the highest charged amount
    // e.g between 6 AM - 7 AM, if a car makes two trips, then the charges are 8 and 13.
    // only 13 should be charged as its the highest in the span of one hour
    Assert.assertEquals(13, totalTollFees);
  }
}