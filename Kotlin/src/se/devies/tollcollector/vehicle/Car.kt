package se.devies.tollcollector.vehicle

class Car : Vehicle {
    override fun isTollFree(): Boolean {
        return false
    }
}