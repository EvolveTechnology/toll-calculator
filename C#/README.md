# Toll fee calculator 1.0 (.NET Standard 2.0 version)
A calculator for vehicle toll fees.

## Assignment
The solution provided here is an interpretation of the assignment to create a toll fee calculator.
The following assumptions and interpretations were used as requirements:

### Flexibility
There is a potential for the toll fee calculator to be used in several cities, and even several countries.
Therefore the rule set has to be somewhat flexible.

### Toll rules change over time
An assumption is that the toll rules will change over time. The solution provides rules for
Gothenburg that correspond to the historical rules over time (from 2013 onwards).

### Provide invoice data
Another assumption is that the output of the calculator is the basis of a (monthly) invoice,
similar to the one that the Swedish Transportstyrelsen sends out, containing:

* Vehicle
* Daily list of passages, total amount and taxable amount (to actually be paid)
* Total taxable amount

### Component, not application
Another assumption is that the toll calculator is a component in a larger system, service or tool.
I.e. it is not an application with a user interface and so on. A console test client is provided.

## Solution
The solution is implemented in .NET Standard 2.0 and a test client and unit tests in .NET Core 2.0.

### Pre-requisites
In order to build the solution Visual Studio 2017 (version 15.3.x) is needed, with .NET Core 2.0 SDK,
see here: https://www.microsoft.com/net/download/core

### Portability
By using .NET Standard 2.0 as platform, systems all over the .NET ecosystem can use the component.
See here: https://github.com/dotnet/standard/blob/master/docs/versions.md