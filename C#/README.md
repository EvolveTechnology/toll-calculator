## Toll Calculator API

A .NET Core WebAPI for calculating toll fees

Currently hosts one endpoint `/api/total-fee` which takes a vehicletype and an array of DateTimes and returns the total fee for all those passages.

### Key features

Since I took over the solution from the last city-developer, some new key features as been introduced.

* Automated tests in the form of UnitTests have been introduced, increasing the reliability of codechanges.
* Any variable used in the core logic, eg. maximum daily fee or the fee of each time slot, have all been extracted to be read from either config or an external database(config in this case)
* We can now handle multiple dates sent into our calculator, while the original calculator left to me by the previous developer would return the maximum daily fee if a great number of passages over multiple days were to be requested.

### Solution shortcomings

There exists a few shortcomings with this solutions still that could be worked on.

* Calculating the public holidays is being outsourced to a 3rd pary in the form of a nuget [Nager.Date](https://www.nuget.org/packages/Nager.Date/). This was done to be able to focus more time and resources on the core logic and is something that can be developed further. For example we could develop a solution where we read info about public holidays from an external database and a seperate function like an Azure Function that runs once a year and extracts the upcoming years public holidays from a public API like [Nager API](https://date.nager.at/Api) or scrapes them from something like [kalender.se](https://www.kalender.se/helgdagar).
* Enum conversion from api model to domain model. If the domain enum is modified, the api model enum will have to be modified accordingly. Although I have a test that catches errors due to this, it still requires the developer to modify the code in 2 places.
* The WebAPI is currently completely unauthenticated.
* No logging currently exists.

### Further development

If I were to be charged with developing this solution further this is a list of possible next steps:

* **Add logging**, my personal preference is Serilog. Either a very simple logging solution where we just save logs to a file, but my personal preference for simple projects is to add a slack sink to Serilog, with seperate info/warning/error channels that can be easily monitored by enabling notifications for the error channel for example.
* **Add authentication** to the WebAPI. Simple solution would be a simple apikey stored in config, a better solution would be to use an external authority, perhaps a simple IdentityServer4 solution.
* **Add an external database** and implement logic to fetch all info currently residing in config/repository classes from the database instead. Possibly a rudimentary admin portal where this configuration can be changed by enterprise personell without developer interferance.

## Devops
[![Build status](https://dev.azure.com/redlerops/toll-calculator/_apis/build/status/Toll%20Calculator%20Build)](https://dev.azure.com/redlerops/toll-calculator/_build/latest?definitionId=2)
While the codebase resides on GitHub I am currently building and deploying the solution from [Azure Devops](https://dev.azure.com/redlerops/toll-calculator/_build), deployment is done to an [Azure Web App](https://toll-calculator.azurewebsites.net).

  

## How to Test?

#### Automated tests
All automated tests are in this solution, they are also ran every time a build pipeline is started.

#### Local environment
Test the WebAPI locally easy through the set up swagger page. The app routes to the swagger page on the root path. Eg. `https://localhost:44390/`

#### Cloud environment
Test the WebAPI against an Azure hosted environment by going to [https://toll-calculator.azurewebsites.net](https://toll-calculator.azurewebsites.net)