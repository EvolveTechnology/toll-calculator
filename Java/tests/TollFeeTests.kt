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
                val calendar = GregorianCalendar(2017, 3, 3, it.key.hour, it.key.minute)
                val fee = calculator.getTollFee(normalCar, calendar.time)
                assertEquals(it.value, fee)
            }
        }
    }

    private enum class TimeOfDay(internal val hour: Int, internal val minute: Int) {
        MIDNIGHT(0, 0), SIX_AM(6, 5), SEVEN_AM(7, 5), EIGHT_AM(8, 5),
        NOON(12, 0), THREE_PM(15, 5), FOUR_PM(16, 5), FIVE_PM(17, 0),
        SIX_PM(18, 5)
    }
}
