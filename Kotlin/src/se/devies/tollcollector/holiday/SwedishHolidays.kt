package se.devies.tollcollector.holiday

import java.util.*

class SwedishHolidays: HolidayChecker {
    override fun isTollFreeDate(date: GregorianCalendar): Boolean {
        // TODO: use some API or whatever to check holidays. I didn't have time to implement this
        return false
    }

}