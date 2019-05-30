import mockit.Mock;
import mockit.MockUp;
import org.junit.jupiter.api.Test;
import vehicles.*;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

import static org.junit.jupiter.api.Assertions.*;

class TollCalculatorTest {

  private final TollCalculator tollCalculator;

  TollCalculatorTest() {
    tollCalculator = new TollCalculator();
  }

  @Test
  void testInvalidData() {
    Motorbike motorbike = new Motorbike();
    Date today = Calendar.getInstance().getTime();
    assertThrows(
        InvalidDataException.class, () -> tollCalculator.getTollFee(motorbike, today, null));

    assertThrows(InvalidDataException.class, () -> tollCalculator.getTollFee(null, today));
  }

  @Test
  void testTollFreeVehicle() throws InvalidDataException {
    Date notTollFreeDate = new GregorianCalendar(2019, Calendar.MAY, 29, 16, 43).getTime();

    Motorbike motorbike = new Motorbike();
    assertEquals(0, tollCalculator.getTollFee(motorbike, notTollFreeDate));

    Tractor tractor = new Tractor();
    assertEquals(0, tollCalculator.getTollFee(tractor, notTollFreeDate));

    Emergency emergency = new Emergency();
    assertEquals(0, tollCalculator.getTollFee(emergency, notTollFreeDate));

    Diplomat diplomat = new Diplomat();
    assertEquals(0, tollCalculator.getTollFee(diplomat, notTollFreeDate));

    Foreign foreign = new Foreign();
    assertEquals(0, tollCalculator.getTollFee(foreign, notTollFreeDate));

    Military military = new Military();
    assertEquals(0, tollCalculator.getTollFee(military, notTollFreeDate));

    Car car = new Car();
    assertNotEquals(0, tollCalculator.getTollFee(car, notTollFreeDate));
  }

  @Test
  void testTollFreeDate() throws InvalidDataException {
    // WEEKENDS
    Date saturday = new GregorianCalendar(2019, Calendar.MAY, 25, 16, 43).getTime();
    Date sunday = new GregorianCalendar(2019, Calendar.MAY, 26, 16, 43).getTime();
    Car car = new Car();
    assertEquals(0, tollCalculator.getTollFee(car, saturday));
    assertEquals(0, tollCalculator.getTollFee(car, sunday));

    // HOLIDAYS
    Date newYear = new GregorianCalendar(2019, Calendar.JANUARY, 1, 16, 43).getTime();
    assertEquals(0, tollCalculator.getTollFee(car, newYear));
  }

  @Test
  void testCalculateToll() {
    // HOUR 6
    Date hour6_1 = new GregorianCalendar(2019, Calendar.MAY, 25, 6, 23).getTime();
    int expected6_1 = 8;
    assertEquals(expected6_1, tollCalculator.calculateTollFee(hour6_1));

    Date hour6_2 = new GregorianCalendar(2019, Calendar.MAY, 25, 6, 43).getTime();
    int expected6_2 = 13;
    assertEquals(expected6_2, tollCalculator.calculateTollFee(hour6_2));

    // HOUR 7
    Date hour7 = new GregorianCalendar(2019, Calendar.MAY, 25, 7, 23).getTime();
    int expected7 = 18;
    assertEquals(expected7, tollCalculator.calculateTollFee(hour7));

    // HOUR 8
    Date hour8_1 = new GregorianCalendar(2019, Calendar.MAY, 25, 8, 23).getTime();
    int expected8_1 = 13;
    assertEquals(expected8_1, tollCalculator.calculateTollFee(hour8_1));

    Date hour8_2 = new GregorianCalendar(2019, Calendar.MAY, 25, 8, 43).getTime();
    int expected8_2 = 8;
    assertEquals(expected8_2, tollCalculator.calculateTollFee(hour8_2));

    // HOUR 9-14
    Date hour9_1 = new GregorianCalendar(2019, Calendar.MAY, 25, 9, 23).getTime();
    int expected9_1 = 0;
    assertEquals(expected9_1, tollCalculator.calculateTollFee(hour9_1));

    Date hour14_1 = new GregorianCalendar(2019, Calendar.MAY, 25, 14, 23).getTime();
    assertEquals(expected9_1, tollCalculator.calculateTollFee(hour14_1));

    Date hour9_2 = new GregorianCalendar(2019, Calendar.MAY, 25, 9, 43).getTime();
    int expected9_2 = 8;
    assertEquals(expected9_2, tollCalculator.calculateTollFee(hour9_2));

    Date hour14_2 = new GregorianCalendar(2019, Calendar.MAY, 25, 14, 43).getTime();
    assertEquals(expected9_2, tollCalculator.calculateTollFee(hour14_2));

    // HOUR 15
    Date hour15_1 = new GregorianCalendar(2019, Calendar.MAY, 25, 15, 23).getTime();
    int expected15_1 = 13;
    assertEquals(expected15_1, tollCalculator.calculateTollFee(hour15_1));

    Date hour15_2 = new GregorianCalendar(2019, Calendar.MAY, 25, 15, 43).getTime();
    int expected15_2 = 18;
    assertEquals(expected15_2, tollCalculator.calculateTollFee(hour15_2));

    // HOUR 16
    Date hour16 = new GregorianCalendar(2019, Calendar.MAY, 25, 16, 23).getTime();
    int expected16 = 18;
    assertEquals(expected16, tollCalculator.calculateTollFee(hour16));

    // HOUR 17
    Date hour17 = new GregorianCalendar(2019, Calendar.MAY, 25, 17, 23).getTime();
    int expected17 = 13;
    assertEquals(expected17, tollCalculator.calculateTollFee(hour17));

    // HOUR 18
    Date hour18_1 = new GregorianCalendar(2019, Calendar.MAY, 25, 18, 23).getTime();
    int expected18_1 = 8;
    assertEquals(expected18_1, tollCalculator.calculateTollFee(hour18_1));

    Date hour18_2 = new GregorianCalendar(2019, Calendar.MAY, 25, 18, 43).getTime();
    int expected18_2 = 0;
    assertEquals(expected18_2, tollCalculator.calculateTollFee(hour18_2));
  }

  @Test
  void testGetTollFee() throws InvalidDataException {
    new MockUp<ApiHelper>() {
      @Mock
      boolean isHoliday(int year, int month, int day) {
        return false;
      }
    };

    // Maximum toll for a day
    Car car = new Car();
    Date date1 = new GregorianCalendar(2019, Calendar.MAY, 6, 6, 43).getTime();
    Date date2 = new GregorianCalendar(2019, Calendar.MAY, 6, 7, 43).getTime();
    Date date3 = new GregorianCalendar(2019, Calendar.MAY, 6, 9, 43).getTime();
    Date date4 = new GregorianCalendar(2019, Calendar.MAY, 6, 10, 43).getTime();
    Date date5 = new GregorianCalendar(2019, Calendar.MAY, 6, 11, 43).getTime();
    Date date6 = new GregorianCalendar(2019, Calendar.MAY, 6, 15, 43).getTime();
    Date date7 = new GregorianCalendar(2019, Calendar.MAY, 6, 16, 43).getTime();
    Date date8 = new GregorianCalendar(2019, Calendar.MAY, 6, 17, 43).getTime();

    assertEquals(
        TollCalculator.MAX_TOLL,
        tollCalculator.getTollFee(car, date1, date2, date3, date4, date5, date6, date7, date8));

    // Multiple toll in one hour
    Date date1_1 = new GregorianCalendar(2019, Calendar.MAY, 6, 6, 23).getTime();
    Date date1_2 = new GregorianCalendar(2019, Calendar.MAY, 6, 6, 43).getTime();
    int highest_toll = 13;
    assertEquals(highest_toll, tollCalculator.getTollFee(car, date1_1, date1_2));

    Date date2_1 = new GregorianCalendar(2019, Calendar.MAY, 6, 6, 43).getTime();
    Date date2_2 = new GregorianCalendar(2019, Calendar.MAY, 6, 7, 23).getTime();
    highest_toll = 18;
    assertEquals(highest_toll, tollCalculator.getTollFee(car, date2_1, date2_2));

  }
}
