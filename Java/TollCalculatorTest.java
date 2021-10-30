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
  @Before
  public void setUp() throws Exception {
  }

  @After
  public void tearDown() throws Exception {
  }


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
}