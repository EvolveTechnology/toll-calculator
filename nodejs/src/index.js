import { toll, vehicles } from "../test/helpers"

const randomDate = (start, end = new Date()) =>
  new Date(start.getTime() + Math.random() * (end.getTime() - start.getTime()))

const dates = Array(15)
  .fill(0)
  .map(() => randomDate(new Date("2017-09-01")))

const vehicle = vehicles.DEFAULT
const fees = toll.getFee(vehicle, dates)
const output = `

Dates:
${dates.map(d => d.toUTCString().slice(0, 22)).join("\n")}

Total fee:
${fees}

`
console.log(output)
