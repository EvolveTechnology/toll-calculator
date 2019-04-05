package se.devies.tollcollector.weekend

import org.junit.Assert.*
import org.junit.Test
import java.util.*

class WeekendCheckerTest {
    val weekendChecker = WeekendChecker()

    @Test
    fun saturdaysShouldBeWeekend() {
        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 6, 7, 0),
            GregorianCalendar(2019, 3, 13, 7, 0)
        )

        testCases.forEach { date -> assertTrue(weekendChecker.isWeekend(date)) }
    }

    @Test
    fun sundaysShouldBeWeekend() {
        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 7, 7, 0),
            GregorianCalendar(2019, 3, 14, 7, 0)
        )

        testCases.forEach { date -> assertTrue(weekendChecker.isWeekend(date)) }
    }

    @Test
    fun mondaysShouldNotBeWeekend() {
        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 1, 7, 0),
            GregorianCalendar(2019, 3, 8, 7, 0)
        )

        testCases.forEach { date -> assertFalse(weekendChecker.isWeekend(date)) }
    }

    @Test
    fun fridaysShouldNotBeWeekend() {
        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 5, 7, 0),
            GregorianCalendar(2019, 3, 12, 7, 0)
        )

        testCases.forEach { date -> assertFalse(weekendChecker.isWeekend(date)) }
    }
}