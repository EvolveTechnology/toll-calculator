import byDayFeeAccumulator from '..';

describe('byDayFeeAccumulator', () => {
  const vehicle = {
    type: 'Card',
  };
  const day = '2018-02-03';

  const byDay = {
    [day]: ['2018-02-03 06:18:00', '2018-02-03 09:10:00'],
  };

  const holidays = ['2018-02-14'];
  const otherDays = {
    fees: {
      '2018-01-31': {
        chargeablePasses: 0,
        isTollFreeDay: true,
        isTollFreeVehicle: false,
        passes: ['2018-01-31 22:56:04'],
        totalFee: 0,
        totalPasses: 1,
      },
    },
  };

  it('accumulates the fee for various days', () => {
    expect(byDayFeeAccumulator(vehicle, byDay, holidays, otherDays, day)).toEqual({
      ...otherDays,
      '2018-02-03': {
        chargeablePasses: 0,
        isTollFreeDay: true,
        isTollFreeVehicle: false,
        passes: ['2018-02-03 06:18:00', '2018-02-03 09:10:00'],
        totalFee: 0,
        totalPasses: 2,
      },
    });
  });
});
