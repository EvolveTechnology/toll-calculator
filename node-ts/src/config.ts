import { FeeInterval, MonthDay, VehicleType } from './Models';

// Maximum fee a vehicle can have per day
export const MAX_FEE_PER_DAY = 60;
// Time in minutes a fee "lasts". A driver can't be charged again within this interval
export const FEE_DURATION_IN_MINUTES = 60;
// Vehicle types that don't have to pay toll fee
export const FREE_VEHICLE_TYPES = [
    VehicleType.MOTORBIKE,
    VehicleType.TRACTOR,
    VehicleType.EMERGENCY,
    VehicleType.DIPLOMAT,
    VehicleType.FOREIGN,
    VehicleType.MILITARY
]
// 0 = sunday = free day
export const FREE_DAYS = [0];
// Red dates
export const FREE_DATES: MonthDay[] = [
    // New years day
    { month: 0, dayOfMonth: 1 },
    // May the fourth be with you
    { month: 4, dayOfMonth: 1 },
    // Chistmas
    { month: 11, dayOfMonth: 24 },
    { month: 11, dayOfMonth: 25 },
    { month: 11, dayOfMonth: 26 }
]
// Tax costs for low, medium and high traffic
const FEE_COSTS = {
    LOW: 8,
    MID: 13,
    HIGH: 18
}
// Fee intervals, what it costs between a given time interval
export const FEE_INTERVALS: FeeInterval[] = [
    {
        fee: FEE_COSTS.LOW,
        from: {
            hour: 6,
            minute: 0
        },
        to: {
            hour: 6,
            minute: 30
        }
    }, {
        fee: FEE_COSTS.MID,
        from: {
            hour: 6,
            minute: 30
        },
        to: {
            hour: 7,
            minute: 0
        }
    }, {
        fee: FEE_COSTS.HIGH,
        from: {
            hour: 7,
            minute: 0
        },
        to: {
            hour: 8,
            minute: 0
        }
    }, {
        fee: FEE_COSTS.MID,
        from: {
            hour: 8,
            minute: 0
        },
        to: {
            hour: 8,
            minute: 30
        }
    }, {
        fee: FEE_COSTS.LOW,
        from: {
            hour: 8,
            minute: 30
        },
        to: {
            hour: 15,
            minute: 0
        }
    }, {
        fee: FEE_COSTS.MID,
        from: {
            hour: 15,
            minute: 0
        },
        to: {
            hour: 15,
            minute: 30
        }
    }, {
        fee: FEE_COSTS.HIGH,
        from: {
            hour: 15,
            minute: 30
        },
        to: {
            hour: 17,
            minute: 0
        }
    }, {
        fee: FEE_COSTS.MID,
        from: {
            hour: 17,
            minute: 0
        },
        to: {
            hour: 18,
            minute: 0
        }
    }, {
        fee: FEE_COSTS.LOW,
        from: {
            hour: 18,
            minute: 0
        },
        to: {
            hour: 18,
            minute: 30
        }
    }
];
