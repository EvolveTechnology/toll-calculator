package tolls

import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Assertions.assertNotEquals
import org.junit.jupiter.api.DynamicTest
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.TestFactory
import java.util.*

internal class TollFeeTests {
    private val arbitraryNonHoliday = CalendarDay(2017, Calendar.MARCH, 3)

    @Test
    fun noFeeIfNeverPassed() {
        val calculator = TollCalculator(VehicleType.CAR, CalendarDay(2017, Calendar.MARCH, 3))
        val fee = calculator.tollFee
        assertEquals(0, fee)
    }

    @Test
    fun multiplePassesAddUp() {
        val calculator = TollCalculator(VehicleType.CAR, arbitraryNonHoliday)
        calculator.passToll(TimeOfDay(6, 0))  //  8 SEK
        calculator.passToll(TimeOfDay(15, 0)) // 13 SEK

        val fee = calculator.tollFee
        assertEquals(21, fee)
    }

    @Test
    fun maxesOutAt60SEK() {
        val calculator = TollCalculator(VehicleType.CAR, arbitraryNonHoliday)
        calculator.passToll(TimeOfDay(6, 30))  // 13 SEK
        calculator.passToll(TimeOfDay(7, 31))  // 18 SEK
        calculator.passToll(TimeOfDay(8, 32))  //  8 SEK
        calculator.passToll(TimeOfDay(9, 33))  //  8 SEK
        calculator.passToll(TimeOfDay(10, 34)) //  8 SEK
        calculator.passToll(TimeOfDay(11, 35)) //  8 SEK
        calculator.passToll(TimeOfDay(12, 36)) //  8 SEK
        calculator.passToll(TimeOfDay(15, 0))  // 13 SEK

        val fee = calculator.tollFee
        assertEquals(60, fee)
    }

    @Test
    fun twoPassesInOneHourCostOnlyOneFee() {
        val calculator = TollCalculator(VehicleType.CAR, arbitraryNonHoliday)
        calculator.passToll(TimeOfDay(6, 5))  // 13 SEK
        calculator.passToll(TimeOfDay(7, 4))  // 18 SEK

        val fee = calculator.tollFee
        assertEquals(18, fee)
    }

    @Test
    fun manyPassesInOneHourCostOnlyOneFee() {
        val calculator = TollCalculator(VehicleType.CAR, arbitraryNonHoliday)
        calculator.passToll(TimeOfDay(6, 6))  //  8 SEK
        calculator.passToll(TimeOfDay(6, 32)) // 13 SEK
        calculator.passToll(TimeOfDay(7, 3))  // 18 SEK

        val fee = calculator.tollFee
        assertEquals(18, fee)
    }

    @Test
    fun freePassesAreIgnored() {
        val calculator = TollCalculator(VehicleType.CAR, arbitraryNonHoliday)
        calculator.passToll(TimeOfDay(5, 56)) // free
        calculator.passToll(TimeOfDay(6, 29)) //  8 SEK
        calculator.passToll(TimeOfDay(7, 10)) // 18 SEK

        val fee = calculator.tollFee
        assertEquals(18, fee)
    }

    @Test
    fun tollFreeForMotorcycles() {
        val calculator = TollCalculator(VehicleType.MOTORBIKE, arbitraryNonHoliday)
        calculator.passToll(TimeOfDay(7, 0))

        val fee = calculator.tollFee
        assertEquals(0, fee)
    }

    @TestFactory
    fun tollFreeOnFixedHolidays(): List<DynamicTest> {
        return mapOf(
                "New Year's Day, 2013" to CalendarDay(2013, Calendar.JANUARY, 1),
                "April 1, 2013" to CalendarDay(2013, Calendar.APRIL, 1),
                "King's birthday, 2013" to CalendarDay(2013, Calendar.APRIL, 30),
                "Labour Day 2013" to CalendarDay(2013, Calendar.MAY, 1),
                "Day before national day, 2013" to CalendarDay(2013, Calendar.JUNE, 5),
                "National day, 2013" to CalendarDay(2013, Calendar.JUNE, 6),
                "Arbitrary day in July, 2013" to CalendarDay(2013, Calendar.JULY, 9),
                "Halloween, 2013" to CalendarDay(2013, Calendar.NOVEMBER, 1),
                "Christmas Eve, 2013" to CalendarDay(2013, Calendar.DECEMBER, 24),
                "Christmas Day, 2013" to CalendarDay(2013, Calendar.DECEMBER, 25),
                "Boxing Day, 2013" to CalendarDay(2013, Calendar.DECEMBER, 26),
                "New Rear's Eve, 2013" to CalendarDay(2013, Calendar.DECEMBER, 31),

                "New Year's Day, 2017" to CalendarDay(2017, Calendar.JANUARY, 1),
                "April 1, 2017" to CalendarDay(2017, Calendar.APRIL, 1),
                "King's birthday, 2017" to CalendarDay(2017, Calendar.APRIL, 30),
                "Labour Day 2017" to CalendarDay(2017, Calendar.MAY, 1),
                "Day before national day, 2017" to CalendarDay(2017, Calendar.JUNE, 5),
                "National day, 2017" to CalendarDay(2017, Calendar.JUNE, 6),
                "Arbitrary day in July, 2017" to CalendarDay(2017, Calendar.JULY, 9),
                "Halloween, 2017" to CalendarDay(2017, Calendar.NOVEMBER, 1),
                "Christmas Eve, 2017" to CalendarDay(2017, Calendar.DECEMBER, 24),
                "Christmas Day, 2017" to CalendarDay(2017, Calendar.DECEMBER, 25),
                "Boxing Day, 2017" to CalendarDay(2017, Calendar.DECEMBER, 26),
                "New Rear's Eve, 2017" to CalendarDay(2017, Calendar.DECEMBER, 31),

                "Arbitrary Saturday" to CalendarDay(2017, Calendar.JUNE, 10),
                "Arbitrary Sunday" to CalendarDay(2017, Calendar.JUNE, 11)
        ).map {
            DynamicTest.dynamicTest(it.key) {
                val calculator = TollCalculator(VehicleType.CAR, it.value)
                calculator.passToll(TimeOfDay(7, 0))

                val fee = calculator.tollFee
                assertEquals(0, fee)
            }
        }
    }

    @TestFactory
    fun tollFreeOnHolidays(): List<DynamicTest> {
        return mapOf(
                "Midsummer's Eve 2017" to CalendarDay(2017, Calendar.APRIL, 23),
                "Eve of Ascension 2017" to CalendarDay(2017, Calendar.MAY, 24),
                "Day of the Ascension 2017" to CalendarDay(2017, Calendar.MAY, 25),
                "Maundy Thursday 2021" to CalendarDay(2021, Calendar.APRIL, 2),
                "Good Friday 2021" to CalendarDay(2021, Calendar.APRIL, 3),
                "Good Friday 2099" to CalendarDay(2099, Calendar.APRIL, 11),
                "Eve of Ascension 2021" to CalendarDay(2021, Calendar.MAY, 12),
                "Day of the Ascension 2021" to CalendarDay(2021, Calendar.MAY, 13)
        ).map {
            DynamicTest.dynamicTest(it.key) {
                val calculator = TollCalculator(VehicleType.CAR, it.value)
                calculator.passToll(TimeOfDay(7, 0))

                val fee = calculator.tollFee
                assertEquals(0, fee)
            }
        }
    }

    @Test
    fun notTollFreeOnNormalDays() {
        val calculator = TollCalculator(VehicleType.CAR, CalendarDay(2017, Calendar.JUNE, 20))
        calculator.passToll(TimeOfDay(7, 0))

        val fee = calculator.tollFee
        assertNotEquals(0, fee)
    }

    @TestFactory
    fun feesAtDifferentTimesOfDay(): List<DynamicTest> {
        return mapOf(
                TimeOfDay(0, 0) to 0,
                TimeOfDay(6, 0) to 8,
                TimeOfDay(7, 0) to 18,
                TimeOfDay(8, 0) to 13,
                TimeOfDay(12, 0) to 8,
                TimeOfDay(15, 0) to 13,
                TimeOfDay(16, 0) to 18,
                TimeOfDay(17, 0) to 13,
                TimeOfDay(18, 0) to 8
        ).map {
            DynamicTest.dynamicTest("${it.key}") {
                val calculator = TollCalculator(VehicleType.CAR, arbitraryNonHoliday)
                calculator.passToll(it.key)

                val fee = calculator.tollFee
                assertEquals(it.value, fee)
            }
        }
    }
}
