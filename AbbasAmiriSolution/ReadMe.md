## Abbas Amiri solution for *Toll Fee Calculator*

The project has six requirements:

- Fees will differ between 8 SEK and 18 SEK, depending on the time of day
- Rush-hour traffic will render the highest fee
- The maximum fee for one day is 60 SEK
- A vehicle should only be charged once an hour
  - In the case of multiple fees in the same hour period, the highest one applies.
- Some vehicle types are fee-free
- Weekends and holidays are fee-free

Based on the requirements, I decided to implement four policy classes to handle the following requirements:

`HolidayPolicy` for [Holidays are fee-free].

`IsWeekend` for [Weekends are fee-free].

`VehicleTypePolicy` for [Some vehicle types are fee-free].

`DayTimePolicy` for [Fees will differ between 8 SEK and 18 SEK, depending on the time of day] and [Rush-hour traffic will render the highest fee].

The idea of the design comes from the idea behind the Strategy design pattern. The design also ensures that [Single-responsibility principle]([Single-responsibility principle - Wikipedia](https://en.wikipedia.org/wiki/Single-responsibility_principle)) is applied.

The `TollFeeCalculator` is responsible for calculating toll-fee. It applies all policies and also <u>the maximum fee for one day is 60 SEK</u> and <u>in the case of multiple fees in the same hour period, the highest one applies</u> requirements.

The `TollFeeCalculator` has one public method: `CalculateTollFee`. It takes traffic times and vehicle type to calculate toll fee. After applying some [Guard Clauses](https://deviq.com/design-patterns/guard-clause) to ensure that incoming parameters are correct; it applies the policies and goes through traffic times to do its job. The method used [Lambda]([Lambda expressions - C# reference | Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions)) to iterate through traffic times. The complexity in the method is **O(n)** which I believe is acceptable.

The original implementation used a class for `Vehicle`. I haven't seen any reason to implement `Vehicle` class because the only property we need in the scenario is the vehicle type that is implemented via an `Enum`.

The time-table fees are implemented by `DayTimeFee` and `DayTimeFeeCollectionBuilder` classes. The timetable is a collection of values that can be loaded from a JSON file or code.

Holidays and Constants can also be loaded from a JSON file or code.

The `DayTimeFeeCollectionBuilder` and `HolidayCollectionBuilder` are responsible for loading data.

To prevent unnecessary complexity by using `DateTime` object in C#, I used `DateOnly` and `TimeOnly` objects that make the code more understandable.

The unit-test project is created to check the validity of objects and processes.








