import org.junit.jupiter.api.Disabled
import org.junit.jupiter.api.DynamicTest
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.TestFactory

import java.util.*

import org.junit.jupiter.api.Assertions.assertEquals

internal class TollFeeTests {
    private val calculator = TollCalculator()

    @Test
    @Disabled(value = "This crashes at the moment")
    fun noFeeIfNever() {
        val fee = calculator.getTollFee(Car())
        assertEquals(0, fee)
    }

    @TestFactory
    fun tollFreeDates(): List<DynamicTest> {
        val normalCar = Car()
        return listOf(
                TollFreeDate.NEW_YEARS_DAY,
                TollFreeDate.MAUNDY_THURSDAY_13,
                TollFreeDate.GOOD_FRIDAY_13,
                TollFreeDate.APRIL_FOOL,
                TollFreeDate.KINGS_BIRTHDAY,
                TollFreeDate.LABOUR_DAY,
                TollFreeDate.ASCENSION_EVE_13,
                TollFreeDate.ASCENSION_DAY_13,
                TollFreeDate.NATIONAL_DAY_EVE,
                TollFreeDate.NATIONAL_DAY,
                TollFreeDate.JULY,
                TollFreeDate.HALLOWEEN_13,
                TollFreeDate.CHRISTMAS_EVE,
                TollFreeDate.CHRISTMAS_DAY,
                TollFreeDate.BOXING_DAY,
                TollFreeDate.NEW_YEAR_EVE

        ).map {
            DynamicTest.dynamicTest("$it") {
                val calendar = GregorianCalendar(it.year, it.month, it.day, 7, 5)
                val fee = calculator.getTollFee(normalCar, calendar.time)
                assertEquals(0, fee)
            }
        }
    }

    @TestFactory
    fun feesAtDifferentTimesOfDay(): List<DynamicTest> {
        val normalCar = Car()
        return mapOf(
                TimeOfDay.MIDNIGHT to 0,
                TimeOfDay.SIX_AM to 8,
                TimeOfDay.SEVEN_AM to 18,
                TimeOfDay.EIGHT_AM to 13,
                TimeOfDay.NOON to 0,
                TimeOfDay.THREE_PM to 13,
                TimeOfDay.FOUR_PM to 18,
                TimeOfDay.FIVE_PM to 13,
                TimeOfDay.SIX_PM to 8
        ).map {
            DynamicTest.dynamicTest("${it.key}") {
                val calendar = GregorianCalendar(2017, Calendar.MARCH, 3, it.key.hour, it.key.minute)
                val fee = calculator.getTollFee(normalCar, calendar.time)
                assertEquals(it.value, fee)
            }
        }
    }

    private enum class TollFreeDate(val year: Int, val month: Int, val day: Int) {
        NEW_YEARS_DAY(2013, Calendar.JANUARY, 1),
        MAUNDY_THURSDAY_13(2013, Calendar.MARCH, 28),
        GOOD_FRIDAY_13(2013, Calendar.MARCH, 29),
        APRIL_FOOL(2013, Calendar.APRIL, 1),
        KINGS_BIRTHDAY(2013, Calendar.APRIL, 30),
        LABOUR_DAY(2013, Calendar.MAY, 1),
        ASCENSION_EVE_13(2013, Calendar.MAY, 8),
        ASCENSION_DAY_13(2013, Calendar.MAY, 9),
        NATIONAL_DAY_EVE(2013, Calendar.JUNE, 5),
        NATIONAL_DAY(2013, Calendar.JUNE, 6),
        JULY(2013, Calendar.JULY, 9),
        HALLOWEEN_13(2013, Calendar.NOVEMBER, 1),
        CHRISTMAS_EVE(2013, Calendar.DECEMBER, 24),
        CHRISTMAS_DAY(2013, Calendar.DECEMBER, 25),
        BOXING_DAY(2013, Calendar.DECEMBER, 26),
        NEW_YEAR_EVE(2013, Calendar.DECEMBER, 31)
    }

    private enum class TimeOfDay(internal val hour: Int, internal val minute: Int) {
        MIDNIGHT(0, 0), SIX_AM(6, 5), SEVEN_AM(7, 5), EIGHT_AM(8, 5),
        NOON(12, 0), THREE_PM(15, 5), FOUR_PM(16, 5), FIVE_PM(17, 0),
        SIX_PM(18, 5)
    }
}
