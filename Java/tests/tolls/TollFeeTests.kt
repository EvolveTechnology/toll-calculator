package tolls

import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.DynamicTest
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.TestFactory
import java.util.*

internal class TollFeeTests {

    @Test
    fun noFeeIfNeverPassed() {
        val calculator = TollCalculator(VehicleType.CAR)
        val fee = calculator.getTollFee()
        assertEquals(0, fee)
    }

    @Test
    fun multiplePassesAddUp() {
        val calculator = TollCalculator(VehicleType.CAR)
        val fee = calculator.getTollFee(
                PaidDate.ARBITRARY_DATE.atTime(TimeOfDay(6, 0)),  //  8 SEK
                PaidDate.ARBITRARY_DATE.atTime(TimeOfDay(15, 0))) // 13 SEK
        assertEquals(21, fee)
    }

    @Test
    fun twoPassesInOneHourCostOnlyOneFee() {
        val calculator = TollCalculator(VehicleType.CAR)
        val fee = calculator.getTollFee(
                PaidDate.ARBITRARY_DATE.atTime(TimeOfDay(6, 5)), // 13 SEK
                PaidDate.ARBITRARY_DATE.atTime(TimeOfDay(7, 4))) // 18 SEK
        assertEquals(18, fee)
    }

    @Test
    fun manyPassesInOneHourCostOnlyOneFee() {
        val calculator = TollCalculator(VehicleType.CAR)
        val fee = calculator.getTollFee(
                PaidDate.ARBITRARY_DATE.atTime(6, 6),  //  8 SEK
                PaidDate.ARBITRARY_DATE.atTime(6, 32), // 13 SEK
                PaidDate.ARBITRARY_DATE.atTime(7, 3))  // 18 SEK
        assertEquals(18, fee)
    }

    @Test
    fun freePassesAreIgnored() {
        val calculator = TollCalculator(VehicleType.CAR)
        val fee = calculator.getTollFee(
                PaidDate.ARBITRARY_DATE.atTime(5, 56), // free
                PaidDate.ARBITRARY_DATE.atTime(6, 29), //  8 SEK
                PaidDate.ARBITRARY_DATE.atTime(7, 10)) // 18 SEK
        assertEquals(18, fee)
    }

    @Test
    fun tollFreeForMotorcycles() {
        val calculator = TollCalculator(VehicleType.MOTORBIKE)
        val date = PaidDate.ARBITRARY_DATE.atTime(TimeOfDay(7, 0))
        val fee = calculator.getTollFee(date)
        assertEquals(0, fee)
    }

    @TestFactory
    fun tollFreeDates(): List<DynamicTest> {
        val calculator = TollCalculator(VehicleType.CAR)
        return TollFreeDate.values().map {
            DynamicTest.dynamicTest("$it") {
                val date = it.atTime(TimeOfDay(7, 0))
                val fee = calculator.getTollFee(date)
                assertEquals(0, fee)
            }
        }
    }

    @TestFactory
    fun feesAtDifferentTimesOfDay(): List<DynamicTest> {
        val calculator = TollCalculator(VehicleType.CAR)
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
                val date = PaidDate.ARBITRARY_DATE.atTime(it.key)
                val fee = calculator.getTollFee(date)
                assertEquals(it.value, fee)
            }
        }
    }

    private interface TollDate {
        var year: Int get
        var month: Int get
        var day: Int get

        fun atTime(hour: Int, minute: Int): Date {
            val calendar = GregorianCalendar.getInstance()
            calendar.set(year, month, day, hour, minute)
            return calendar.time
        }

        fun atTime(time: TimeOfDay): Date {
            return atTime(time.hour, time.minute)
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
        HALLOWEEN(2013, Calendar.NOVEMBER, 1),
        CHRISTMAS_EVE(2013, Calendar.DECEMBER, 24),
        CHRISTMAS_DAY(2013, Calendar.DECEMBER, 25),
        BOXING_DAY(2013, Calendar.DECEMBER, 26),
        NEW_YEARS_EVE(2013, Calendar.DECEMBER, 31),
        //MAUNDY_THURSDAY_17(2017, Calendar.APRIL, 14),
        //GOOD_FRIDAY_17(2017, Calendar.APRIL, 15),
        //ASCENSION_EVE_17(2017, Calendar.MAY, 24),
        //ASCENSION_DAY_17(2017, Calendar.MAY, 25),
        SOME_SATURDAY(2017, Calendar.JUNE, 10),
        SOME_SUNDAY(2017, Calendar.JUNE, 11),
    }
}

// fix bugs
//     getTollFee() only work if all dates are same-day - otherwise they should not limit to 60
