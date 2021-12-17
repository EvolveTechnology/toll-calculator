package com.solution;

import com.solution.pojo.*;
import com.solution.service.impl.RoadTollServiceImpl;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.ArrayList;
import java.util.List;
import static com.solution.util.Constants.*;
import static org.junit.jupiter.api.Assertions.assertEquals;
/**
 * @description: Used to test the charging situation of normal vehicles in each time of day.
 * @author: Richard(Duo.Wang)
 * @createDate: 2021/12/17 - 13:06
 * @version: v1.0
 */
public class TollTestForCalcFee {

    private final TollCalculator tollCalculator;
    private static LocalDate date;
    private static Vehicle car;
    public TollTestForCalcFee() {
        this.tollCalculator = new TollCalculator(new RoadTollServiceImpl());
    }

    @BeforeAll
    private static void initDate() {
        date = LocalDate.of(2021, 12, 17);//LocalDate.now();
        car = new Car();
    }

    @DisplayName("MIDNIGHT AND EARLY MORNING ZERO FEE TIME TEST")
    @Test
    public void midnightMorningZeroFeeTimeTest() {
        List<LocalDateTime> dates = new ArrayList<>();
        // early morning time
        dates.add(LocalDateTime.of(date, LocalTime.of(0, 0))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(0, 59))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(1, 0))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(1, 59))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(2, 0))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(2, 59))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(3, 0))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(3, 59))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(4, 0))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(4, 59))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(5, 0))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(5, 59))); // 0
        //midnight time
        dates.add(LocalDateTime.of(date, LocalTime.of(18, 30))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(19, 10))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(20, 15))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(21, 20))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(22, 30))); // 0
        dates.add(LocalDateTime.of(date, LocalTime.of(23, 40))); // 0

        assertEquals(0, tollCalculator.getTollFee(car, dates.toArray(new LocalDateTime[dates.size()])));
    }

    @DisplayName("MORNING RUSH HOUR TOLL CALCULATION TEST")
    @Test
    public void morningRushHourTollTest() {
        List<LocalDateTime> dates = new ArrayList<>();
        dates.add(LocalDateTime.of(date, LocalTime.of(6, 0))); // 8
        dates.add(LocalDateTime.of(date, LocalTime.of(6, 29)));// 8 skip within 60 mins
        dates.add(LocalDateTime.of(date, LocalTime.of(6, 50)));//13
        dates.add(LocalDateTime.of(date, LocalTime.of(7, 0)));// 18
        dates.add(LocalDateTime.of(date, LocalTime.of(7, 59)));// 18 skip within 60 mins --> total 31
        dates.add(LocalDateTime.of(date, LocalTime.of(8, 0)));// 13
        dates.add(LocalDateTime.of(date, LocalTime.of(8, 29)));//13 skip within 60 mins
        dates.add(LocalDateTime.of(date, LocalTime.of(8, 30)));//8 -->total 44
        dates.add(LocalDateTime.of(date, LocalTime.of(9, 0)));//0 -->total 44
        assertEquals(44, tollCalculator.getTollFee(car, dates.toArray(new LocalDateTime[dates.size()])));
    }

    @DisplayName("EVENING RUSH HOUR TOLL CALCULATION TEST")
    @Test
    public void eveningRushHourTollTest() {
        List<LocalDateTime> dates = new ArrayList<>();

        dates.add(LocalDateTime.of(date, LocalTime.of(15, 0)));// 13
        dates.add(LocalDateTime.of(date, LocalTime.of(15, 30)));// 18 --> same hour max value
        // total 18
        dates.add(LocalDateTime.of(date, LocalTime.of(16, 0)));// 18 --> next hour
        dates.add(LocalDateTime.of(date, LocalTime.of(16, 30)));// 18 --> same hour same value
        // total 36
        dates.add(LocalDateTime.of(date, LocalTime.of(17, 0)));// 13 -- next hour
        dates.add(LocalDateTime.of(date, LocalTime.of(17, 30)));// 13 -- same hour same value
        // total 49
        dates.add(LocalDateTime.of(date, LocalTime.of(18, 0)));// 8 --> next hour
        dates.add(LocalDateTime.of(date, LocalTime.of(18, 30)));// 0
        assertEquals(57, tollCalculator.getTollFee(car, dates.toArray(new LocalDateTime[dates.size()])));
    }

    @DisplayName("NORMAL DAY TIME HALF AN HOUR BEFORE CALCULATION TEST")
    @Test
    public void normalDayHourBeforeTollTest() {
        List<LocalDateTime> dates = new ArrayList<>();
        dates.add(LocalDateTime.of(date, LocalTime.of(9, 15)));//0
        dates.add(LocalDateTime.of(date, LocalTime.of(10, 5)));//0
        dates.add(LocalDateTime.of(date, LocalTime.of(11, 25)));//0
        dates.add(LocalDateTime.of(date, LocalTime.of(12, 5)));//0
        dates.add(LocalDateTime.of(date, LocalTime.of(14, 15)));//0
        assertEquals(0, tollCalculator.getTollFee(car, dates.toArray(new LocalDateTime[dates.size()])));
    }

    @DisplayName("NORMAL DAY TIME HALF AN HOUR AFTER CALCULATION TEST")
    @Test
    public void normalDayHourAfterTollTest() {
        List<LocalDateTime> dates = new ArrayList<>();
        dates.add(LocalDateTime.of(date, LocalTime.of(9, 35)));//8
        dates.add(LocalDateTime.of(date, LocalTime.of(10, 45)));//8  total-->16
        dates.add(LocalDateTime.of(date, LocalTime.of(11, 55)));//8  total-->24
        dates.add(LocalDateTime.of(date, LocalTime.of(12, 45)));//0  skip within 60 mins --> total 24
        dates.add(LocalDateTime.of(date, LocalTime.of(14, 35)));//8  total-->32
        assertEquals(32, tollCalculator.getTollFee(car, dates.toArray(new LocalDateTime[dates.size()])));
    }

    @DisplayName("MAX FEE CALCULATION TEST")
    @Test
    public void MaxFeeCalcTest() {
        List<LocalDateTime> dates = new ArrayList<>();
        dates.add(LocalDateTime.of(date, LocalTime.of(6, 0)));//8
        dates.add(LocalDateTime.of(date, LocalTime.of(11, 35)));//8
        dates.add(LocalDateTime.of(date, LocalTime.of(12, 35)));//8
        dates.add(LocalDateTime.of(date, LocalTime.of(14, 35)));//8
        dates.add(LocalDateTime.of(date, LocalTime.of(15, 5)));// 13
        dates.add(LocalDateTime.of(date, LocalTime.of(15, 30)));// 18 --> same hour max value
        // total 50
        dates.add(LocalDateTime.of(date, LocalTime.of(16, 0)));// 18 --> next hour
        dates.add(LocalDateTime.of(date, LocalTime.of(16, 30)));// 18 --> same hour same value
        // total 68
        assertEquals(MAX_FEE_FOR_ONE_DAY, tollCalculator.getTollFee(car, dates.toArray(new LocalDateTime[dates.size()])));
    }

}
