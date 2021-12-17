package com.solution;

import com.solution.pojo.*;
import com.solution.service.impl.RoadTollServiceImpl;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import static org.junit.jupiter.api.Assertions.assertEquals;
/**
 * @description: Used to test toll-free conditions for weekends, holidays and special vehicles.
 * @author: Richard(Duo.Wang)
 * @createDate: 2021/12/17 - 8:46
 * @version: v1.0
 */
public class TollTestForFreeDayFreeVehicle {

    private final TollCalculator tollCalculator;
    private static LocalDate date;
    private static Vehicle car;
    private static int year;

    public TollTestForFreeDayFreeVehicle() {
        this.tollCalculator = new TollCalculator(new RoadTollServiceImpl());
    }

    @BeforeAll
    private static void initDate() {
        year = LocalDate.now().getYear();
        date = LocalDate.of(2021, 12, 17);//LocalDate.now();
        car = new Car();
    }


    @DisplayName("FREE WEEKEND TOLL TEST")
    @Test
    public void freeWeekendTollTest(){
        LocalDateTime time = LocalDateTime.of(2021, 12, 18, 15, 35);// SATURDAY
        assertEquals(0,  tollCalculator.getTollFee(car, time));
        assertEquals(0,  tollCalculator.getTollFee(car, time.plusDays(1)));// SUNDAY
    }

    @DisplayName("FREE DATE TOLL TEST")
    @Test
    public void freeDateTollTest() {
        assertEquals(0,  tollCalculator.getTollFee(car, LocalDateTime.of(year, 12, 24, 7, 0)));
        assertEquals(0,  tollCalculator.getTollFee(car, LocalDateTime.of(year, 12, 25, 7, 0)));
    }

    @DisplayName("FREE VEHICLE TOLL TEST")
    @Test
    public void freeVehicleTollTest() {
        Vehicle motorbike = new Motorbike();
        Vehicle tractor = new Tractor();
        Vehicle emergency = new Emergency();
        Vehicle foreign = new Foreign();
        Vehicle military = new Military();
        Vehicle diplomat = new Diplomat();

        LocalDateTime time = LocalDateTime.of(date, LocalTime.of(16, 40));
        assertEquals(0,  tollCalculator.getTollFee(motorbike, time));
        assertEquals(0,  tollCalculator.getTollFee(tractor, time));
        assertEquals(0,  tollCalculator.getTollFee(emergency, time));
        assertEquals(0,  tollCalculator.getTollFee(foreign, time));
        assertEquals(0,  tollCalculator.getTollFee(military, time));
        assertEquals(0,  tollCalculator.getTollFee(diplomat, time));
    }



}
