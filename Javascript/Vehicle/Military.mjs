import Vehicle from './Vehicle.mjs';

export default class Military extends Vehicle {
    constructor() {
        super();
    }

    get type() {
        return 'Military';
    }
}