# Toll fee calculator 1.0
A calculator for vehicle toll fees.

## Assignment Implementation - Joachim Ekstrand
Since there is no description of a user interface I chose to only create some unit tests to show that the
business logic works. Which fees that should be used at which times are also not specified among the requirements,
so I kept the existing "rules" for this and just fixed some bugs.

### Implementation Restrictions
All projects needs boundaries. Here I avoided:
* A total re-implementation of the business logic. There are quite a few "better" ways to implement this. Which way is
best depends much on surrounding circumstances so this needs to be discussed with people involved in the project.
* Adding more years than 2013 for looking up public holidays and weekends to look for fee-free days. This made no sense
to me since in a "real" implementation one would probably prefer using an external calender for this so it will work
automatically, and not every year needs to be added manually.
* Testing every possible use case. OK, this is probably almost impossible, but it could be a good idea in this case to
have a very rigorous set of test data to be run at least before every release. I created only unit-tests.
Unit tests are, at least in my opinion, only sanity tests. They should run fast and discover the most obvious faults.
To try and make unit tests covering "all possible scenarios" is very difficult and a very bad idea in my experience.

### Possible improvements
* Using rules instead of hard-coded time/fee conditions. When fees, and perhaps fee time interval, changes it is a good
idea to avoid having to re-implement the fee calculation logic.
* Figure out how to handle when passing a checkpoint just before 12 am and then right after 12 am. Right now the driver
will get charged for both - even if those times are within an hour. As there are no fees at night this is not a problem
at the moment, but if we think we will need the possibility to have fees at night we also need to consider how to handle
maximum daily fee when it intersects with maximum hourly fee.
* Use only immutable variables and better coding-style. Using close-to-functional code style is almost always a good
idea. I have not been working with Java for many, many years so there is a lot of "new" things, best practices etc.
that would probably make this code much more modern. I also have not used any explicit coding-style, even though I
think that is very important in a project that is expected to live for a while. I am not even sure if my IDE (NetBeans)
cleans up trailing white-spaces correctly, which is kid of embarrassing.

### Changes in business logic
The code had some confusing, unnecessary and sometimes incorrect code. I fixed the bugs and removed some unnecessary
code from the original business logic I am still using. I thought that it is best to have only one public method in
TollCalculator that takes a Vehicle and a list of dates with all the times the vehicle passes a checkpoint, no matter
the range of the dates list, so I made all other methods private.
