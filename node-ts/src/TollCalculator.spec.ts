import { assert } from 'chai';
import { FEE_DURATION_IN_MINUTES, FEE_INTERVALS, FREE_DAYS, FREE_DATES, FREE_VEHICLE_TYPES, MAX_FEE_PER_DAY } from './config';
import { TollCalculator } from './TollCalculator';
import { Vehicle, VehicleType } from './Models';

describe('TollCalculator', () => {

  // Generate dates for a day. 1 hour for each amount, so if amount is 3 it will generate times from 00 - 02 for the specific day
  const generateDatesForADay = (date: number, amount: number): Date[] => {
    return Array.from(new Array(amount), (val, idx) => new Date(new Date(date).setUTCHours(idx, 0, 0, 0)));
  }
  // 2017-11-12 = sunday
  const SUNDAY = Date.UTC(2017, 10, 12);
  // 2017-11-13 = monday no red day
  const ORDINARY_DAY = Date.UTC(2017, 10, 13);

  describe('#tollFee()', () => {

    it('should be free for free vehicle types', () => {
      FREE_VEHICLE_TYPES.forEach(type => {
        const tollFee = new TollCalculator()
          .tollFee(
          new Vehicle(type),
          generateDatesForADay(ORDINARY_DAY, 12));

        assert.equal(tollFee, 0, `${VehicleType[type]} should be fee-free`);
      });
    });

    it('should be free for all vehicles on sundays', () => {
      const tollFee = new TollCalculator()
        .tollFee(
        new Vehicle(VehicleType.STANDARD),
        generateDatesForADay(SUNDAY, 12));

      assert.equal(tollFee, 0, 'Free days should be fee-free');
    });

    it('should be free for all vehicles on free dates', () => {
      const vehicle = new Vehicle(VehicleType.STANDARD);
      FREE_DATES.forEach(d => {
        const freeDate = Date.UTC(2017, d.month, d.dayOfMonth, 12, 0);
        const tollFee = new TollCalculator()
          .tollFee(
          vehicle,
          generateDatesForADay(freeDate, 12));

        assert.equal(tollFee, 0, 'Free dates should be fee-free');
      })
    });

    it(`should maximum cost ${MAX_FEE_PER_DAY} sek for a day`, () => {
      const tollFee = new TollCalculator()
        .tollFee(
        new Vehicle(VehicleType.STANDARD),
        generateDatesForADay(ORDINARY_DAY, 24));

      assert.equal(tollFee, MAX_FEE_PER_DAY, `Maximum fee for a day should be ${MAX_FEE_PER_DAY} sek`);
    });

    it(`should only be charged once per ${FEE_DURATION_IN_MINUTES} minutes`, () => {
      const costInterval = FEE_INTERVALS[0];
      const tollFee = new TollCalculator()
        .tollFee(
        new Vehicle(VehicleType.STANDARD),
        [
          new Date(new Date(ORDINARY_DAY).setUTCHours(costInterval.from.hour, costInterval.from.minute)),
          new Date(new Date(ORDINARY_DAY).setUTCHours(costInterval.from.hour, costInterval.from.minute + (FEE_DURATION_IN_MINUTES - 5)))
        ]);

      assert.equal(tollFee, costInterval.fee, `Vehicles can't be charged more than one time per ${FEE_DURATION_IN_MINUTES} minutes`);
    });

  });

});
