package com.work;

import com.work.exceptions.MissingHolidayDataException;
import com.work.model.vehicles.*;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.text.SimpleDateFormat;
import java.time.Instant;
import java.util.Date;

import static org.junit.jupiter.api.Assertions.assertThrows;

public class TollCalculatorTest {

    TollCalculator tc;
    Car car ;

    public TollCalculatorTest() {
       tc = new TollCalculator();
        car = new Car();

    }

    @Test
    public void testWithNoDates() throws Exception {
        Assertions.assertEquals(0,tc.getTollFee(car));
    }

    @Test
    public void testTollFreeVehicles() throws Exception {
        Assertions.assertEquals(0,tc.getTollFee(new Motorbike(), Date.from(Instant.now())));
        Assertions.assertEquals(0,tc.getTollFee(new Emergency(), Date.from(Instant.now())));
        Assertions.assertEquals(0,tc.getTollFee(new Diplomat(), Date.from(Instant.now())));
        Assertions.assertEquals(0,tc.getTollFee(new Tractor(), Date.from(Instant.now())));
        Assertions.assertEquals(0,tc.getTollFee(new Military(), Date.from(Instant.now())));
        Assertions.assertEquals(0,tc.getTollFee(new Foreign(), Date.from(Instant.now())));

    }

    @Test
    public void testWeekends() throws Exception {
        Assertions.assertEquals(0,tc.getTollFee(car, Date.from(Instant.now())));
    }

    @Test
    public void testHolidays() throws Exception {
        Assertions.assertEquals(0,tc.getTollFee(car, new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").parse("2021-01-01 10:00:00")));
    }

    @Test
    public void testSingleDate() throws Exception {
        Assertions.assertEquals(8,tc.getTollFee(car, new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 10:00:00")));
    }

    @Test
    public void testMultipleTimesInHour() throws Exception {
        Assertions.assertEquals(18,tc.getTollFee(car,
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 15:00:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 15:10:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 15:30:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 15:45:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 16:00:00")));
    }

    @Test
    public void testMultipleTimesInConsecutiveHours() throws Exception {
        Assertions.assertEquals(36,tc.getTollFee(car,
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 15:00:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 15:15:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 15:30:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 15:45:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 16:00:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 16:15:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 16:30:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 16:45:00")));
    }

    @Test
    public void testFreePaidHourCombination() throws Exception {
        Assertions.assertEquals(18,tc.getTollFee(car,
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 15:00:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 15:10:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 15:30:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 15:45:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 06:45:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 18:45:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 16:00:00")));
    }

    @Test
    public void testMaximumPricePerDay() throws Exception {
        Assertions.assertEquals(60,tc.getTollFee(car,
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 09:00:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 11:00:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 11:30:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 13:30:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 15:30:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 16:30:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 16:00:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 10:05:00"),
                new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").parse("2021/11/18 17:30:00")));
    }

    @Test
    public void testMissingHolidayList() {
        assertThrows(MissingHolidayDataException.class, () ->
                tc.getTollFee(car, new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").parse("2023-05-08 10:00:00")));
    }
}
