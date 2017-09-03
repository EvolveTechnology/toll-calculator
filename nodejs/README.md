# TollCalculator

#### Installing
```bash
npm install
```

#### Run tests
```bash
npm test

```
##### watch-mode:
```bash
npm run test:watch
```

### Future improvements
#### Store toll entries on an append-only log for simple querying
```js
import isWithinRange from 'date-fns/is_within_range'

import Vehicle from "./Vehicle"

const vehicle = new Vehicle(spec)

const fees = vehicle.log
  .filter(entry => isWithinRange(entry.date, "2017-01-01", "2017-12-31"))
  .map(({ fee }) => fee)
  .reduce(sum, 0)
```
#### Events
```js
// store in db
toll.on("entry", db.save)

// notify law enforcement on watched vehicles
toll.on("entry", entry => {
  if (watchlist.includes(entry.vehicle.registration) {
    // call API
  }
})

// user API
toll.on("entry", entry => {
  const monthlyFees = vehicle.log
    .filter(entry => isWithinRange(entry.date, "2017-09-01", "2017-09-30"))
    .map(({ fee }) => fee)
    .reduce(sum, 0)

  if (monthlyFees > 5000) {
    // remind user to drive less
  }
})

// privately owned tolls
toll.on("entry", entry =>
  web3.eth.sendTransaction({
    from: users.get(entry.vehicle.owner).address,
    to: users.get(toll.owner).address,
    value: entry.fee,
  })
)
```
