import { assert } from 'chai';
import { TollCalculator } from './TollCalculator';
import { Vehicle, VehicleType } from './Models';

describe('TollCalculator', () => {

  // Generate dates for a day. 1 hour for each amount, so if amount is 3 it will generate times from 00 - 02 for the specific day
  const generateDatesForADay = (date: number, amount: number): Date[] => {
    return Array.from(new Array(amount), (val, idx) => new Date(new Date(date).setUTCHours(idx, 0, 0, 0)));
  }
  // 2017-11-12 = sunday
  const SUNDAY = Date.UTC(2017, 10, 12);
  // 2017-12-25 = christmas
  const CHRISTMAS_DAY = Date.UTC(2017, 11, 25);
  // 2017-11-13 = monday no red day
  const ORDINARY_DAY = Date.UTC(2017, 10, 13);

  describe('#tollFee()', () => {

    it('should be free for diplomatic vehicle types', () => {
      const tollFee = new TollCalculator()
      .tollFee(
        new Vehicle(VehicleType.DIPLOMAT),
        generateDatesForADay(ORDINARY_DAY, 12));

      assert.equal(tollFee, 0, 'Diplomat vehicle should be fee-free');
    });

    it('should be free for all vehicles on sundays', () => {
      const tollFee = new TollCalculator()
      .tollFee(
        new Vehicle(VehicleType.STANDARD),
        generateDatesForADay(SUNDAY, 12));

      assert.equal(tollFee, 0, 'Sundays should be fee-free');
    });

    it('should be free for all vehicles on christmas', () => {
      const tollFee = new TollCalculator()
      .tollFee(
        new Vehicle(VehicleType.STANDARD),
        generateDatesForADay(CHRISTMAS_DAY, 12));

      assert.equal(tollFee, 0, 'Christmas should be fee-free');
    });

    it('should maximum cost 60 sek for a day', () => {
      const tollFee = new TollCalculator()
      .tollFee(
        new Vehicle(VehicleType.STANDARD),
        generateDatesForADay(ORDINARY_DAY, 24));

      assert.equal(tollFee, 60, 'Maximum fee for a day should be 60 sek');
    });

    it('should only be charged once per hour', () => {
      const tollFee = new TollCalculator()
      .tollFee(
        new Vehicle(VehicleType.STANDARD),
        [
          new Date(new Date(ORDINARY_DAY).setUTCHours(12, 0)),
          new Date(new Date(ORDINARY_DAY).setUTCHours(12, 10))
        ]);

      assert.equal(tollFee, 18, 'Vehicles can\'t be charged more than one time per hour');
    });

  });

});
