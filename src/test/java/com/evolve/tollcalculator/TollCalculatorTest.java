package com.evolve.tollcalculator;

import com.evolve.services.HolidayServiceImpl;
import com.evolve.services.TollFeeServiceImpl;
import com.evolve.vehicles.Motorbike;
import com.evolve.vehicles.Vehicle;
import org.junit.Test;
import org.junit.Assert;
import com.evolve.vehicles.Car;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.time.ZoneId;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class TollCalculatorTest {
    private final TollCalculator tollCalculator;

    public TollCalculatorTest() {
        tollCalculator = new TollCalculator(new HolidayServiceImpl(), new TollFeeServiceImpl());
    }

    @Test
    public void testTollFreeVehicle() {
        // toll-free vehicle
        Vehicle vehicle = new Motorbike();
        // non-weekend, not a holiday
        LocalDate date = LocalDate.of(2021, 2, 1);
        LocalDateTime time = LocalDateTime.of(date, LocalTime.of(8, 0));
        int fee = tollCalculator.getTollFee(vehicle, Date.from(time.atZone(ZoneId.systemDefault()).toInstant()));
        Assert.assertEquals(0, fee);
    }

    @Test
    public void testWeekend() {
        // normal vehicle
        Vehicle vehicle = new Car();
        // Saturday
        LocalDate date = LocalDate.of(2021, 2, 6);
        LocalDateTime time = LocalDateTime.of(date, LocalTime.of(8, 0));
        int fee = tollCalculator.getTollFee(vehicle, Date.from(time.atZone(ZoneId.systemDefault()).toInstant()));
        Assert.assertEquals(time.toString(), 0, fee);
        // Sunday
        date = date.plusDays(1);
        time = LocalDateTime.of(date, LocalTime.of(8, 0));
        fee = tollCalculator.getTollFee(vehicle, Date.from(time.atZone(ZoneId.systemDefault()).toInstant()));
        Assert.assertEquals(time.toString(), 0, fee);
    }

    @Test
    public void testTollFreePeriod() {
        Vehicle vehicle = new Car();
        List<Date> dates = new ArrayList<>();
        // non-weekend, not a holiday
        LocalDate date = LocalDate.of(2021, 2, 1);
        LocalDateTime time = LocalDateTime.of(date, LocalTime.of(5, 30)); // 05:30 fee: 0
        dates.add(Date.from(time.atZone(ZoneId.systemDefault()).toInstant()));
        time = time.plusMinutes(30);    // 06:00 fee: 8 (not counted)
        dates.add(Date.from(time.atZone(ZoneId.systemDefault()).toInstant()));
        time = time.plusMinutes(40);    // 06:40 fee: 13
        dates.add(Date.from(time.atZone(ZoneId.systemDefault()).toInstant()));
        Date[] datesArray = new Date[dates.size()];
        int fee = tollCalculator.getTollFee(vehicle, dates.toArray(datesArray));
        // The toll-free shall be dropped. Thus the total fee should be 13.
        Assert.assertEquals(13, fee);
    }

    @Test
    public void testTollFeeCalculation() {
        Vehicle vehicle = new Car();
        List<Date> dates = new ArrayList<>();
        // non-weekend, not a holiday
        LocalDate date = LocalDate.of(2021, 2, 1);
        LocalDateTime time = LocalDateTime.of(date, LocalTime.of(6, 00)); // 06:00 fee: 8 (not counted)
        dates.add(Date.from(time.atZone(ZoneId.systemDefault()).toInstant()));
        time = time.plusMinutes(40);    // 06:40 fee: 13
        dates.add(Date.from(time.atZone(ZoneId.systemDefault()).toInstant()));
        time = time.plusMinutes(40);    // 07:20 fee: 18
        dates.add(Date.from(time.atZone(ZoneId.systemDefault()).toInstant()));
        time = time.plusMinutes(40);    // 08:00 fee: 13 (not counted)
        dates.add(Date.from(time.atZone(ZoneId.systemDefault()).toInstant()));
        time = time.plusMinutes(40);    // 08:40 fee: 8
        dates.add(Date.from(time.atZone(ZoneId.systemDefault()).toInstant()));
        time = time.plusHours(6);    // 14:40 fee: 8 (not counted)
        dates.add(Date.from(time.atZone(ZoneId.systemDefault()).toInstant()));
        time = time.plusMinutes(40);    // 15:20 fee: 13
        dates.add(Date.from(time.atZone(ZoneId.systemDefault()).toInstant()));
        Date[] datesArray = new Date[dates.size()];
        int fee = tollCalculator.getTollFee(vehicle, dates.toArray(datesArray));
        // 13 + 18 + 8 + 13 = 52
        Assert.assertEquals("Till " + time.toString(), 52, fee);
        time = time.plusMinutes(40);    // 16:00 fee: 18
        dates.add(Date.from(time.atZone(ZoneId.systemDefault()).toInstant()));
        fee = tollCalculator.getTollFee(vehicle, dates.toArray(datesArray));
        Assert.assertEquals("Reaches maximum for one day.", TollCalculator.MAX_FEE_IN_ONE_DAY, fee);
    }
}
