package com.solution;

import com.solution.pojo.*;
import com.solution.service.impl.RoadTollServiceImpl;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.List;

import static com.solution.util.Constants.*;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
/**
 * @description: Used to test if an exception is thrown cause the parameter is null or not on the same day.
 * @author: Richard(Duo.Wang)
 * @createDate: 2021/12/17 - 11:23
 * @version: v1.0
 */
public class TollTestForNullParamExecption {

    private final TollCalculator tollCalculator;
    private static LocalDate date;
    private static Vehicle car;

    public TollTestForNullParamExecption() {
        this.tollCalculator = new TollCalculator(new RoadTollServiceImpl());
    }

    @BeforeAll
    private static void initDate() {
        date = LocalDate.of(2021, 12, 17);//LocalDate.now();
        car = new Car();
    }

    @DisplayName("NULL PARAM VEHICLE TEST")
    @Test
    public void nullParamVehicleTest() {
        RuntimeException re = assertThrows(RuntimeException.class,
                () -> tollCalculator.getTollFee(null,
                        List.of(LocalDateTime.of(date, LocalTime.of(6, 0)))
                        .toArray(new LocalDateTime[1])));
        assertEquals(NULL_PARAM_VEHICLE_MSG, re.getMessage());
    }

    @DisplayName("NULL PARAM DATES TEST")
    @Test
    public void nullParamDatesTest() {
        RuntimeException re = assertThrows(RuntimeException.class,
                () -> tollCalculator.getTollFee(car, (LocalDateTime[])null));
        assertEquals(NULL_PARAM_DATES_MSG, re.getMessage());
    }

    @DisplayName("DIFFERENT DATES TEST")
    @Test
    public void differentDatesTest() {
        RuntimeException re = assertThrows(RuntimeException.class,
                () -> tollCalculator.getTollFee(car,
                        List.of(LocalDateTime.of(2021, 12, 17, 6, 0),
                                LocalDateTime.of(2021, 12, 18, 6, 0))
                                .toArray(new LocalDateTime[2])));
        assertEquals(MORE_THAN_ONE_DAY_MSG, re.getMessage());
    }

}
