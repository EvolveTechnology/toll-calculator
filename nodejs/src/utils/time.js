import format from "date-fns/format"

export const isToday = d => d.toDateString() === new Date().toDateString()
export const toDate = date => format(date, "YYYY-MM-DD")
export const toTime = date => format(date, "HHmm")
export const toHour = time => time.slice(0, 2)

// Is datetime within time tuple - eg: ["0000", "0100"]
export const withinTime = date => ([hours]) =>
  toTime(date) >= hours[0] && toTime(date) <= hours[1]

// Check if hour is same as previous - eg: 11 === 11
export const sameHour = (x, i, arr) =>
  toHour(toTime(x)) === toHour(toTime(arr[i - 1]))

// Check if date matches wildcards - eg. xxxx-12-24
export const matchesDate = date => free =>
  toDate(date).includes(free.replace(/x/gi, ""))
