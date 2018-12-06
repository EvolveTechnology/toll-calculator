import tollFreeCheck from '..';

describe('tollFreeVehicles', () => {
  const vehicles = [{ type: 'Car' }, { type: 'Tractor' }, { type: 'Motorbike' }];
  it('checks for a group of vehicles', () => {
    expect(vehicles.map(tollFreeCheck)).toEqual([false, true, true]);
  });
});
