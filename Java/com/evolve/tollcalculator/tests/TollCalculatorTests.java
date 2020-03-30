package com.evolve.tollcalculator.tests;

import com.evolve.tollcalculator.toll.TollCalculator;
import com.evolve.tollcalculator.vehicle.Car;
import com.evolve.tollcalculator.vehicle.Motorbike;
import org.junit.jupiter.api.Test;

import java.time.LocalDateTime;
import java.util.List;

import static java.util.Arrays.asList;
import static org.junit.jupiter.api.Assertions.*;

public class TollCalculatorTests
{
    private TollCalculator tollCalculator = new TollCalculator();

    @Test
    public void testZeroFeeForTollFreeVehicle()
    {
        int fee = tollCalculator.getTollFee( LocalDateTime.of(2020, 9, 1, 13, 30), new Motorbike());

        assertEquals(0, fee);
    }

    @Test
    public void testZeroFeeForTollFreeDate_Weekend()
    {
        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 2, 8, 14, 30), new Car()));
        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 2, 9, 14, 30), new Car()));
    }

    @Test
    public void testZeroFeeForTollFreeDate_Holiday_Year2020()
    {
        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 1, 1, 14, 30), new Car()));

        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 3, 28, 14, 30), new Car()));
        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 3, 29, 14, 30), new Car()));

        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 4, 1, 14, 30), new Car()));
        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 4, 30, 14, 30), new Car()));

        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 5, 1, 14, 30), new Car()));
        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 5, 8, 14, 30), new Car()));
        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 5, 9, 14, 30), new Car()));

        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 6, 5, 14, 30), new Car()));
        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 6, 6, 14, 30), new Car()));
        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 6, 21, 14, 30), new Car()));

        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 7, 21, 14, 30), new Car()));
        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 7, 3, 14, 30), new Car()));

        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 11, 1, 14, 30), new Car()));

        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 12, 24, 14, 30), new Car()));
        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 12, 25, 14, 30), new Car()));
        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 12, 26, 14, 30), new Car()));
        assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 12, 31, 14, 30), new Car()));
    }

    @Test
    public void testZeroFeeForTollFreeTime()
    {
        int[] freeWholeHours = {19, 20, 21, 22, 23, 0, 1, 2, 3, 4, 5};

        for(int i = 30; i <= 59; i++) {
            assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 10, 1, 18, i), new Car()));
        }

        for(int hour : freeWholeHours) {
            for(int i = 0; i <= 59; i++) {
                assertEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 10, 1, hour, i), new Car()));
            }
        }
    }

    @Test
    public void testFeeHours()
    {
        int[] feeWholeHours = {6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17};

        for(int i = 0; i <= 29; i++) {
            assertNotEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 10, 1, 18, i), new Car()));
        }

        for(int hour : feeWholeHours) {
            for(int i = 0; i <= 59; i++) {
                assertNotEquals(0, tollCalculator.getTollFee(LocalDateTime.of(2020, 10, 1, hour, i), new Car()));
            }
        }
    }

    @Test
    public void testMaximumFee()
    {
        List<LocalDateTime> dates = asList(
            LocalDateTime.of(2020, 10, 1, 7, 23),
            LocalDateTime.of(2020, 10, 1, 9, 2),
            LocalDateTime.of(2020, 10, 1, 10, 30),
            LocalDateTime.of(2020, 10, 1, 11, 45),
            LocalDateTime.of(2020, 10, 1, 13, 12),
            LocalDateTime.of(2020, 10, 1, 14, 23),
            LocalDateTime.of(2020, 10, 1, 16, 23),
            LocalDateTime.of(2020, 10, 1, 17, 0)
        );

        assertEquals(60, tollCalculator.getTollFee(new Car(), dates));
    }

    @Test
    public void testHighestFeeWithinHour()
    {
        List<LocalDateTime> dates = asList(
                LocalDateTime.of(2020, 10, 1, 6, 12),
                LocalDateTime.of(2020, 10, 1, 6, 59),
                LocalDateTime.of(2020, 10, 1, 7, 12)
        );

        assertEquals(18, tollCalculator.getTollFee(new Car(), dates));
    }

    @Test
    public void testHighestFeeMultipleGroups()
    {
        List<LocalDateTime> dates = asList(
                LocalDateTime.of(2020, 10, 1, 6, 12),
                LocalDateTime.of(2020, 10, 1, 7, 12),
                LocalDateTime.of(2020, 10, 1, 6, 59),

                LocalDateTime.of(2020, 10, 1, 19, 00),
                LocalDateTime.of(2020, 10, 1, 19, 52),

                LocalDateTime.of(2020, 10, 1, 8, 15),
                LocalDateTime.of(2020, 10, 1, 8, 59),

                LocalDateTime.of(2020, 10, 1, 18, 12)
        );

        assertEquals(39, tollCalculator.getTollFee(new Car(), dates));
    }

    @Test
    public void testFee_8_Intervals()
    {
        for(int i = 0; i <= 29; i++) {
            assertEquals(8, tollCalculator.getTollFee(LocalDateTime.of(2020, 10, 1, 6, i), new Car()));
        }

        for(int i = 30; i <= 59; i++) {
            assertEquals(8, tollCalculator.getTollFee(LocalDateTime.of(2020, 10, 1, 8, i), new Car()));
        }

        for(int j = 9; j <= 14; j++) {
            for (int i = 0; i <= 59; i++) {
                assertEquals(8, tollCalculator.getTollFee(LocalDateTime.of(2020, 10, 1, j, i), new Car()));
            }
        }

        for(int i = 0; i <= 29; i++) {
            assertEquals(8, tollCalculator.getTollFee(LocalDateTime.of(2020, 10, 1, 18, i), new Car()));
        }
    }

    @Test
    public void testFee_13_Intervals()
    {
        for(int i = 30; i <= 59; i++) {
            assertEquals(13, tollCalculator.getTollFee(LocalDateTime.of(2020, 10, 1, 6, i), new Car()));
        }

        for(int i = 0; i <= 29; i++) {
            assertEquals(13, tollCalculator.getTollFee(LocalDateTime.of(2020, 10, 1, 8, i), new Car()));
        }

        for(int i = 0; i <= 29; i++) {
            assertEquals(13, tollCalculator.getTollFee(LocalDateTime.of(2020, 10, 1, 15, i), new Car()));
        }

        for(int i = 0; i <= 59; i++) {
            assertEquals(13, tollCalculator.getTollFee(LocalDateTime.of(2020, 10, 1, 17, i), new Car()));
        }
    }

    @Test
    public void testFee_18_Intervals()
    {
        for(int i = 0; i <= 59; i++) {
            assertEquals(18, tollCalculator.getTollFee(LocalDateTime.of(2020, 10, 1, 7, i), new Car()));
        }

        for(int i = 0; i <= 59; i++) {
            assertEquals(18, tollCalculator.getTollFee(LocalDateTime.of(2020, 10, 1, 16, i), new Car()));
        }
    }
}
