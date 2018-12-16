# Toll fee calculator 1.0

## <a id="background"></a>Background

Our city has decided to implement toll fees in order to reduce traffic congestion during rush hours.
This is the current draft of requirements:

- Fees will differ between 8 SEK and 18 SEK, depending on the time of day
- Rush-hour traffic will render the highest fee
- The maximum fee for one day is 60 SEK
- A vehicle should only be charged once an hour
- Some vehicle types are fee-free
- Weekends and holidays are fee-free

## Your assignment

The last city-developer quit recently, claiming that this solution is production-ready.
You are now the new developer for our city - congratulations!

Your job is to deliver the code and from now on, you are the responsible go-to-person for this solution. This is a solution you will have to put your name on.

## Instructions

You can make any modifications or suggestions for modifications that you see fit. Fork this repository and deliver your results via a pull-request or send us an e-mail. You could also create a gist, for privacy reasons, and send us the link.

## Solution

### Demo

Available [here.](https://nice-sky.surge.sh/)

- You may want to search for `SDG-560`, or `QNX-473`.

- Otherwise navigate to the [dashboard](https://nice-sky.surge.sh/dashboard) and fetch all available vehicles.

### Asumptions

I've made a few asumptions.

1. There is a system which collects, or rather, logs the data of a vehicle passing through a tolling point.
2. The data is taken local time.
3. This system keeps updates a database, from which all vehicles that went through a tolling point can be collected.
4. For the holidays I use a third party API. In practice, we should have our own defined holidays.

### Understanding the application

The proposed solution breaks the assignment into two pieces.

    - A back end to process the data
    - A front end to interact with data

The `data` in this context refers to a collection of vehicles all of which have passes through a tolling point in the city.

> The tech stack of each piece is explained further in their own read me.

### Back End

The back end is further broken in two. First, an express webtask (AKA a lambda, FaaS, etc.), and the actual functions which calculate the tolling fee.

The express webtask, acts as a gateway, by exposing two end points, `/all`/ and `/vehicle`, which respond to `POST` requests. The webtask is in `backend/index.js`.

Second, the functions which calculate the tolling fee. These are applications of the `byDayFeeAccumulator` function.

1. `byVehicleCalculator`

   - Gets All vehicles and filters the required one.
   - Gets holidays for the years included in the vehicle data.
   - Group the dates array by day using `groupByDay`.
   - Applies `byDayFeeAccumulator` to a single vehicle.

2. `allVehiclesCalculator`

   - Gets All vehicles.
   - Gets holidays for the years included in the vehicles data.
     - One 'GET' request for each year.
   - Applies `groupByDay` and `byDayFeeAccumulator` to the collection of vehicles.

The `byVehicleCalculator` and `allVehiclesCalculator` functions are to be found in `backend/src/index.js`.

The vehicles for both cases, contain a `dates` array, which is a log of the toll-passes done by the vehicle.

The `byDayFeeAccumulator` takes an object, which may have fees accumulated for other days, and calculates the fee for the target `day`, by running `tollCalculator` against the toll-passes done by the vehicle that `day`.

The `tollCalculator` function calculates the cost of each pass, adds up the total for the day, takes care of free fares and maximum for the day.

It does this by relying in supporting functions, such as `dailyFeeAccumulator`, `feeByTimeOfDay`, `tollFreeVehicles`, `tollFreeDays` and `intervalMarker`.

These support functions impose the special business conditions required by the city [administration](#background).

Then, `byDayFeeAccumulator` when ran against all days that a vehicle passes through tolls, returns fees object carrying the passes and the total fee for each day, in addition to the rest of vehicle meta data.

```javascript
// input
const vehicle = {
  id: "063ad323-8c82-40e6-b509-af8f99c47324",
  type: "Truck",
  regNum: "QNX-473",
  dates: ["2018-11-15 17:51:37"]
};

//output
const vehicleWithFees = {
  fees: {
    "2018-11-15": {
      chargeablePasses: 1,
      isHoliday: false,
      isSaturday: false,
      isSunday: false,
      isTollFreeVehicle: false,
      passes: ["2018-11-15 17:51:37"],
      totalFee: 13,
      totalPasses: 1
    }
  },
  dates: ["2018-11-15 17:51:37"],
  id: "063ad323-8c82-40e6-b509-af8f99c47324",
  regNum: "QNX-473",
  type: "Truck"
};
```

#### References

> The `byDayFeeAccumulator` function is to be found in `backend/src/services/byDayFeeAccumulator/index.js`.

> The `tollCalculator` function is to be found in `backend/src/services/tollCalculator/index.js`.

> The `groupByDay` function is to be found in `backend/src/services/groupByDay/index.js`.

> The `dailyFeeAccumulator` function is to be found in `backend/src/services/dailyFeeAccumulator/index.js`.

> The `feeByTimeOfDay` function is to be found in `backend/src/services/feeByTimeOfDay/index.js`.

> The `tollFreeVehicles` function is to be found in `backend/src/services/tollFreeVehicles/index.js`.

> The `tollFreeDays` function is to be found in `backend/src/services/tollFreeDays/index.js`.

> The `intervalMarker` function is to be found in `backend/src/service/intervalMarker/index.js`.

### Front End

In the front end application one can:

1. Play the role of a citizen in the home `/` path.

   - Citizens can query a vehicle registration number and see daily charges.

2. Play the role of an Admin in the `/dashboard` path.

   - Admins can query the whole database to see vehicles with debt.

   - Admins can also sort or filter by type.

   - Additionally they can search for a registration number or click on a vehicle to see more detail.

The back end responds with the a `fees` object attached to every vehicle, whether it is one vehicle or an array of vehicles.

This `fees` object contains the `key-values` pairs, where the key is the `date` and the values are:

- chargeablePasses
- isHoliday
- isSaturday
- isSunday
- isTollFreeVehicle
- passes
- totalFee
- totalPasses

These allow the front end to aggregate data further and have a total for the vehicle, which adds all of the fees for all days, and also allows the front end to show the reason why certain days may be charged with zero SEK, that is, holidays, Saturday, Sunday, or simply out of chargeable hours. Furthermore the front end shows if a vehicle is toll free.

When looking at a single vehicle, either in home `/` or dashboard `/dashboard`, one can see a summary for the vehicle, and a circular progress bar for every day, indicating how much of the maximum 60 SEK has been covered that day.

The front end is also able to manage network failures, offline situations and empty queries in a user-friendly manner.

## Test coverage

### Back end

| File      | % Stmts | % Branch | % Funcs | % Lines | Uncovered Line #s |
| --------- | ------- | -------- | ------- | ------- | ----------------- |
| All files | 100     | 100      | 100     | 100     |                   |

To generate a report:

```bash
cd backend && yarn coverage
```

### Front end

| File      | % Stmts | % Branch | % Funcs | % Lines | Uncovered Line #s |
| --------- | ------- | -------- | ------- | ------- | ----------------- |
| All files | 100     | 100      | 100     | 100     |                   |

To generate a report:

```bash
cd frontend && yarn test --coverage
```

> The api mock is not tested, because it is a simple express app.

### Maintenance

Keeping the application up to date is really simple.

As as administrator perhaps you would like to change the price scheme. For that you just modify, the fees files, located at `backend/src/services/feeByTimeOfDay/fees.js`.

```javascript
export default [
  { range: makeRange(6)(6, 29), fee: LOW },
  { range: makeRange(6, 30)(6, 59), fee: MEDIUM },
  { range: makeRange(7)(7, 59), fee: HIGH },
  { range: makeRange(8)(8, 29), fee: MEDIUM },
  { range: makeRange(8, 30)(14, 59), fee: LOW },
  { range: makeRange(15)(15, 29), fee: MEDIUM },
  { range: makeRange(15, 30)(16, 29), fee: HIGH },
  { range: makeRange(17)(17, 59), fee: MEDIUM },
  { range: makeRange(18)(18, 29), fee: LOW }
];
```

As a developer that should be very easy!

Or if you'd like to change the maximum fee, just edit the constant `MAX_FEE` in `backend/src/services/constants/index.js`. There you can also edit the `LOW`, `MEDIUM` and `HIGH` price levels.

## Running the solution locally

You'll need:

- [Yarn](https://yarnpkg.com/lang/en/docs/install/#debian-stable)
- [wt-cli](https://webtask.io/docs/wt-cli)

The solution uses nodeJS, and was build under version `11.2.0`, but should work with anything above `8.0.0`.

1. From the root, install dependencies.

```bash
yarn install-all
```

2. To run the application locally you need to start three components. Luckily, we do that with just one command.

```bash
yarn start-all
```

3. You'll be taken to your favorite web browser opened at localhost:3000/

4. Try searching for `SDG-560`, that vehicle sure has a lot of debt.

#### What just happened?

You installed `node_modules` in all sub-parts of the project, and then:

- An api-mock ran in port 9191.
- The backend solution ran in port 1337.
- The frontend solution ran in port 3000.

The api-mock is used to avoid requesting data from live endpoints and avoid sharing api keys, which may end up causing monetary costs to my self. These endpoints and api keys are hidden in a `.secrets` file not included in the project, which is why **if you want to run it locally, you must follow the steps above.**
