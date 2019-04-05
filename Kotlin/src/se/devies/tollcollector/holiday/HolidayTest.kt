package se.devies.tollcollector.holiday

import org.junit.Test

import org.junit.Assert.*
import se.devies.tollcollector.TollCalculator
import se.devies.tollcollector.vehicle.*
import java.util.*

// I have made the assumption that a vehicle can only be of a single type,
// i.e., no "emergency tractors" etc.
class HolidayTest {
    private val tollCalculator = TollCalculator(MockHolidayChecker())

    @Test
    fun nationalDayShouldBeTollFree() {
        val car = Car()

        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 5, 6, 7, 0)
        )

        testCases.forEach { date -> assertEquals(0, tollCalculator.getTotalTollFeeForOneDay(car, date)) }
    }

    @Test
    fun firstOfMay() {
        val car = Car()

        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 4, 1, 7, 0)
        )

        testCases.forEach { date -> assertEquals(0, tollCalculator.getTotalTollFeeForOneDay(car, date)) }
    }
}