import * as t from "../src/types"

import { vehicles } from "./helpers"

const vehicle = vehicles.DEFAULT
test("it has an id", () => {
  expect(vehicle.id).toBeDefined()
})

test("it has a type", () => {
  expect(vehicle.type).toEqual(t.VEHICLE.CAR)
})
