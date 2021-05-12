import Vehicle from './Vehicle.mjs';

export default class Foreign extends Vehicle {
    constructor() {
        super();
    }

    get type() {
        return 'Foreign';
    }
}