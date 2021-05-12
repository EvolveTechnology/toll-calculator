import Vehicle from './Vehicle.mjs';

export default class Motorbike extends Vehicle {
    constructor() {
        super();
    }

    get type() {
        return 'Motorbike';
    }
}