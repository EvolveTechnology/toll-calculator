package se.devies.tollcollector.vehicle

class Military : Vehicle {
    override fun isTollFree(): Boolean {
        return true
    }
}