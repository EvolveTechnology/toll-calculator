import Vehicle from './Vehicle.mjs';

export default class Diplomat extends Vehicle {
    constructor() {
        super();
    }

    get type() {
        return 'Diplomat';
    }
}