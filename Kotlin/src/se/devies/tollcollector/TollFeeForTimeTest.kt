package se.devies.tollcollector

import se.devies.tollcollector.holiday.MockHolidayChecker
import org.junit.Test

import org.junit.Assert.*
import se.devies.tollcollector.vehicle.Car
import java.util.*

class TollFeeForTimeTest {
    private val tollCalculator = TollCalculator(MockHolidayChecker())

    @Test
    fun zeroPassesShouldGiveNoFee() {
        val car = Car()

        assertEquals(0, tollCalculator.getTotalTollFeeForOneDay(car))
    }

    @Test
    fun rushHourFee() {
        val car = Car()

        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 1, 7, 0),
            GregorianCalendar(2019, 3, 1, 7, 10),
            GregorianCalendar(2019, 3, 1, 7, 59),
            GregorianCalendar(2019, 3, 1, 15, 30),
            GregorianCalendar(2019, 3, 1, 16, 30),
            GregorianCalendar(2019, 3, 1, 16, 59)
        )

        testCases.forEach{ date -> assertEquals(22, tollCalculator.getTotalTollFeeForOneDay(car, date)) }
    }

    @Test
    fun normalHourFee() {
        val car = Car()

        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 1, 6, 30),
            GregorianCalendar(2019, 3, 1, 6, 40),
            GregorianCalendar(2019, 3, 1, 6, 59),
            GregorianCalendar(2019, 3, 1, 8, 0),
            GregorianCalendar(2019, 3, 1, 8, 15),
            GregorianCalendar(2019, 3, 1, 8, 29),
            GregorianCalendar(2019, 3, 1, 15, 0),
            GregorianCalendar(2019, 3, 1, 15, 29),
            GregorianCalendar(2019, 3, 1, 17, 0),
            GregorianCalendar(2019, 3, 1, 17, 59)
        )

        testCases.forEach{ date -> assertEquals(16, tollCalculator.getTotalTollFeeForOneDay(car, date)) }

    }

    @Test
    fun lowHourFee() {
        val car = Car()

        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 1, 6, 0),
            GregorianCalendar(2019, 3, 1, 6, 29),
            GregorianCalendar(2019, 3, 1, 8, 30),
            GregorianCalendar(2019, 3, 1, 9, 30),
            GregorianCalendar(2019, 3, 1, 10, 0),
            GregorianCalendar(2019, 3, 1, 11, 0),
            GregorianCalendar(2019, 3, 1, 12, 0),
            GregorianCalendar(2019, 3, 1, 13, 0),
            GregorianCalendar(2019, 3, 1, 14, 0),
            GregorianCalendar(2019, 3, 1, 14, 59),
            GregorianCalendar(2019, 3, 1, 18, 0),
            GregorianCalendar(2019, 3, 1, 18, 29)
        )

        testCases.forEach{ date -> assertEquals(9, tollCalculator.getTotalTollFeeForOneDay(car, date)) }
    }

    @Test
    fun freeHours() {
        val car = Car()

        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 1, 0, 0),
            GregorianCalendar(2019, 3, 1, 0, 1),
            GregorianCalendar(2019, 3, 1, 1, 1),
            GregorianCalendar(2019, 3, 1, 2, 10),
            GregorianCalendar(2019, 3, 1, 3, 10),
            GregorianCalendar(2019, 3, 1, 4, 10),
            GregorianCalendar(2019, 3, 1, 5, 10),
            GregorianCalendar(2019, 3, 1, 5, 59),
            GregorianCalendar(2019, 3, 1, 18, 30),
            GregorianCalendar(2019, 3, 1, 19, 30),
            GregorianCalendar(2019, 3, 1, 20, 0),
            GregorianCalendar(2019, 3, 1, 21, 0),
            GregorianCalendar(2019, 3, 1, 22, 0),
            GregorianCalendar(2019, 3, 1, 23, 0),
            GregorianCalendar(2019, 3, 1, 23, 59)
        )

        testCases.forEach{ date -> assertEquals(0, tollCalculator.getTotalTollFeeForOneDay(car, date)) }
    }
}