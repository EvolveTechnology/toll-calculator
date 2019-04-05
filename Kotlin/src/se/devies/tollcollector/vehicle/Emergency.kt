package se.devies.tollcollector.vehicle

class Emergency : Vehicle {
    override fun isTollFree(): Boolean {
        return true
    }
}