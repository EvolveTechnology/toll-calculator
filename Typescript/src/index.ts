import { add, differenceInMinutes } from 'date-fns'
import isHoliday from './is-holiday'

export enum Vehicle {
  Car = 'Car',
  Bus = 'Bus',
  Emergency = 'Emergency',
  Diplomat = 'Diplomat',
  Foreign = 'Foreign',
  Military = 'Military',
  Motorbike = 'Motorbike',
  Tractor = 'Tractor',
  Truck = 'Truck',
  Trailer = 'Trailer',
}

/**
 * Check if vehicle is toll free
 * Based on https://www.transportstyrelsen.se/sv/vagtrafik/Trangselskatt/Undantag-fran-trangselskatt/
 *
 * @param {Vehicle} vehicle vehicle
 * @param {number} weight weight of the vehicle in kilograms
 * @returns {number} true if vehicle is toll free
 */
function isTollFreeVehicle(vehicle: Vehicle, weight: number): boolean {
  if (vehicle === Vehicle.Bus) return weight >= 1400

  return (
    vehicle === Vehicle.Emergency ||
    vehicle === Vehicle.Tractor ||
    vehicle === Vehicle.Motorbike ||
    vehicle === Vehicle.Diplomat ||
    vehicle === Vehicle.Foreign ||
    vehicle === Vehicle.Military
  )
}

/**
 * Check if a date is toll free.
 * Based on https://www.transportstyrelsen.se/sv/vagtrafik/Trangselskatt/Trangselskatt-i-goteborg/Tider-och-belopp-i-Goteborg/dagar-da-trangselskatt-inte-tas-ut-i-goteborg/
 *
 * @param {Date} date the date to date
 * @returns {boolean} true if the date is toll free
 */
function isTollFreeDate(date: Date): boolean {
  const month = date.getMonth()
  const weekday = date.getDay()

  // Check if month is July or weekday is Saturday or Sunday
  if (month === 6 || weekday === 0 || weekday === 6) return true

  const tomorrow = add(date, { days: 1 })
  return isHoliday(date) || isHoliday(tomorrow)
}

/**
 * Get toll fee in SEK based on date
 * List from https://www.transportstyrelsen.se/sv/vagtrafik/Trangselskatt/Trangselskatt-i-goteborg/Tider-och-belopp-i-Goteborg/
 *
 * @param {date} date the date to check
 * @returns {number} toll fee in SEK
 */
function getTollFee(date: Date): number {
  const hour = date.getHours()
  const minutes = date.getMinutes()

  if (hour === 6) return minutes <= 29 ? 9 : 16
  if (hour === 7) return 22
  if (hour === 8 && minutes <= 29) return 16
  if (hour >= 8 && hour <= 14) return 9
  if (hour === 15 && minutes <= 29) return 16
  if (hour >= 15 && hour <= 16) return 22
  if (hour === 17) return 16
  if (hour === 18 && minutes <= 29) return 9

  return 0
}

/**
 * Get the total toll fee for a vehicle, the vehicles weight and array of dates
 *
 * @param {VehicleType} vehicle a vehicle
 * @param {number} weight weight of the vehicle in kilograms
 * @param {Date[]} dates an array of dates
 * @returns {number} total toll fee for dates and vehicles
 */
function getTotalTollFee(
  vehicle: Vehicle,
  weight: number,
  dates: Date[],
): number {
  if (
    isTollFreeVehicle(vehicle, weight) ||
    dates.some(date => isTollFreeDate(date))
  )
    return 0

  let intervalStart
  let totalFee = 0

  for (const date of dates) {
    if (!intervalStart) intervalStart = date
    const nextFee = getTollFee(date)
    const tempFee = getTollFee(intervalStart)

    if (differenceInMinutes(date, intervalStart) <= 60) {
      if (totalFee > 0) totalFee -= tempFee
      totalFee += nextFee >= tempFee ? nextFee : tempFee
    } else {
      totalFee += nextFee
      intervalStart = date
    }
  }

  return totalFee > 60 ? 60 : totalFee
}

export default getTotalTollFee
