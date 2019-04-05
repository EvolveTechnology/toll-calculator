package se.devies.tollcollector.holiday

import java.util.*

interface HolidayChecker {
    fun isTollFreeDate(date: GregorianCalendar): Boolean
}