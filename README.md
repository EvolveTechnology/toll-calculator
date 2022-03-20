![here we are](https://media.giphy.com/media/FnGJfc18tDDHy/giphy.gif)

# Toll fee calculator 1.0
A calculator for vehicle toll fees.

* Make sure you read these instructions carefully
* The current code base is in Java and C#, but please make sure that you do an implementation in a language **you feel comfortable** in like Javascript, Python, Assembler or [ModiScript](https://en.wikipedia.org/wiki/ModiScript) (please don't choose ModiScript). 
* No requirement but bonus points if you know what movie is in the gif
* Hackers - 1995 

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

## Changes
Radical changes introduced based on the maintainability of the code. Each of the changes are listed below.
1. High level folders of Java and C# code removed.
2. Maven build tool introduced to project folder structure change accordingly.
3. Package structure of classes also changed accordint to the Maven folder structure
4. Unit testing with mocking introduced
5. With the unit test implementation found few problems in the getTollFee method.
	- This method double charge user when he use highway even within 60 minutes
	- Input dates field assume all the passes coming in sorted order
	- Also there is no validation whether passes are within same day or not
 6. Optimized algorithm not to calculate toll when charge exceeds 60 SEK
 
 NOTE: This code can be a part of REST API. But not completed that part since it is not that clear to me. 
 
 ## To Run
 Install maven3.8.4 to run unit test cases
 mvn clean package 
  


## Help I dont know C# or Java
No worries! We accept submissions in other languages as well, why not try it in Go or nodejs.

