# Toll fee calculator 1.0 (.NET Framework 4.6.1)
A calculator for vehicle toll fees.

## Assignment
The solution provided here is the intepretation of the coding assignment.

## Implementation
### Country specific rules
Some rules, such as the types of vehicles which are toll free and toll free days, are common for the whole country. These rules are implemented in a class which is then derived by the city specific implementations.

### City specific rules
Rules for the time and fees, as well as if several passages within X amount of time, are specific for the cities and implemented in corresponding classes. The solution currently implements the toll calculator for Gothenburg and Stockholm.

## Dependencies
* Svenska Helgdagar API (http://api.dryg.net/dagar/v1/?datum=ÅÅÅÅMMDD) - used to determine whether a date is a public holiday
* Newtonsoft.Json v6.0.4
* Microsoft.AspNet.WebApi.Client v5.2.0

## Environment
The solution is run on Visual Studio 2017. There is no presentation project in the solution (console or web app) but one can run tests. Edit tests to test the implementation with different parameters.

## Potential improvements
There is a huge potential for improvements:
* Currently all rules and fees are hardcoded. One could instead store them in a database. For example, store the fees for different time intervals in a table and retrieve them when needed;
* Use API's for retrieval of rules (possibly even retrieve the total fee directly from some API);
* The current implementation applies only the current rules and fees. One could introduce even the historical rules and fees.