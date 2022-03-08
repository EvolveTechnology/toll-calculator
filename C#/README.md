# Toll Fee Calculator 1.0

## Background

Our city has decided to implement toll fees in order to reduce traffic congestion during rush hours. This is the current draft of requirements:

- Fees will differ between 8 SEK and 18 SEK, depending on the time of day.
- Rush-hour traffic will render the highest fee.
- The maximum fee for one day is 60 SEK.
- A vehicle should only be charged once an hour. In the case of multiple fees in the same hour period, the highest one applies.
- Some vehicle types are fee-free.
- Weekends and holidays are fee-free.

## Implementation

Below changes are added to the base code to make sure all the requirements are covered.

1. Fix below logic errors.
- Add ```intervalStart = date``` to correctly handle multiple times per vehicle correctly.
- Add ```dates.OrderBy(d => d).ToList()``` to handle unordered toll time array calculation correctly.
- Create an extension method to calculate the time difference of each toll time to calculate amount for the time. It fixed the 8.30 to 14.59 time range amount calculation error.
- Check array length to avoid calculation errors in calculation logic.
2. Refactor the code for better readability and maintainability.
3. Create extension methods for common functions.
4. Add unit tests for testability.

There are two implementations in this coding assignment. Fixed logical errors and optimize the coding in **Logic Implementation** which can be find [here](Logic-Implementation) and micro service implementation in **API Implementation** which can be find [here](API-Implementation).
