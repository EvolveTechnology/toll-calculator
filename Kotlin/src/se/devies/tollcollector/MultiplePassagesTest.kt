package se.devies.tollcollector
import se.devies.tollcollector.holiday.MockHolidayChecker
import org.junit.Test

import org.junit.Assert.*
import se.devies.tollcollector.vehicle.Car
import java.util.*

class MultiplePassagesTest {
    private val tollCalculator = TollCalculator(MockHolidayChecker())

    @Test
    fun multiplePassesShouldAddUp() {
        val car = Car()

        val dates = arrayOf(
            GregorianCalendar(2019, 3, 1, 5, 30),
            GregorianCalendar(2019, 3, 1, 6, 40),
            GregorianCalendar(2019, 3, 1, 8, 0),
            GregorianCalendar(2019, 3, 1, 14, 45),
            GregorianCalendar(2019, 3, 1, 22, 45)
        )

        assertEquals(41, tollCalculator.getTotalTollFeeForOneDay(car, *dates))
    }

    @Test
    fun aVehicleShouldOnlyBeChargeOncePerHour() {
        val car = Car()

        var dates = arrayOf(
            GregorianCalendar(2019, 3, 1, 6, 40),
            GregorianCalendar(2019, 3, 1, 6, 41),
            GregorianCalendar(2019, 3, 1, 6, 59)
        )

        assertEquals(16, tollCalculator.getTotalTollFeeForOneDay(car, *dates))

        dates = arrayOf(
            GregorianCalendar(2019, 3, 1, 6, 40),
            GregorianCalendar(2019, 3, 1, 6, 41),
            GregorianCalendar(2019, 3, 1, 6, 59),
            GregorianCalendar(2019, 3, 1, 7, 59)
        )

        assertEquals(38, tollCalculator.getTotalTollFeeForOneDay(car, *dates))
    }

    @Test
    fun passageParametersDoesNotNeedToBeSorted() {
        val car = Car()

        val dates = arrayOf(
            GregorianCalendar(2019, 3, 1, 6, 59),
            GregorianCalendar(2019, 3, 1, 7, 59),
            GregorianCalendar(2019, 3, 1, 6, 40),
            GregorianCalendar(2019, 3, 1, 6, 41)
        )

        assertEquals(38, tollCalculator.getTotalTollFeeForOneDay(car, *dates))
    }

    @Test
    fun maximumFeeIs60SEK() {
        val car = Car()

        val dates = arrayOf(
            GregorianCalendar(2019, 3, 1, 6, 0),
            GregorianCalendar(2019, 3, 1, 7, 0),
            GregorianCalendar(2019, 3, 1, 8, 0),
            GregorianCalendar(2019, 3, 1, 9, 0),
            GregorianCalendar(2019, 3, 1, 10, 0),
            GregorianCalendar(2019, 3, 1, 11, 0),
            GregorianCalendar(2019, 3, 1, 12, 0),
            GregorianCalendar(2019, 3, 1, 13, 0),
            GregorianCalendar(2019, 3, 1, 14, 0),
            GregorianCalendar(2019, 3, 1, 15, 0),
            GregorianCalendar(2019, 3, 1, 16, 0),
            GregorianCalendar(2019, 3, 1, 17, 0),
            GregorianCalendar(2019, 3, 1, 18, 0)
        )

        assertEquals(60, tollCalculator.getTotalTollFeeForOneDay(car, *dates))
    }
}