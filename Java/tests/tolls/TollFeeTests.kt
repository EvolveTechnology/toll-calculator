package tolls

import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Assertions.assertNotEquals
import org.junit.jupiter.api.DynamicTest
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.TestFactory
import java.util.*

internal class TollFeeTests {

    @Test
    fun noFeeIfNeverPassed() {
        val calculator = TollCalculator(VehicleType.CAR, CalendarDay(2017, Calendar.MARCH, 3, SwedishCalendar()))
        val fee = calculator.tollFee
        assertEquals(0, fee)
    }

    @Test
    fun multiplePassesAddUp() {
        val calculator = TollCalculator(VehicleType.CAR, PaidDate.ARBITRARY_DATE.calendarDay)
        calculator.passToll(TimeOfDay(6, 0))  //  8 SEK
        calculator.passToll(TimeOfDay(15, 0)) // 13 SEK

        val fee = calculator.tollFee
        assertEquals(21, fee)
    }

    @Test
    fun maxesOutAt60SEK() {
        val calculator = TollCalculator(VehicleType.CAR, PaidDate.ARBITRARY_DATE.calendarDay)
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
        val calculator = TollCalculator(VehicleType.CAR, PaidDate.ARBITRARY_DATE.calendarDay)
        calculator.passToll(TimeOfDay(6, 5))  // 13 SEK
        calculator.passToll(TimeOfDay(7, 4))  // 18 SEK

        val fee = calculator.tollFee
        assertEquals(18, fee)
    }

    @Test
    fun manyPassesInOneHourCostOnlyOneFee() {
        val calculator = TollCalculator(VehicleType.CAR, PaidDate.ARBITRARY_DATE.calendarDay)
        calculator.passToll(TimeOfDay(6, 6))  //  8 SEK
        calculator.passToll(TimeOfDay(6, 32)) // 13 SEK
        calculator.passToll(TimeOfDay(7, 3))  // 18 SEK

        val fee = calculator.tollFee
        assertEquals(18, fee)
    }

    @Test
    fun freePassesAreIgnored() {
        val calculator = TollCalculator(VehicleType.CAR, PaidDate.ARBITRARY_DATE.calendarDay)
        calculator.passToll(TimeOfDay(5, 56)) // free
        calculator.passToll(TimeOfDay(6, 29)) //  8 SEK
        calculator.passToll(TimeOfDay(7, 10)) // 18 SEK

        val fee = calculator.tollFee
        assertEquals(18, fee)
    }

    @Test
    fun tollFreeForMotorcycles() {
        val calculator = TollCalculator(VehicleType.MOTORBIKE, PaidDate.ARBITRARY_DATE.calendarDay)
        calculator.passToll(TimeOfDay(7, 0))

        val fee = calculator.tollFee
        assertEquals(0, fee)
    }

    @TestFactory
    fun tollFreeOnFixedHolidays(): List<DynamicTest> {
        return TollFreeDate.values().map {
            DynamicTest.dynamicTest("$it") {
                val calculator = TollCalculator(VehicleType.CAR, it.calendarDay)
                calculator.passToll(TimeOfDay(7, 0))

                val fee = calculator.tollFee
                assertEquals(0, fee)
            }
        }
    }

    @Test
    fun tollFreeOnHolidays() {
        val calculator = TollCalculator(VehicleType.CAR, CalendarDay(2017, Calendar.JUNE, 20, HolidayCalendar { _, _, _ -> true }))
        calculator.passToll(TimeOfDay(7, 0))

        val fee = calculator.tollFee
        assertEquals(0, fee)
    }

    @Test
    fun notTollFreeOnNormalDays() {
        val calculator = TollCalculator(VehicleType.CAR, CalendarDay(2017, Calendar.JUNE, 20, HolidayCalendar { _, _, _ -> false }))
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
                val calculator = TollCalculator(VehicleType.CAR, PaidDate.ARBITRARY_DATE.calendarDay)
                calculator.passToll(it.key)

                val fee = calculator.tollFee
                assertEquals(it.value, fee)
            }
        }
    }

    private interface TollDate {
        var year: Int get
        var month: Int get
        var day: Int get

        val calendarDay: CalendarDay get() = CalendarDay(year, month, day, SwedishCalendar())
    }

    // Hmm... These properties can be set. (Don't do that!)
    // Kotlin doesn't allow `val` to implement `var ... get` (as Swift would).
    // So the question is does code reuse trump enforced immutability?
    private enum class PaidDate(
            override var year: Int,
            override var month: Int,
            override var day: Int) : TollDate {
        ARBITRARY_DATE(2017, Calendar.MARCH, 3);
    }

    private enum class TollFreeDate(
            override var year: Int,
            override var month: Int,
            override var day: Int) : TollDate {
        NEW_YEARS_DAY(2013, Calendar.JANUARY, 1),
        APRIL_FOOL(2013, Calendar.APRIL, 1),
        KINGS_BIRTHDAY(2013, Calendar.APRIL, 30),
        LABOUR_DAY(2013, Calendar.MAY, 1),
        NATIONAL_DAY_EVE(2013, Calendar.JUNE, 5),
        NATIONAL_DAY(2013, Calendar.JUNE, 6),
        JULY(2013, Calendar.JULY, 9),
        HALLOWEEN(2013, Calendar.NOVEMBER, 1),
        CHRISTMAS_EVE(2013, Calendar.DECEMBER, 24),
        CHRISTMAS_DAY(2013, Calendar.DECEMBER, 25),
        BOXING_DAY(2013, Calendar.DECEMBER, 26),
        NEW_YEARS_EVE(2013, Calendar.DECEMBER, 31),
        SOME_SATURDAY(2017, Calendar.JUNE, 10),
        SOME_SUNDAY(2017, Calendar.JUNE, 11),
    }
}
