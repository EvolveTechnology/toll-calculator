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
        val fee = calculator.getTollFee(VehicleType.CAR)
        assertEquals(0, fee)
    }

    @Test
    fun multiplePassesAddUp() {
        val offToWork = PaidDate.ARBITRARY_DATE.atTime(TimeOfDay.SIX_AM)
        val goingHome = PaidDate.ARBITRARY_DATE.atTime(TimeOfDay.THREE_PM)

        val fee = calculator.getTollFee(VehicleType.CAR, offToWork, goingHome)
        assertEquals(21, fee)
    }

    @Test
    fun tollFreeForMotorcycles() {
        val date = PaidDate.ARBITRARY_DATE.atTime(TimeOfDay.SEVEN_AM)
        val fee = calculator.getTollFee(VehicleType.MOTORBIKE, date)
        assertEquals(0, fee)
    }

    @TestFactory
    fun tollFreeDates(): List<DynamicTest> {
        val normalCar = VehicleType.CAR
        return TollFreeDate.values().map {
            DynamicTest.dynamicTest("$it") {
                val date = it.atTime(TimeOfDay.SEVEN_AM)
                val fee = calculator.getTollFee(normalCar, date)
                assertEquals(0, fee)
            }
        }
    }

    @TestFactory
    fun feesAtDifferentTimesOfDay(): List<DynamicTest> {
        val normalCar = VehicleType.CAR
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
                val date = PaidDate.ARBITRARY_DATE.atTime(it.key)
                val fee = calculator.getTollFee(normalCar, date)
                assertEquals(it.value, fee)
            }
        }
    }

    private interface TollDate {
        var year: Int get
        var month: Int get
        var day: Int get

        fun atTime(time: TimeOfDay): Date {
            val calendar = GregorianCalendar.getInstance()
            calendar.set(year, month, day, time.hour, time.minute)
            return calendar.time
        }
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
        NEW_YEAR_EVE(2013, Calendar.DECEMBER, 31);
    }

    private enum class TimeOfDay(internal val hour: Int, internal val minute: Int) {
        MIDNIGHT(0, 0), SIX_AM(6, 5), SEVEN_AM(7, 5), EIGHT_AM(8, 5),
        NOON(12, 0), THREE_PM(15, 5), FOUR_PM(16, 5), FIVE_PM(17, 0),
        SIX_PM(18, 5)
    }
}

// fix bugs
//     _13 -> _17 (different dates for different years)
//     getTollFee() only work if all dates are same-day - otherwise they should not limit to 60
//     Enter no dates and it crashes
// refactor code
