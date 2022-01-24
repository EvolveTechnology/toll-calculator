package com.evolve.tollCalculator.util;


import com.evolve.tollCalculator.TollCalculator;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.io.IOException;
import java.io.InputStream;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;
import java.util.concurrent.TimeUnit;

import static com.evolve.tollCalculator.util.Constants.DATE_DELIMITER;


public class DateUtils {

    private static final Logger logger = LogManager.getLogger(DateUtils.class);
    public static Map<String, String> s_time_fee_Map = initTimeFeeMappingMap();
    public static List<String> s_holiday_list = new ArrayList<String>();

    private static Calendar getEasterSunday(int year) {
        int day = 0;
        int month = 0;

        int g = year % 19;
        int c = year / 100;
        int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
        int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

        day   = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
        month = 3;

        if (day > 31) {
            month++;
            day -= 31;
        }

        Calendar calendar = Calendar.getInstance();
        calendar.set(year, (int) month - 1, (int) day, 0, 0);

        return calendar;
    }

    private static Calendar getAscensionDay(int year) {
        return getDateBaseEasterSunday(year, 39);
    }

    private static Map<String, String> initTimeFeeMappingMap() {
        Properties getProperties = new Properties();
        InputStream inputStream = TollCalculator.class.getResourceAsStream("/timeFeeMapping.properties");
        try {
            getProperties.load(inputStream);
        } catch (IOException e) {
            logger.error("get time fee mapping exception ");
            return null;
        }
        return (Map) getProperties;
    }

    private static Calendar getDateBaseEasterSunday(int year, int dates) {
        Calendar calendar = getEasterSunday(year);
        calendar.add(Calendar.DATE, dates);
        return calendar;
    }

    private static Calendar getWhitSunday(int year) {
        return getDateBaseEasterSunday(year, 49);
    }

    private static Calendar getEasterMonday(int year) {
        return getDateBaseEasterSunday(year, 1);
    }

    private static Calendar getGoodFriday(int year) {
        return getDateBaseEasterSunday(year, -2);
    }

    private static Calendar getMidsummerDay(int year) {
        return getFirstSaturday(year, 6, 20);
    }

    private static Calendar getMidsummerEveDay(int year) {
        Calendar calendar = getMidsummerDay(year);
        calendar.add(Calendar.DATE, -1);
        return calendar;
    }

    private static Calendar getAllSaintsDay(int year) {
        return getFirstSaturday(year, 11, 1);
    }

    public static Calendar getCalendarByDate(Date date) {
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.setTime(date);
        return calendar;
    }

    private static String formatHoliday(Date date) {
        Calendar calendar = getCalendarByDate(date);
        int month = getDateAttribute(calendar, Calendar.MONTH);
        int day = getDateAttribute(calendar, Calendar.DAY_OF_MONTH);
        return month + DATE_DELIMITER + day;
    }

    public static int getDateAttribute(Calendar calendar, int type) {
        return calendar.get(type);
    }


    public static boolean isInOneHourPeriod(Date intervalStart, Date date) {
        TimeUnit timeUnit = TimeUnit.MINUTES;
        long diffInMillies = date.getTime() - intervalStart.getTime();
        long minutes = timeUnit.convert(diffInMillies, TimeUnit.MILLISECONDS);
        return minutes <= 60;
    }

    public static boolean isTimeRange(String dateRange, Date tollDate) throws ParseException {

        SimpleDateFormat dateFormat = new SimpleDateFormat("HH:mm");
        Date tollTime = dateFormat.parse(dateFormat.format(tollDate));

        int index = dateRange.indexOf(DATE_DELIMITER);

        Date beginRangeTime = dateFormat.parse(dateRange.substring(0, index));
        Date endRangeTime = dateFormat.parse(dateRange.substring(index + 1));

        if ((tollTime.after(beginRangeTime) & tollTime.before(endRangeTime))
                || tollTime.equals(beginRangeTime)
                || tollTime.equals(endRangeTime)) {
            return true;
        }

        return false;
    }

    public static Boolean isWeekendOrHoliday(Date date) {
        Calendar calendar = getCalendarByDate(date);
        int month = getDateAttribute(calendar, Calendar.MONTH);
        int day = getDateAttribute(calendar, Calendar.DAY_OF_MONTH);

        int dayOfWeek = calendar.get(Calendar.DAY_OF_WEEK);
        if (dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY) return true;

        if (s_holiday_list.contains(month + DATE_DELIMITER + day)) {
            logger.info("{} is holiday", date);
            return true;
        }

        return false;
    }

    private static Calendar getFirstSaturday(int year, int month, int date) {
        for (int i = 0; i <= 6; i++) {
            Calendar tempCalendar = Calendar.getInstance();
            tempCalendar.set(year, (month - 1), date + i);
            int dayOfWeek = tempCalendar.get(Calendar.SATURDAY);
            if (dayOfWeek == Calendar.SATURDAY) {
                return tempCalendar;
            }
        }
        return null;
    }


    // https://date.nager.at/PublicHoliday/Country/SE/2020
    public static void initHolidayList(int year) {
        s_holiday_list = List.of(Calendar.JANUARY + "-1",
                Calendar.JANUARY + "-6",
                formatHoliday(getGoodFriday(year).getTime()),
                formatHoliday(getEasterSunday(year).getTime()),
                formatHoliday(getEasterMonday(year).getTime()),
                Calendar.MAY + "-1",
                formatHoliday(getAscensionDay(year).getTime()),
                formatHoliday(getWhitSunday(year).getTime()),
                Calendar.JUNE + "-6",
                formatHoliday(getMidsummerEveDay(year).getTime()),
                formatHoliday(getMidsummerDay(year).getTime()),
                formatHoliday(getAllSaintsDay(year).getTime()),
                Calendar.DECEMBER + "-24",
                Calendar.DECEMBER + "-25",
                Calendar.DECEMBER + "-26",
                Calendar.DECEMBER + "-31");
    }
}
