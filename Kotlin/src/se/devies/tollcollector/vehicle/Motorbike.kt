package se.devies.tollcollector.vehicle

class Motorbike : Vehicle {
    override fun isTollFree(): Boolean {
        return true
    }
}