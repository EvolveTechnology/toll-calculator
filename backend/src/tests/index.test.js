import axios from 'axios';
import MockAdapter from 'axios-mock-adapter';
import { byVehicleCalculator, allVehiclesCalculator } from '..';
import { holidaysURI } from '../services/holidaysAPI';

import vehicles from '../services/vehiclesAPI/tests/mock';
import { holidays2018 as holidays } from '../services/holidaysAPI/tests/mock';
import expectedByVehicle from './mock';

const mock = new MockAdapter(axios);
const apiKey = 'secret';
const vehiclesEndPoint = 'vehiclesEndpoint';
const year = '2018';

mock.onGet(vehiclesEndPoint).reply(200, vehicles);
mock.onGet(holidaysURI(apiKey, year)).reply(200, { ...holidays });

describe('byVehicleCalculator', () => {
  const regNum = 'QNX-473';
  it('calculates toll for one vehicle', async () => {
    const result = await byVehicleCalculator(regNum, apiKey, vehiclesEndPoint);
    expect(result).toEqual(expectedByVehicle);
  });
});

describe('allVehiclesCalculator', () => {
  it('calculates toll for all vehicles', async () => {
    const result = await allVehiclesCalculator(apiKey, vehiclesEndPoint);
    expect(result).toEqual([expectedByVehicle]);
  });
});
