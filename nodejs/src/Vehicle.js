import nanoid from "nanoid"

class Vehicle {
  constructor({ type }) {
    this.id = nanoid()
    this.type = type
  }
}

export default Vehicle
