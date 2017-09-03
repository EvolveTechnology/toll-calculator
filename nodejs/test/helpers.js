import * as t from "../src/types"
import { config } from "../src/config"
import TollCalculator from "../src/TollCalculator"
import Vehicle from "../src/Vehicle"

export const toll = new TollCalculator(config)
export const vehicles = {
  DEFAULT: new Vehicle({ type: t.VEHICLE.CAR }),
  FREE: new Vehicle({ type: t.VEHICLE.MILITARY }),
}
