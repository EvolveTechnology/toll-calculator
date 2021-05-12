import Vehicle from './Vehicle.mjs';

export default class Emergency extends Vehicle {
    constructor() {
        super();
    }

    get type() {
        return 'Emergency';
    }
}