package se.devies.tollcollector
import se.devies.tollcollector.holiday.HolidayChecker
import se.devies.tollcollector.vehicle.Vehicle
import se.devies.tollcollector.weekend.WeekendChecker
import java.lang.Math.*
import java.util.*

// We inject a HolidayChecker so that we can use a mock implementation in our tests
// Potentially, we could also inject the different fees and rules if we want to reuse our solution in other countries
class TollCalculator(private val holidayChecker: HolidayChecker) {

    // I took the liberty of using the current prices as of 2019-04-05
    private val RUSH_HOUR_FEE = 22
    private val NORMAL_FEE = 16
    private val LOW_HOUR_FEE = 9
    private val NO_FEE = 0

    private val MAXIMUM_FEE = 60

    // Times when the price changes and the new price from that time
    private val priceChangeTimetable: HashMap<TimeOfDay, Int> = hashMapOf(
        TimeOfDay(0, 0) to NO_FEE,
        TimeOfDay(6, 0) to LOW_HOUR_FEE,
        TimeOfDay(6, 30) to NORMAL_FEE,
        TimeOfDay(7, 0) to RUSH_HOUR_FEE,
        TimeOfDay(8, 0) to NORMAL_FEE,
        TimeOfDay(8, 30) to LOW_HOUR_FEE,
        TimeOfDay(15, 0) to NORMAL_FEE,
        TimeOfDay(15, 30) to RUSH_HOUR_FEE,
        TimeOfDay(17, 0) to NORMAL_FEE,
        TimeOfDay(18, 0) to LOW_HOUR_FEE,
        TimeOfDay(18, 30) to NO_FEE
    )

    private val weekendChecker = WeekendChecker()

    // Note that I have changed the method signature, under the assumption that
    // I as responsible for the city's toll calculator can make such decisions freely
    // In case we need to keep the old signature, we could just have that one as a proxy function
    fun getTotalTollFeeForOneDay(vehicle: Vehicle, vararg dates: GregorianCalendar): Int {
        if (!allOnTheSameDay(dates)) {
            throw IllegalArgumentException("All dates need to be on the same day")
        }

        if (vehicle.isTollFree()) {
            return 0
        }

        // Filter out passages before morning toll starts,
        // I assume that these do not count as passages at all
        val filteredList = dates.filter { date -> getTollFee(date) != 0 }.toMutableList()

        if (filteredList.count() == 0) {
            return 0
        }

        if (isTollFreeDate(dates[0])) {
            return 0
        }

        filteredList.sort()

        var totalFee = 0

        var firstPassageInCurrentTimeframe = filteredList.removeAt(0)
        totalFee += getTollFee(firstPassageInCurrentTimeframe)

        for (date in filteredList) {
            if (isMoreThanOneHourApart(date, firstPassageInCurrentTimeframe)) {
                // The requirements were a bit unclear, so I have implemented the simplest solution:
                // You only pay for the first passage in each 1 hour timeframe, as opposed to the most expensive
                totalFee += getTollFee(date)
                firstPassageInCurrentTimeframe = date
            }
        }

        return min(totalFee, MAXIMUM_FEE)
    }

    private fun allOnTheSameDay(dates: Array<out GregorianCalendar>): Boolean {
        if (dates.count() == 0) {
            return true
        }

        val day = dates[0]

        for (date in dates) {
            if (day.get(Calendar.YEAR) != date.get(Calendar.YEAR)
                || day.get(Calendar.MONTH) != date.get(Calendar.MONTH)
                || day.get(Calendar.DAY_OF_MONTH) != date.get(Calendar.DAY_OF_MONTH)) {
                return false
            }
        }

        return true
    }

    private fun isTollFreeDate(date: GregorianCalendar): Boolean {
        return holidayChecker.isTollFreeDate(date) || weekendChecker.isWeekend(date)
    }

    private fun getTollFee(date: GregorianCalendar): Int {
        val hour = date.get(Calendar.HOUR_OF_DAY)
        val minute = date.get(Calendar.MINUTE)
        return getTollFeeForTime(hour, minute)
    }

    private fun getTollFeeForTime(hour: Int, minute: Int): Int {
        val time = TimeOfDay(hour, minute)

        // Loop through timetable in reverse order
        // (becuase it makes it possible to have a more human-readable timetable)
        priceChangeTimetable.toSortedMap(reverseOrder()).forEach { (priceChangeTime, price) ->
            if (time >= priceChangeTime) {
                return price
            }
        }

        // Default fallback. For a real-life application we can put an Assertion to make sure we never end up here
        return NO_FEE
    }

    private fun isMoreThanOneHourApart(date: GregorianCalendar, otherDate: GregorianCalendar): Boolean {
        return abs(date.timeInMillis - otherDate.timeInMillis) > 1000 * 60 * 60
    }
}
