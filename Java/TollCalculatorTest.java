import org.junit.Test;

import java.util.Calendar;
import java.util.GregorianCalendar;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertTrue;

public class TollCalculatorTest {

    TollCalculator tollCalculator = new TollCalculator();

    @Test
    public void testMinAndMaxTollFee() {
        Calendar mondayAfterMayday = new GregorianCalendar(2021, Calendar.MAY, 3, 15, 1);
        assertTrue("Toll fee is less than the minimum", tollCalculator.getTollFee(new Car(), mondayAfterMayday.getTime()) >= 8);
        assertTrue("Toll fee is more than the maximum", tollCalculator.getTollFee(new Car(), mondayAfterMayday.getTime()) <= 18);
    }

    @Test
    public void testRushHourTollFee() {
        Calendar mondayAfterMayday = new GregorianCalendar(2021, Calendar.MAY, 3, 16, 1);
        assertEquals(18, tollCalculator.getTollFee(new Car(), mondayAfterMayday.getTime()));
    }

    @Test
    public void testMaximumFeePerDay() {
        Calendar h1 = new GregorianCalendar(2021, Calendar.MAY, 3, 6, 1); // 8
        Calendar h2 = new GregorianCalendar(2021, Calendar.MAY, 3, 7, 2); // 18 - should take this as max
        Calendar h3 = new GregorianCalendar(2021, Calendar.MAY, 3, 8, 3); // 13 - should take this as max of hour 7:30 to 8:01
        Calendar h4 = new GregorianCalendar(2021, Calendar.MAY, 3, 10, 4); // 13
        Calendar h5 = new GregorianCalendar(2021, Calendar.MAY, 3, 17, 5); // 13 -- adds up to 65
        assertEquals(60, tollCalculator.getTollFee(new Car(), h1.getTime(), h2.getTime(), h3.getTime(), h4.getTime(), h5.getTime()));
    }

    @Test
    public void testTollFeeWithinSameHour() {
        Calendar sameHour1 = new GregorianCalendar(2021, Calendar.MAY, 3, 6, 29); // 8
        Calendar sameHour2 = new GregorianCalendar(2021, Calendar.MAY, 3, 7, 1); // 18 - should take this as max
        Calendar secondHour1 = new GregorianCalendar(2021, Calendar.MAY, 3, 7, 30); // 18 - should take this as max of hour 7:30 to 8:01
        Calendar secondHour2 = new GregorianCalendar(2021, Calendar.MAY, 3, 8, 1); // 13
        assertEquals(36, tollCalculator.getTollFee(new Car(), sameHour1.getTime(), sameHour2.getTime(), secondHour1.getTime(), secondHour2.getTime()));
    }

    @Test
    public void testTollFreeVehicleType() {
        Calendar mondayAfterMayday = new GregorianCalendar(2021, Calendar.MAY, 3, 6, 1);
        assertEquals(0, tollCalculator.getTollFee(new Motorbike(), mondayAfterMayday.getTime()));
    }

    @Test
    public void testTollFreeDayHoliday() {
        Calendar mayDay = new GregorianCalendar(2021, Calendar.MAY, 1, 6, 1);
        assertEquals(0, tollCalculator.getTollFee(new Car(), mayDay.getTime()));
    }

    @Test
    public void testTollFreeDayWeekend() {
        Calendar mayDay = new GregorianCalendar(2022, Calendar.MARCH, 20, 6, 1);
        assertEquals(0, tollCalculator.getTollFee(new Car(), mayDay.getTime()));
    }

    @Test
    public void testIntermittentRange() {
        Calendar h1 = new GregorianCalendar(2021, Calendar.MAY, 3, 6, 1); // 8
        Calendar h2 = new GregorianCalendar(2021, Calendar.MAY, 3, 8, 2); // 13
        Calendar h3 = new GregorianCalendar(2021, Calendar.MAY, 3, 11, 16); // 8
        Calendar h4 = new GregorianCalendar(2021, Calendar.MAY, 3, 15, 4); // 13
        Calendar h5 = new GregorianCalendar(2021, Calendar.MAY, 3, 15, 32); // 18 -- take 18, as both are in same hour
        Calendar h6 = new GregorianCalendar(2021, Calendar.MAY, 3, 18, 29); // 8 - adds to 55
        assertEquals(55, tollCalculator.getTollFee(new Car(), h1.getTime(), h2.getTime(), h3.getTime(), h4.getTime(), h5.getTime(), h6.getTime()));
    }

}
