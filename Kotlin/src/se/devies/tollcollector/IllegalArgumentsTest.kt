package se.devies.tollcollector
import se.devies.tollcollector.holiday.MockHolidayChecker
import org.junit.Test

import org.junit.Assert.*
import se.devies.tollcollector.vehicle.Car
import java.lang.IllegalArgumentException
import java.util.*
import kotlin.test.assertFailsWith

class IllegalArgumentsTest {
    private val tollCalculator = TollCalculator(MockHolidayChecker())

    @Test
    fun differentDaysShouldThrowIllegalArgumentException() {
        val car = Car()

        val dates = arrayOf(
            GregorianCalendar(2019, 3, 1, 5, 30),
            GregorianCalendar(2019, 3, 2, 5, 30)
        )

        assertFailsWith(IllegalArgumentException::class) {
            tollCalculator.getTotalTollFeeForOneDay(car, *dates)
        }
    }
}