package se.devies.tollcollector.holiday

import java.util.*

class MockHolidayChecker: HolidayChecker {
    override fun isTollFreeDate(date: GregorianCalendar): Boolean {
        if (date.get(Calendar.MONTH) == 5 && date.get(Calendar.DAY_OF_MONTH) == 6) {
            return true
        } else if (date.get(Calendar.MONTH) == 4 && date.get(Calendar.DAY_OF_MONTH) == 1){
            return true
        }

        return false
    }

}