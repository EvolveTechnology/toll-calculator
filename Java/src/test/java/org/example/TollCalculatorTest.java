package org.example;

import org.example.config.TimeslotFees;
import org.example.data.Car;
import org.example.data.Motorbike;
import org.example.config.TollFeeConfiguration;
import org.example.exception.ParameterNotFoundException;
import org.junit.jupiter.api.Test;
import org.junit.runner.RunWith;
import org.springframework.test.context.junit4.SpringRunner;

import java.time.LocalDateTime;
import java.time.MonthDay;
import java.util.Arrays;

import static org.junit.jupiter.api.Assertions.*;


@RunWith(SpringRunner.class)

public class TollCalculatorTest {

    private final TollCalculator tollCalculator;

    public TollCalculatorTest() {

        TollFeeConfiguration tollFeeConfiguration = new TollFeeConfiguration();
        tollFeeConfiguration.setMaximumTollFeesPerDay(60);
        tollFeeConfiguration.setHolidays(Arrays.asList(
                MonthDay.of(1, 1),
                MonthDay.of(3, 28),
                MonthDay.of(3, 29),
                MonthDay.of(4, 1),
                MonthDay.of(4, 30),
                MonthDay.of(5, 1),
                MonthDay.of(5, 8),
                MonthDay.of(5, 9),
                MonthDay.of(6, 5),
                MonthDay.of(6, 9),
                MonthDay.of(6, 11),
                MonthDay.of(7, 9),
                MonthDay.of(11, 1),
                MonthDay.of(12, 24),
                MonthDay.of(12, 25),
                MonthDay.of(12, 26),
                MonthDay.of(12, 31)
        ));
        tollFeeConfiguration.setTimeslotFees(Arrays.asList(
                new TimeslotFees("06:00", "06:29", 8),
                new TimeslotFees("06:30", "06:59", 13),
                new TimeslotFees("07:00", "07:59", 18),
                new TimeslotFees("08:00", "08:29", 13),
                new TimeslotFees("08:30", "14:59", 8),
                new TimeslotFees("08:30", "14:59", 8),
                new TimeslotFees("15:00", "15:29", 13),
                new TimeslotFees("15:30", "16:59", 18),
                new TimeslotFees("17:00", "17:59", 13),
                new TimeslotFees("18:00", "18:30", 8)
        ));

        this.tollCalculator = new TollCalculator(tollFeeConfiguration);

    }

    @Test
    public void testGetTollFeeVehicleNull() {
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 3, 29, 11, 11, 11);
        ParameterNotFoundException parameterNotFoundException = assertThrows(ParameterNotFoundException.class, () -> {
            this.tollCalculator.getTollFee(null, DateUtil.toDate(localDateTime1));
        });
        assertEquals("vehicle parameter is null.", parameterNotFoundException.getMessage());

    }

    @Test
    public void testGetTollFeeDatesNull() {
        ParameterNotFoundException parameterNotFoundException = assertThrows(ParameterNotFoundException.class, () -> {
            this.tollCalculator.getTollFee(new Car(), null);
        });

        assertEquals("dates parameter is null.", parameterNotFoundException.getMessage());
    }

    @Test
    public void testGetTollFeeForHoliday() {
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 3, 29, 11, 11, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 3, 28, 11, 12, 44);
        int fees = this.tollCalculator.getTollFee(new Car(), DateUtil.toDate(localDateTime1), DateUtil.toDate(localDateTime2));
        assertEquals(0, fees);
    }

    @Test
    public void testGetTollFeeForWeekend() {
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 15, 11, 11, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 15, 11, 12, 44);
        int fees = this.tollCalculator.getTollFee(new Car(), DateUtil.toDate(localDateTime1), DateUtil.toDate(localDateTime2));
        assertEquals(0, fees);
    }

    @Test
    public void testGetTollFeeForUnsortedDates() {
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 11, 11, 10, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 11, 7, 10, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 11, 9, 10, 44);
        LocalDateTime localDateTime4 = LocalDateTime.of(2023, 4, 11, 13, 10, 44);
        LocalDateTime localDateTime5 = LocalDateTime.of(2023, 4, 11, 8, 10, 44);

        int fees = this.tollCalculator.getTollFee(new Car(),
                DateUtil.toDate(localDateTime1),
                DateUtil.toDate(localDateTime2),
                DateUtil.toDate(localDateTime3),
                DateUtil.toDate(localDateTime4),
                DateUtil.toDate(localDateTime5)
        );
        assertEquals(42, fees);
    }


    @Test
    public void testGetTollFeeForMaximumFees() {
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 11, 11, 10, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 11, 7, 10, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 11, 9, 10, 44);
        LocalDateTime localDateTime4 = LocalDateTime.of(2023, 4, 11, 13, 10, 44);
        LocalDateTime localDateTime5 = LocalDateTime.of(2023, 4, 11, 8, 10, 44);
        LocalDateTime localDateTime6 = LocalDateTime.of(2023, 4, 11, 14, 10, 44);
        LocalDateTime localDateTime7 = LocalDateTime.of(2023, 4, 11, 16, 15, 44);
        LocalDateTime localDateTime8 = LocalDateTime.of(2023, 4, 11, 15, 10, 44);

        int fees = this.tollCalculator.getTollFee(new Car(),
                DateUtil.toDate(localDateTime1),
                DateUtil.toDate(localDateTime2),
                DateUtil.toDate(localDateTime3),
                DateUtil.toDate(localDateTime4),
                DateUtil.toDate(localDateTime5),
                DateUtil.toDate(localDateTime6),
                DateUtil.toDate(localDateTime7),
                DateUtil.toDate(localDateTime8)
        );
        assertEquals(60, fees);
    }

    @Test
    public void testGetTollFeeForTollFreeVehicles() {
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 11, 11, 10, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 11, 7, 10, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 11, 9, 10, 44);
        LocalDateTime localDateTime4 = LocalDateTime.of(2023, 4, 11, 13, 10, 44);
        LocalDateTime localDateTime5 = LocalDateTime.of(2023, 4, 11, 8, 10, 44);
        LocalDateTime localDateTime6 = LocalDateTime.of(2023, 4, 11, 14, 10, 44);
        LocalDateTime localDateTime7 = LocalDateTime.of(2023, 4, 11, 16, 15, 44);
        LocalDateTime localDateTime8 = LocalDateTime.of(2023, 4, 11, 15, 10, 44);

        int fees = this.tollCalculator.getTollFee(new Motorbike(),
                DateUtil.toDate(localDateTime1),
                DateUtil.toDate(localDateTime2),
                DateUtil.toDate(localDateTime3),
                DateUtil.toDate(localDateTime4),
                DateUtil.toDate(localDateTime5),
                DateUtil.toDate(localDateTime6),
                DateUtil.toDate(localDateTime7),
                DateUtil.toDate(localDateTime8)
        );
        assertEquals(0, fees);
    }

    @Test
    public void testGetTollFeeWithDifferentDates() {
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 11, 11, 10, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 11, 7, 10, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 11, 9, 10, 44);
        LocalDateTime localDateTime4 = LocalDateTime.of(2023, 4, 11, 13, 10, 44);

        LocalDateTime localDateTime5 = LocalDateTime.of(2023, 4, 12, 8, 10, 44);
        LocalDateTime localDateTime6 = LocalDateTime.of(2023, 4, 12, 14, 05, 44);
        LocalDateTime localDateTime7 = LocalDateTime.of(2023, 4, 12, 16, 15, 44);
        LocalDateTime localDateTime8 = LocalDateTime.of(2023, 4, 12, 15, 10, 44);

        int fees = this.tollCalculator.getTollFee(new Car(),
                DateUtil.toDate(localDateTime1),
                DateUtil.toDate(localDateTime2),
                DateUtil.toDate(localDateTime3),
                DateUtil.toDate(localDateTime4),
                DateUtil.toDate(localDateTime5),
                DateUtil.toDate(localDateTime6),
                DateUtil.toDate(localDateTime7),
                DateUtil.toDate(localDateTime8)
        );
        assertEquals(94, fees);
    }

    @Test
    public void testGetTollFeeForOffHours() {
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 14, 5, 11, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 14, 19, 12, 44);
        int fees = this.tollCalculator.getTollFee(new Car(), DateUtil.toDate(localDateTime1), DateUtil.toDate(localDateTime2));
        assertEquals(0, fees);
    }

    @Test
    public void testGetTollFeeForHourlyMaxTollTaxCase1() {
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 14, 6, 50, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 14, 7, 10, 44);
        int fees = this.tollCalculator.getTollFee(new Car(), DateUtil.toDate(localDateTime1), DateUtil.toDate(localDateTime2));
        assertEquals(18, fees);
    }

    @Test
    public void testGetTollFeeForHourlyMaxTollTaxCase2() {
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 14, 7, 55, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 14, 8, 5, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 14, 8, 35, 44);
        int fees = this.tollCalculator.getTollFee(new Car(), DateUtil.toDate(localDateTime1), DateUtil.toDate(localDateTime2), DateUtil.toDate(localDateTime3));
        assertEquals(18, fees);
    }


    @Test
    public void testGetTollFeeForHourlyMaxTollTaxCase3() {
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 14, 7, 55, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 14, 16, 45, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 14, 17, 15, 44);
        int fees = this.tollCalculator.getTollFee(new Car(), DateUtil.toDate(localDateTime1), DateUtil.toDate(localDateTime2), DateUtil.toDate(localDateTime3));
        assertEquals(36, fees);
    }

    @Test
    public void testGetTollFeeForHourlyMaxTollTaxCase4() {
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 14, 7, 55, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 14, 8, 5, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 14, 17, 15, 44);
        int fees = this.tollCalculator.getTollFee(new Car(), DateUtil.toDate(localDateTime1), DateUtil.toDate(localDateTime2), DateUtil.toDate(localDateTime3));
        assertEquals(31, fees);
    }


    @Test
    public void testGetTollFeeForAmount8() {
        LocalDateTime localDateTime = LocalDateTime.of(2023, 4, 14, 6, 10, 44);
        int fees = this.tollCalculator.getTollFee(new Car(), DateUtil.toDate(localDateTime));
        assertEquals(8, fees);
    }


    @Test
    public void testGetTollFeeForAmount13() {
        LocalDateTime localDateTime = LocalDateTime.of(2023, 4, 14, 6, 40, 44);
        int fees = this.tollCalculator.getTollFee(new Car(), DateUtil.toDate(localDateTime));
        assertEquals(13, fees);
    }

    @Test
    public void testGetTollFeeForAmount18() {
        LocalDateTime localDateTime = LocalDateTime.of(2023, 4, 14, 7, 10, 44);
        int fees = this.tollCalculator.getTollFee(new Car(), DateUtil.toDate(localDateTime));
        assertEquals(18, fees);
    }
}
