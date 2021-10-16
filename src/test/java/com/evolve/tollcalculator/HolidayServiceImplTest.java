package com.evolve.tollcalculator;

import com.evolve.services.HolidayService;
import com.evolve.services.HolidayServiceImpl;
import org.junit.Test;
import org.junit.Assert;

import java.time.LocalDate;

public class HolidayServiceImplTest {
    private final HolidayService holidayService;
    private static final int YEAR = 2021;

    public HolidayServiceImplTest() {
        holidayService = new HolidayServiceImpl();
    }

    @Test
    public void testFixedHolidays() {
        Assert.assertTrue("New Year's Day", holidayService.isHoliday(LocalDate.of(YEAR, 1, 1)));
        Assert.assertTrue("New Year's Eve", holidayService.isHoliday(LocalDate.of(YEAR, 12, 31)));
        Assert.assertTrue("Epiphany", holidayService.isHoliday(LocalDate.of(YEAR, 1, 6)));
        Assert.assertTrue("Walpurgis Night", holidayService.isHoliday(LocalDate.of(YEAR, 4, 30)));
        Assert.assertTrue("May 1st", holidayService.isHoliday(LocalDate.of(YEAR, 5, 1)));
        Assert.assertTrue("National Day", holidayService.isHoliday(LocalDate.of(YEAR, 6, 6)));
        Assert.assertTrue("Christmas's Eve", holidayService.isHoliday(LocalDate.of(YEAR, 12, 24)));
        Assert.assertTrue("Christmas Day", holidayService.isHoliday(LocalDate.of(YEAR, 12, 25)));
        Assert.assertTrue("Boxing Day", holidayService.isHoliday(LocalDate.of(YEAR, 12, 26)));
    }

    @Test
    public void testMidsummer() {
        LocalDate midsummer = LocalDate.of(YEAR, 6, 26);
        Assert.assertTrue("Midsummer's Day", holidayService.isHoliday(midsummer));
        Assert.assertTrue("Midsummer's Eve", holidayService.isHoliday(midsummer.minusDays(1)));
    }

    @Test
    public void testAllSaints() {
        LocalDate allSaints = LocalDate.of(YEAR, 11, 6);
        Assert.assertTrue("All Saints' Day", holidayService.isHoliday(allSaints));
        Assert.assertTrue("All Saints' Eve", holidayService.isHoliday(allSaints.minusDays(1)));
    }

    @Test
    public void testEaster() {
        LocalDate easterSunday = LocalDate.of(YEAR, 4, 4);
        Assert.assertTrue("Easter Sunday", holidayService.isHoliday(easterSunday));
        Assert.assertTrue("Easter's Eve", holidayService.isHoliday(easterSunday.minusDays(1)));
        Assert.assertTrue("Good Friday", holidayService.isHoliday(easterSunday.minusDays(2)));
        Assert.assertTrue("Easter Monday", holidayService.isHoliday(easterSunday.plusDays(1)));
        Assert.assertTrue("Ascension Day", holidayService.isHoliday(easterSunday.plusDays(39)));
    }
}
