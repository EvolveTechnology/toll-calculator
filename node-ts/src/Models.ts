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

export interface MonthDay {
    month: number;
    dayOfMonth: number;
}

export interface HourMinutes {
    hour: number;
    minute: number;
}

export interface FeeInterval {
    fee: number;
    from: HourMinutes;
    to: HourMinutes;
}
