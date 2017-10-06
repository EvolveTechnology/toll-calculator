export const assert = console.assert
export const sum = (a, b) => a + b
export const not = fn => (...args) => !fn(...args)
export const clamp = (val, max, min = 0) => Math.min(Math.max(min, val), max)
export const includes = (v, list) => list.includes(v)
export const values = obj => Object.keys(obj).map(key => obj[key])

export const groupBy = (fn, arr) =>
  arr.reduce((xs, d) => {
    const key = fn(d)
    return {
      ...xs,
      [fn(d)]: (xs[fn(d)] || []).concat(d),
    }
  }, {})
