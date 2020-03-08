![here we are](https://media.giphy.com/media/FnGJfc18tDDHy/giphy.gif)

# Toll fee calculator 1.0
A calculator for vehicle toll fees.

* Make sure you read these instructions carefully
* The current code base is in Java and C#, but please make sure that you do an implementation in a language **you feel comfortable** in like Javascript, Python, Assembler or [ModiScript](https://en.wikipedia.org/wiki/ModiScript) (please don't choose ModiScript). 
* No requirement but bonus points if you know what movie is in the gif

## Background
Our city has decided to implement toll fees in order to reduce traffic congestion during rush hours.
This is the current draft of requirements:
 
* Fees will differ between 8 SEK and 18 SEK, depending on the time of day 
* Rush-hour traffic will render the highest fee
* The maximum fee for one day is 60 SEK
* A vehicle should only be charged once an hour
  * In the case of multiple fees in the same hour period, the highest one applies.
* Some vehicle types are fee-free
* Weekends and holidays are fee-free

## Your assignment
The last city-developer quit recently, claiming that this solution is production-ready. 
You are now the new developer for our city - congratulations! 

Your job is to deliver the code and from now on, you are the responsible go-to-person for this solution. This is a solution you will have to put your name on. 

## Instructions
You can make any modifications or suggestions for modifications that you see fit. Fork this repository and deliver your results via a pull-request or send us an e-mail. You could also create a gist, for privacy reasons, and send us the link.

## Help I dont know C# or Java
No worries! We accept submissions in other languages as well, why not try it in Go or nodejs.


# Toll Free Calculator Implementation by Chinthaka
Improved version of the toll calculator is done in C# and can be found C#\TollCalculator folder as a solution.

This version supports injecting following service interfaces (Inject to TollCalculator main class)

* ITollFreeVehicles
* ITollFreeDates
* IDailyTollFees

pass null value to use the default implementation of any of the above services.

	ITollCalculator dailyTollCalculator = new TollCalculator(new CustomTollFreeVehicles(), null, null, null);
	
Also you can change the maximum fee per day by injecting maxPerDay value.

Using above interfaces you can change the functionality of toll rates, toll free dates , toll free vehicles and maximum fee per day.
	
TollFreeDates class can be initiated with different country codes to use holidays from different countries.

	ITollFreeDates tollFreeDates = new TollFreeDates(CountryCode.AU);
	var result = tollFreeDates.IsTollFreeDate(holiday);
	
If the user needs to add new vehicle types you can derive a subclass from VehicleType class and add additional vehicles.

	public class CustomVehicleType: VehicleType
	{
		public static readonly VehicleType Van = new CustomVehicleType("Van");
		public static readonly VehicleType Bus = new CustomVehicleType("Bus");

		public CustomVehicleType(string name) : base(name)
		{
		}
	}

## Finally if you want to see who is shouting hack the planet watch movie "HACKERS"