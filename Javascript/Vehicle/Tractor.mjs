import Vehicle from './Vehicle.mjs';

export default class Tractor extends Vehicle {
    constructor() {
        super();
    }

    get type() {
        return 'Tractor';
    }
}