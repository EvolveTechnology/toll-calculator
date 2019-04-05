package se.devies.tollcollector

import se.devies.tollcollector.holiday.MockHolidayChecker
import org.junit.Test

import org.junit.Assert.*
import se.devies.tollcollector.vehicle.*
import java.util.*

// I have made the assumption that a se.devies.tollcollector.vehicle can only be of a single type,
// i.e., no "emergency tractors" etc.
class TollFreeVehiclesTest {
    private val tollCalculator = TollCalculator(MockHolidayChecker())

    @Test
    fun carsShouldPayToll() {
        val car = Car()

        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 1, 7, 0),
            GregorianCalendar(2019, 3, 1, 8, 10),
            GregorianCalendar(2019, 3, 1, 9, 59),
            GregorianCalendar(2019, 3, 1, 12, 30),
            GregorianCalendar(2019, 3, 1, 15, 30),
            GregorianCalendar(2019, 3, 1, 16, 59)
        )

        testCases.forEach{ date -> assertNotEquals(0, tollCalculator.getTotalTollFeeForOneDay(car, date)) }
    }

    @Test
    fun diplomatVehiclesShouldNotPayToll() {
        val vehicle = Diplomat()

        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 1, 7, 0),
            GregorianCalendar(2019, 3, 1, 8, 10),
            GregorianCalendar(2019, 3, 1, 9, 59),
            GregorianCalendar(2019, 3, 1, 12, 30),
            GregorianCalendar(2019, 3, 1, 15, 30),
            GregorianCalendar(2019, 3, 1, 16, 59)
        )

        testCases.forEach{ date -> assertEquals(0, tollCalculator.getTotalTollFeeForOneDay(vehicle, date)) }
    }

    @Test
    fun emergencyVehiclesShouldNotPayToll() {
        val vehicle = Emergency()

        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 1, 7, 0),
            GregorianCalendar(2019, 3, 1, 8, 10),
            GregorianCalendar(2019, 3, 1, 9, 59),
            GregorianCalendar(2019, 3, 1, 12, 30),
            GregorianCalendar(2019, 3, 1, 15, 30),
            GregorianCalendar(2019, 3, 1, 16, 59)
        )

        testCases.forEach{ date -> assertEquals(0, tollCalculator.getTotalTollFeeForOneDay(vehicle, date)) }
    }

    @Test
    fun foreignVehiclesShouldNotPayToll() {
        val vehicle = Foreign()

        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 1, 7, 0),
            GregorianCalendar(2019, 3, 1, 8, 10),
            GregorianCalendar(2019, 3, 1, 9, 59),
            GregorianCalendar(2019, 3, 1, 12, 30),
            GregorianCalendar(2019, 3, 1, 15, 30),
            GregorianCalendar(2019, 3, 1, 16, 59)
        )

        testCases.forEach{ date -> assertEquals(0, tollCalculator.getTotalTollFeeForOneDay(vehicle, date)) }
    }

    @Test
    fun militaryVehiclesShouldNotPayToll() {
        val vehicle = Military()

        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 1, 7, 0),
            GregorianCalendar(2019, 3, 1, 8, 10),
            GregorianCalendar(2019, 3, 1, 9, 59),
            GregorianCalendar(2019, 3, 1, 12, 30),
            GregorianCalendar(2019, 3, 1, 15, 30),
            GregorianCalendar(2019, 3, 1, 16, 59)
        )

        testCases.forEach{ date -> assertEquals(0, tollCalculator.getTotalTollFeeForOneDay(vehicle, date)) }
    }

    @Test
    fun motorbikesShouldNotPayToll() {
        val vehicle = Motorbike()

        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 1, 7, 0),
            GregorianCalendar(2019, 3, 1, 8, 10),
            GregorianCalendar(2019, 3, 1, 9, 59),
            GregorianCalendar(2019, 3, 1, 12, 30),
            GregorianCalendar(2019, 3, 1, 15, 30),
            GregorianCalendar(2019, 3, 1, 16, 59)
        )

        testCases.forEach{ date -> assertEquals(0, tollCalculator.getTotalTollFeeForOneDay(vehicle, date)) }
    }

    @Test
    fun tractorsShouldNotPayToll() {
        val vehicle = Tractor()

        val testCases: ArrayList<GregorianCalendar> = arrayListOf(
            GregorianCalendar(2019, 3, 1, 7, 0),
            GregorianCalendar(2019, 3, 1, 8, 10),
            GregorianCalendar(2019, 3, 1, 9, 59),
            GregorianCalendar(2019, 3, 1, 12, 30),
            GregorianCalendar(2019, 3, 1, 15, 30),
            GregorianCalendar(2019, 3, 1, 16, 59)
        )

        testCases.forEach{ date -> assertEquals(0, tollCalculator.getTotalTollFeeForOneDay(vehicle, date)) }
    }
}