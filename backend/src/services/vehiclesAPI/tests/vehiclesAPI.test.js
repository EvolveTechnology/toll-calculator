import axios from 'axios';
import MockAdapter from 'axios-mock-adapter';
import getAllVehicles from '..';
import vehicles from './mock';

// This sets the mock adapter on the default instance
const mock = new MockAdapter(axios);
const url = 'http://secretURL.com/vehicles';

mock.onGet(url).reply(200, vehicles);

describe('getALlVehicles', () => {
  it('fetches the holidays', async () => {
    const results = await getAllVehicles(url);
    expect(results).toEqual(vehicles);
  });
});
