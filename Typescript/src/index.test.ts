import getTotalTollFee, { Vehicle } from '.'

const testCases = [
  {
    vehicle: Vehicle.Car,
    weight: 1000,
    dates: [
      new Date(2021, 0, 12, 6, 15, 0),
      new Date(2021, 0, 12, 6, 30, 0),
      new Date(2021, 0, 12, 7, 45, 0),
      new Date(2021, 0, 12, 7, 48, 0),
      new Date(2021, 0, 12, 0, 0, 0),
    ],
    expected: 38,
  },
  {
    vehicle: Vehicle.Car,
    weight: 1000,
    dates: [new Date(2021, 0, 12, 8, 25, 0)],
    expected: 16,
  },
  {
    vehicle: Vehicle.Car,
    weight: 1000,
    dates: [new Date(2021, 0, 12)],
    expected: 0,
  },
  {
    vehicle: Vehicle.Military,
    weight: 1000,
    dates: [new Date(2021, 0, 12, 6, 15, 0)],
    expected: 0,
  },
  {
    vehicle: Vehicle.Bus,
    weight: 1100,
    dates: [new Date(2021, 0, 12, 8, 25, 0)],
    expected: 16,
  },
  {
    vehicle: Vehicle.Bus,
    weight: 1500,
    dates: [new Date(2021, 0, 12, 8, 25, 0)],
    expected: 0,
  },
]

describe('getTotalTollFee', () => {
  test.each(testCases)(
    'should return $expected when $weight and dates are passed',
    ({ vehicle, weight, dates, expected }) =>
      expect(getTotalTollFee(vehicle, weight, dates)).toEqual(expected),
  )
})
