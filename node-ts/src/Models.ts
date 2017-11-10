export enum VehicleType {
    STANDARD,
    MOTORBIKE,
    TRACTOR,
    EMERGENCY,
    DIPLOMAT,
    FOREIGN,
    MILITARY
}

export class Vehicle {
    constructor(public type: VehicleType) {
    }
}
