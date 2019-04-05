package se.devies.tollcollector.vehicle

class Foreign : Vehicle {
    override fun isTollFree(): Boolean {
        return true
    }
}