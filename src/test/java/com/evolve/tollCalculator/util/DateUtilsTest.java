package com.evolve.tollCalculator.util;


import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

class DateUtilsTest {

    private SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm");
    private static final Logger logger = LogManager.getLogger(DateUtilsTest.class);

    @ParameterizedTest
    @CsvSource({
            //for holiday
            "2013-01-01 17:37,18:00-18:30,false",
            "2013-12-01 17:37,17:30-17:59,true"
    })
    void isTimeRange(String dateString, String range,boolean isRanged) {
        try {
            assertEquals(DateUtils.isTimeRange(range, formatter.parse(dateString)),isRanged);
        } catch (ParseException e) {
            logger.error("Invalid date format");

        }
    }

    @ParameterizedTest
    @CsvSource({
            //for holiday
            "2013-01-01 17:37,true",
            "2013-01-12 17:37,true",
            "2013-12-12 17:37,false"
    })
    void isWeekendOrHoliday(String dateString, boolean isWeedendOrHoliday) {
        try {
            DateUtils.initHolidayList(2013);
            assertEquals(DateUtils.isWeekendOrHoliday(formatter.parse(dateString)),isWeedendOrHoliday);
        } catch (ParseException e) {
            logger.error("Invalid date format");

        }
    }

    @Test
    void initHolidayList() {
        List<String> holidayList2013 = List.of(
                "0-1", "0-6", "2-29", "2-31", "3-1", "4-1",
                "4-9", "4-19", "5-6", "5-21", "5-22",
                "10-2", "11-24", "11-25", "11-26", "11-31"
        );
        DateUtils.initHolidayList(2013);
        assertEquals(DateUtils.s_holiday_list, holidayList2013);

        List<String> holidayList2020 = List.of(
                "0-1", "0-6", "3-10", "3-12", "3-13",
                "4-1", "4-21", "4-31", "5-6", "5-19",
                "5-20", "10-7", "11-24", "11-25", "11-26", "11-31"
        );
        DateUtils.initHolidayList(2020);
        assertEquals(DateUtils.s_holiday_list, holidayList2020);
    }
}