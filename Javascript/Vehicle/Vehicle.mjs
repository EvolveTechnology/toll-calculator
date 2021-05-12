export default class Vehicle {
    constructor() {
        if (new.target === Vehicle) {
            throw new TypeError('Cannot construct Vehicle instances directly');
        }
    }
}