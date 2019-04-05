package se.devies.tollcollector.vehicle

class Tractor : Vehicle {
    override fun isTollFree(): Boolean {
        return true
    }
}