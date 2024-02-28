package org.example;

import org.example.config.TimeslotFees;
import org.example.config.TollConfiguration;
import org.example.data.DefaultVehicle;
import org.example.exception.APIBadRequestException;
import org.example.exception.ParameterNotFoundException;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.context.TestPropertySource;


import java.time.LocalDateTime;
import java.time.MonthDay;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.stream.Collectors;

import static org.junit.jupiter.api.Assertions.*;

@TestPropertySource(locations = "classpath:application.properties")
public class TollCalculatorTest {


    @Autowired
    private final TollCalculator tollCalculator;

    public TollCalculatorTest() {

        TollConfiguration tollConfiguration = new TollConfiguration();
        tollConfiguration.setMaximumTollFeesPerDay(60);
        tollConfiguration.setHolidays(Arrays.asList(
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
        tollConfiguration.setTimeslotFees(Arrays.asList(
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
        tollConfiguration.setVehicleTypes(Arrays.stream("Car,Truck,Motorbike,Tractor,Emergency,Diplomat,Foreign,Military".split(",")).collect(Collectors.toList()));
        tollConfiguration.setTollFreeVehicles(Arrays.stream("Motorbike,Tractor,Emergency,Diplomat,Foreign,Military".split(",")).collect(Collectors.toList()));
        this.tollCalculator = new TollCalculator(tollConfiguration);

    }

    @Test
    public void testGetTollFeeVehicleNull() {
        //if vehicle is not passed, then it should throw parameter not found or missing exception.

        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 3, 29, 11, 11, 11);
        ParameterNotFoundException parameterNotFoundException = assertThrows(ParameterNotFoundException.class, () -> {
            this.tollCalculator.getTollFee(null, Collections.singletonList(localDateTime1));
        });
        assertEquals("vehicle parameter is null.", parameterNotFoundException.getMessage());

    }

    @Test
    public void testGetTollFeeDatesNull() {
        //if vehicle is present, but dates are not passed,  it should throw parameter not found or missing exception.

        ParameterNotFoundException parameterNotFoundException = assertThrows(ParameterNotFoundException.class, () -> {
            this.tollCalculator.getTollFee(new DefaultVehicle("Car"), null);
        });

        assertEquals("dates parameter is null.", parameterNotFoundException.getMessage());
    }

    @Test
    public void testGetTollFeeDatesInvalidVehicles() {
        //if vehicle is present, but dates are not passed,  it should throw parameter not found or missing exception.
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 3, 29, 11, 11, 11);
        APIBadRequestException apiBadRequestException = assertThrows(APIBadRequestException.class, () -> {
            this.tollCalculator.getTollFee(new DefaultVehicle("xyz"), Collections.singletonList(localDateTime1));
        });

        assertTrue(apiBadRequestException.getMessage().startsWith("Vehicle should be a valid. It should be of one of these vehicles:"));
    }

    @Test
    public void testGetTollFeeForHoliday() {
        //if vehicle is car, and dates are fall on holiday, it should return 0 fees.
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 3, 28, 7, 10, 44);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 3, 28, 9, 10, 44);
        List<LocalDateTime> dateTimeList = Arrays.asList(localDateTime1,
                localDateTime2);
        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        assertEquals(0, fees);
    }

    @Test
    public void testGetTollFeeForWeekend() {
        //if vehicle is car, and dates are fall on weekend, it should return 0 fees.

        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 15, 11, 11, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 15, 11, 12, 44);
        List<LocalDateTime> dateTimeList = Arrays.asList(localDateTime1,
                localDateTime2);
        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        assertEquals(0, fees);
    }

    @Test
    public void testGetTollFeeForUnsortedDates() {
        //if vehicle is car, and dates are not in ordered, so it should sort them and return calculated fees per expectation.

        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 11, 11, 10, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 11, 7, 10, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 11, 9, 10, 44);
        LocalDateTime localDateTime4 = LocalDateTime.of(2023, 4, 11, 13, 10, 44);
        LocalDateTime localDateTime5 = LocalDateTime.of(2023, 4, 11, 8, 10, 44);

        List<LocalDateTime> dateTimeList = Arrays.asList(localDateTime1,
                localDateTime2,
                localDateTime3,
                localDateTime4,
                localDateTime5);


        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        assertEquals(42, fees);
    }


    @Test
    public void testGetTollFeeForMaximumFees() {
        // if vehicle is car, and vehicle is passed more times from toll, so that it calculates fees more than maximum fees per day
        // in this case it should return maximum fees per day, i.e. 60.

        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 11, 11, 10, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 11, 7, 10, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 11, 9, 10, 44);
        LocalDateTime localDateTime4 = LocalDateTime.of(2023, 4, 11, 13, 10, 44);
        LocalDateTime localDateTime5 = LocalDateTime.of(2023, 4, 11, 8, 10, 44);
        LocalDateTime localDateTime6 = LocalDateTime.of(2023, 4, 11, 14, 10, 44);
        LocalDateTime localDateTime7 = LocalDateTime.of(2023, 4, 11, 16, 15, 44);
        LocalDateTime localDateTime8 = LocalDateTime.of(2023, 4, 11, 15, 10, 44);

        List<LocalDateTime> dateTimeList = Arrays.asList(localDateTime1,
                localDateTime2,
                localDateTime3,
                localDateTime4,
                localDateTime5,
                localDateTime6,
                localDateTime7,
                localDateTime8);

        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        assertEquals(60, fees);
    }

    @Test
    public void testGetTollFeeForTollFreeVehicles() {
        // if vehicle is Motorbike, and as it is toll-free vehicle, fees should be return as 0.

        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 11, 11, 10, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 11, 7, 10, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 11, 9, 10, 44);
        LocalDateTime localDateTime4 = LocalDateTime.of(2023, 4, 11, 13, 10, 44);
        LocalDateTime localDateTime5 = LocalDateTime.of(2023, 4, 11, 8, 10, 44);
        LocalDateTime localDateTime6 = LocalDateTime.of(2023, 4, 11, 14, 10, 44);
        LocalDateTime localDateTime7 = LocalDateTime.of(2023, 4, 11, 16, 15, 44);
        LocalDateTime localDateTime8 = LocalDateTime.of(2023, 4, 11, 15, 10, 44);

        List<LocalDateTime> dateTimeList = Arrays.asList(localDateTime1,
                localDateTime2,
                localDateTime3,
                localDateTime4,
                localDateTime5,
                localDateTime6,
                localDateTime7,
                localDateTime8);

        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Motorbike"), dateTimeList);
        assertEquals(0, fees);
    }

    @Test
    public void testGetTollFeeWithDifferentDates() {
        // if vehicle is Car, and dates are passed for multiple days, it should return total fees return for those days.
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 11, 9, 10, 44);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 11, 13, 10, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 12, 8, 10, 44);
        LocalDateTime localDateTime4 = LocalDateTime.of(2023, 4, 12, 14, 5, 44);


        List<LocalDateTime> dateTimeList = Arrays.asList(localDateTime1,
                localDateTime2,
                localDateTime3,
                localDateTime4);

        APIBadRequestException apiBadRequestException = assertThrows(APIBadRequestException.class, () -> {
            this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        });

        assertEquals("Input dates are not valid as it includes the different days.", apiBadRequestException.getMessage());
    }

    @Test
    public void testGetTollFeeForOffHours() {
        // if vehicle is Car, and it is passed at off hours then fees should be calculated 0.

        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 14, 5, 11, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 14, 19, 12, 44);
        List<LocalDateTime> dateTimeList = Arrays.asList(localDateTime1, localDateTime2);
        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        assertEquals(0, fees);
    }

    @Test
    public void testGetTollFeeForHourlyMaxTollTaxCase_DifferentSlot_InHour() {
        // if vehicle is Car, and it is passed multiple times in a hour then it should consider max fees.
        // A vehicle should only be charged once an hour
        // -->  In the case of multiple fees in the same hour period, the highest one applies.

        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 14, 6, 50, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 14, 7, 10, 44);
        List<LocalDateTime> dateTimeList = Arrays.asList(localDateTime1, localDateTime2);
        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        assertEquals(18, fees);
    }

    @Test
    public void testGetTollFeeForHourlyMaxTollTaxCase_ThreeDifferentSlot_InHour() {
        // if vehicle is Car, and it is passed multiple times in a hour then it should consider max fees.
        // A vehicle should only be charged once an hour
        // -->  In the case of multiple fees in the same hour period, the highest one applies.

        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 14, 7, 55, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 14, 8, 5, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 14, 8, 35, 44);
        List<LocalDateTime> dateTimeList = Arrays.asList(localDateTime1, localDateTime2,localDateTime3);
        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        assertEquals(18, fees);
    }


    @Test
    public void testGetTollFeeForHourlyMaxTollTaxCase_ThreeDifferentSlot() {
        // if vehicle is Car, and it is passed multiple times in a hour then it should consider max fees.
        // A vehicle should only be charged once an hour
        // -->  In the case of multiple fees in the same hour period, the highest one applies.

        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 14, 7, 55, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 14, 16, 45, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 14, 17, 15, 44);
        List<LocalDateTime> dateTimeList = Arrays.asList(localDateTime1, localDateTime2,localDateTime3);
        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        assertEquals(36, fees);
    }

    @Test
    public void testGetTollFeeForHourlyMaxTollTaxCase_ThreeDifferentSlot_2() {
        // if vehicle is Car, and it is passed multiple times in a hour then it should consider max fees.
        // A vehicle should only be charged once an hour
        // -->  In the case of multiple fees in the same hour period, the highest one applies.

        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 14, 7, 55, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 14, 8, 5, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 14, 17, 15, 44);
        List<LocalDateTime> dateTimeList = Arrays.asList(localDateTime1, localDateTime2,localDateTime3);
        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        assertEquals(31, fees);
    }

    @Test
    public void testGetTollFeeForHourlyMaxTollTaxCase_InHour() {
        // if vehicle is Car, and it is passed multiple times in a hour then it should consider max fees.
        // A vehicle should only be charged once an hour
        // -->  In the case of multiple fees in the same hour period, the highest one applies.

        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 14, 7, 55, 11);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 14, 8, 5, 44);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 14, 8, 15, 44);
        List<LocalDateTime> dateTimeList = Arrays.asList(localDateTime1, localDateTime2,localDateTime3);
        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        assertEquals(18, fees);
    }

    @Test
    public void testGetTollFeeFirstTimeWithZeroFees() {
        //This is special case we need to handle particular set of times
        LocalDateTime localDateTime1 = LocalDateTime.of(2023, 4, 11, 5, 45, 16);
        LocalDateTime localDateTime2 = LocalDateTime.of(2023, 4, 11, 6, 47, 16);
        LocalDateTime localDateTime3 = LocalDateTime.of(2023, 4, 11, 6, 48, 16);
        LocalDateTime localDateTime4 = LocalDateTime.of(2023, 4, 11, 6, 49, 16);

        LocalDateTime localDateTime5 = LocalDateTime.of(2023, 4, 11, 7, 55, 16);
        LocalDateTime localDateTime6 = LocalDateTime.of(2023, 4, 11, 8, 10, 16);
        LocalDateTime localDateTime7 = LocalDateTime.of(2023, 4, 11, 10, 15, 16);
        List<LocalDateTime> dateTimeList = Arrays.asList(localDateTime1,
                localDateTime2,
                localDateTime3,
                localDateTime4,
                localDateTime5,
                localDateTime6,
                localDateTime7);
        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        assertEquals(39, fees);
    }
    @Test
    public void testGetTollFeeForAmount8() {
        // if vehicle is Car, and it is passed at time when toll fee is 8.

        LocalDateTime localDateTime = LocalDateTime.of(2023, 4, 14, 6, 10, 44);
        List<LocalDateTime> dateTimeList = Collections.singletonList(localDateTime);
        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        assertEquals(8, fees);
    }


    @Test
    public void testGetTollFeeForAmount13() {
        // if vehicle is Car, and it is passed at time when toll fee is 13.

        LocalDateTime localDateTime = LocalDateTime.of(2023, 4, 14, 6, 40, 44);
        List<LocalDateTime> dateTimeList = Collections.singletonList(localDateTime);
        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        assertEquals(13, fees);
    }

    @Test
    public void testGetTollFeeForAmount18() {
        // if vehicle is Car, and it is passed at time when toll fee is 18.

        LocalDateTime localDateTime = LocalDateTime.of(2023, 4, 14, 7, 10, 44);
        List<LocalDateTime> dateTimeList = Collections.singletonList(localDateTime);
        int fees = this.tollCalculator.getTollFee(new DefaultVehicle("Car"), dateTimeList);
        assertEquals(18, fees);
    }
}
