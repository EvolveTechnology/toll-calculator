package se.devies.tollcollector

class TimeOfDay(private val hour: Int, private val minute: Int) : Comparable<TimeOfDay> {
    override fun compareTo(other: TimeOfDay): Int {
        val hourDiff = hour - other.hour

        if (hourDiff == 0) {
            return minute - other.minute
        }

        return hourDiff
    }
}