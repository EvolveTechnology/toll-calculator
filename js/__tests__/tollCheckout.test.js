'use strict';

import * as tollCheckout from '../tollCheckout';
import * as tollCalculator from '../tollCalculator';
import * as vehicle from '../vehicle';
import * as database from '../database';

jest.mock('../tollCalculator');
jest.mock('../database');

beforeAll(() => {
  tollCheckout.setDb(database);
});

/**
 * StarMode should be decreased by the difference between in minutes each passage
 */
it('Calculates StarMode', () => {
  database.transactionsById.mockReturnValueOnce([
    {
      id: 'ABC123',
      vehicleType: vehicle.car.type,
      date: new Date('2018-10-08T06:01Z'),
      starMode: 60
    },
  ]).mockReturnValueOnce([
    {
      id: 'ABC123',
      vehicleType: vehicle.car.type,
      date: new Date('2018-10-08T06:01Z'),
      starMode: 60
    }, {
      id: 'ABC123',
      vehicleType: vehicle.car.type,
      date: new Date('2018-10-08T06:40Z'),
      starMode: 19
    }
  ]);

  let passage = {
    id: 'ABC123',
    vehicleType: vehicle.car.type,
    date: new Date('2018-10-08T06:40Z'),
  };
  tollCheckout.setStarMode(passage);
  expect(passage.starMode).toBe(21);

  passage = {
    id: 'ABC123',
    vehicleType: vehicle.car.type,
    date: new Date('2018-10-08T06:42Z'),
  };
  tollCheckout.setStarMode(passage);
  expect(passage.starMode).toBe(19);
});

it('Creates a single transaction if passage not in StarMode', () => {
  tollCalculator.getTollFromDate.mockReturnValue(9);
  let firstPassage = {
    id: 'ABC123',
    vehicleType: vehicle.car.type,
    date: new Date('2018-10-08T06:01Z'),
    starMode: 60,
  };

  let transactions = tollCheckout.createLineItems(firstPassage, []);
  expect(transactions.length).toBe(1);
  expect(transactions[0].fee).toBe(9);
});

it('Creates two transactions when passage in StarMode', () => {
  tollCalculator.getTollFromDate.mockReturnValue(16);
  let firstPassage = {
    id: 'ABC123',
    vehicleType: vehicle.car.type,
    date: new Date('2018-10-08T06:01Z'),
    starMode: 60,
    fee: 9,
  };

  let passage = {
    id: 'ABC123',
    vehicleType: vehicle.car.type,
    date: new Date('2018-10-08T06:40Z'),
  };

  let transactions = tollCheckout.createLineItems(passage, [
    firstPassage,
  ]);

  expect(transactions.length).toBe(2);
  expect(transactions[0].fee).toBe(16);
  expect(transactions[1].fee).toBe(-9);
});

/**
 * The transactions should look like this:
 * fee: 9
 *   transaction: 9
 * fee: 3
 *   transaction: 3
 *   transaction: -3
 * fee: 16
 *   transaction: 16
 *   transaction: -9
 * We keep the highest fee for  the duration of a StarMode, and credit the
 * cheaper ones.
 */
it('Creates transactions with correct fees', () => {
  tollCalculator.getTollFromDate.mockReturnValue(16);

  let starModePassagesList = [{
    id: 'A',
    vehicleType: 1,
    date: '',
    fee: 9,
    starMode: 60,
  },{
    id: 'A',
    vehicleType: 1,
    date: '',
    fee: 3,
    starMode: 21,
  }],
    passage = {
      id: 'A',
      vehicleType: 1,
      date: '',
    };

  let transactions = tollCheckout.createLineItems(passage, starModePassagesList);

  expect(transactions.length).toBe(2);
  expect(transactions[0].fee).toBe(16);
  expect(transactions[1].fee).toBe(-9);
});

it('Checks out', () => {
  database.transactionsById.mockReturnValue([]);

  //tollCheckout.checkout(queue.shift());
  //tollCheckout.checkout(queue.shift());
  //tollCheckout.checkout(queue.shift());
  //tollCheckout.checkout(queue.shift());
  //tollCheckout.checkout(queue.shift());
});
