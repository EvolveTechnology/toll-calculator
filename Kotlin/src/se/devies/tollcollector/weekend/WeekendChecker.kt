package se.devies.tollcollector.weekend

import java.util.*

class WeekendChecker {
    fun isWeekend(date: GregorianCalendar): Boolean {
        val dayOfWeek = date.get(Calendar.DAY_OF_WEEK)

        return dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY
    }

}