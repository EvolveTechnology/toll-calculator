package tollfee;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import static org.junit.Assert.*;
import org.junit.Test;

/**
 * Test class TollCalculator.
 */
public class TollCalculatorTest {

  /**
   * Test of getTollFee method, of class TollCalculator.
   *
   * Check that getTollFee method throws NullPointerException if both parameters are null.
   */
  @Test(expected = NullPointerException.class)
  public void testGetTollFeeNullPointers() {
    TollCalculator instance = new TollCalculator();
    instance.getTollFee(null, null);
  }

  /**
   * Test of getTollFee method, of class TollCalculator.
   *
   * Check that getTollFee method throws NullPointerException if vehicle is null.
   */
  @Test(expected = NullPointerException.class)
  public void testGetTollFeeNullPointersVehicle() {
    ArrayList<Date> dates = new ArrayList<>(Arrays.asList(
      new GregorianCalendar(2013, Calendar.APRIL, 15, 15, 50).getTime(),
      new GregorianCalendar(2013, Calendar.JUNE, 19, 17, 10).getTime()
    ));

    TollCalculator instance = new TollCalculator();
    instance.getTollFee(null, dates);
  }

  /**
   * Test of getTollFee method, of class TollCalculator.
   *
   * Check that getTollFee method throws NullPointerException if list of dates is null.
   */
  @Test(expected = NullPointerException.class)
  public void testGetTollFeeNullPointersDates() {
    Car car = new Car();

    TollCalculator instance = new TollCalculator();
    instance.getTollFee(car, null);
  }

  /**
   * Test of getTollFee method, of class TollCalculator.
   *
   * Here we check that for every hour we always get charged only one time with the highest fee for that hour
   * and that we get charged no more than 60 per day.
   */
  @Test
  public void testGetTollFeeDayAndHour() {
    Car car = new Car();

    ArrayList<Date> dates = new ArrayList<>(Arrays.asList(
      // Check that two dates within the same hour only returns the highest fee (13)
      new GregorianCalendar(2013, Calendar.FEBRUARY, 15, 14, 50).getTime(), // Fee is 8
      new GregorianCalendar(2013, Calendar.FEBRUARY, 15, 15, 10).getTime(), // Fee is 13
      new GregorianCalendar(2013, Calendar.FEBRUARY, 15, 15, 15).getTime(), // Fee is 13
      // Check that within the same day fee is never higher than 60
      new GregorianCalendar(2013, Calendar.MARCH, 15, 15, 34).getTime(), // Fee is 18
      new GregorianCalendar(2013, Calendar.MARCH, 15, 16, 44).getTime(), // Fee is 18
      new GregorianCalendar(2013, Calendar.MARCH, 15, 6, 44).getTime(), // Fee is 13
      new GregorianCalendar(2013, Calendar.MARCH, 15, 7, 46).getTime(), // Fee is 18
      new GregorianCalendar(2013, Calendar.MARCH, 15, 17, 49).getTime() // Fee is 13
    ));

    TollCalculator instance = new TollCalculator();
    int result = instance.getTollFee(car, dates);

    assertEquals(73, result);
  }

  /**
   * Test of getTollFee method, of class TollCalculator.
   *
   * Test that weekends and public holidays are fee-free. Note that this only checks
   * year 2013. We are limiting the implementation to work for only this year.
   */
  @Test
  public void testGetTollFeeWeekendsAndHolidays() {
    Car car = new Car();

    ArrayList<Date> dates = new ArrayList<>(Arrays.asList(
      // Check that weekend gives no fee
      new GregorianCalendar(2013, Calendar.APRIL, 6, 19, 50).getTime(),
      new GregorianCalendar(2013, Calendar.APRIL, 14, 15, 10).getTime(),
      // Check that holidays gives no fee
      new GregorianCalendar(2013, Calendar.MAY, 1, 15, 34).getTime(),
      new GregorianCalendar(2013, Calendar.DECEMBER, 25, 16, 44).getTime()
    ));

    TollCalculator instance = new TollCalculator();
    int result = instance.getTollFee(car, dates);

    assertEquals(0, result);
  }

  /**
   * Test of getTollFee method, of class TollCalculator.
   *
   * Check that a fee-free vehicle gets no fee.
   */
  @Test
  public void testGetTollFeeFreeVehicles() {
    Motorbike motorbike = new Motorbike();

    ArrayList<Date> dates = new ArrayList<>(Arrays.asList(
      new GregorianCalendar(2013, Calendar.FEBRUARY, 15, 14, 50).getTime(),
      new GregorianCalendar(2013, Calendar.FEBRUARY, 15, 15, 10).getTime(),
      new GregorianCalendar(2013, Calendar.FEBRUARY, 15, 15, 15).getTime(),
      new GregorianCalendar(2013, Calendar.MARCH, 15, 15, 34).getTime(),
      new GregorianCalendar(2013, Calendar.MARCH, 15, 16, 44).getTime(),
      new GregorianCalendar(2013, Calendar.MARCH, 15, 6, 44).getTime(),
      new GregorianCalendar(2013, Calendar.MARCH, 15, 7, 46).getTime(),
      new GregorianCalendar(2013, Calendar.MARCH, 15, 17, 49).getTime()
    ));

    TollCalculator instance = new TollCalculator();
    int result = instance.getTollFee(motorbike, dates);

    assertEquals(0, result);
  }
}
