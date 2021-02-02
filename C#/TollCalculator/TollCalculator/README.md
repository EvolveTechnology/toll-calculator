![here we are](https://media.giphy.com/media/FnGJfc18tDDHy/giphy.gif)

# Toll fee calculator 1.0
A calculator for vehicle toll fees for a Toll Pass or for all the passes for the entire day

## Functionality
 
* Fees will differ between 8 SEK and 18 SEK, depending on the time of day 
* Rush-hour traffic will render the highest fee
* The maximum fee for one day is 60 SEK
* A vehicle should only be charged once an hour
  * In the case of multiple fees in the same hour period, the highest one applies.
* Some vehicle types are fee-free
* Weekends and holidays are fee-free

##Usage and technical details
Can be used as a MVC app or as an API
Uses MongoDb to store and retrieve the Toll Passes which can be leveraged to be deployed on the cloud
The TollCalculator class that calculates the Toll Fees could be refactored.
Partial views can be used instead of a separate view to display the Fees.

