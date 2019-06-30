import { parseISO } from "date-fns";
import {
    __test__isTollFreeVehicle,
    __test__calculateHourlyToll,
    calculateDailyToll,
} from '../src/toll'
 
var assert = require('assert');
describe('tollCalculator', function() {
  describe('toll free vehicles', function() {
    it('motorcycle', function() {
      assert.equal(__test__isTollFreeVehicle({type: 'motorcycle'}), true)
    });
    it('tractor', function() {
        assert.equal(__test__isTollFreeVehicle({type: 'tractor'}), true)
    });
    it('emergency', function() {
        assert.equal(__test__isTollFreeVehicle({type: 'emergency'}), true)
    });
    it('diplomat', function() {
        assert.equal(__test__isTollFreeVehicle({type: 'diplomat'}), true)
    });
    it('foreign', function() {
        assert.equal(__test__isTollFreeVehicle({type: 'foreign'}), true)
    });
    it('military', function() {
        assert.equal(__test__isTollFreeVehicle({type: 'military'}), true)
    });
    it('car is not toll free', function() {
        assert.equal(__test__isTollFreeVehicle({type: 'car'}), false)
    });
    it('unknown vehicle type is tolled', function() {
        assert.equal(__test__isTollFreeVehicle({type: 'blurg'}), false)
    });
  });
  describe('toll intervals', function() {
    it('06:00 - 06:30', function() {
        assert.equal(__test__calculateHourlyToll(parseISO('2019-01-02T06:20')), 8)
    });
    it('06:30 - 07:00', function() {
        assert.equal(__test__calculateHourlyToll(parseISO('2019-01-02T06:40')), 13)
    });
    it('07:00 - 08:00', function() {
        assert.equal(__test__calculateHourlyToll(parseISO('2019-01-02T07:20')), 18)
    });
    it('08:00 - 08:30', function() {
        assert.equal(__test__calculateHourlyToll(parseISO('2019-01-02T08:20')), 13)
    });
    it('08:30 - 15:00', function() {
        assert.equal(__test__calculateHourlyToll(parseISO('2019-01-02T08:40')), 8)
    });
    it('15:00 - 15:30', function() {
        assert.equal(__test__calculateHourlyToll(parseISO('2019-01-02T15:20')), 13)
    });
    it('15:30 - 17:00', function() {
        assert.equal(__test__calculateHourlyToll(parseISO('2019-01-02T15:40')), 18)
    });
    it('17:00 - 17:30', function() {
        assert.equal(__test__calculateHourlyToll(parseISO('2019-01-02T17:20')), 13)
    });
    it('18:00 - 18:30', function() {
        assert.equal(__test__calculateHourlyToll(parseISO('2019-01-02T18:20')), 8)
    });
    it('18:30 - 00:00', function() {
        assert.equal(__test__calculateHourlyToll(parseISO('2019-01-02T18:40')), 0)
    });
    it('00:00 - 06:00', function() {
        assert.equal(__test__calculateHourlyToll(parseISO('2019-01-02T00:01')), 0)
    });
    it('milliseconds before', function() {
        assert.equal(__test__calculateHourlyToll(parseISO('2019-01-02T07:59:59')), 18)
    });
    it('milliseconds after', function() {
        assert.equal(__test__calculateHourlyToll(parseISO('2019-01-02T08:00:00')), 13)
    });
  });
  describe('toll daily', function() {
    it('same hour - same toll', function() {
        assert.equal(calculateDailyToll({type: 'car'}, ['2019-01-02T08:00:00', '2019-01-02T08:09:00']), 13)
    });
    it('same hour - two tolls, highest toll paid - ascending', function() {
        assert.equal(calculateDailyToll({type: 'car'}, ['2019-01-02T06:20:00', '2019-01-02T06:40:00']), 13)
    });
    it('same hour - two tolls, highest toll paid - decending', function() {
        assert.equal(calculateDailyToll({type: 'car'}, ['2019-01-02T08:20:00', '2019-01-02T08:40:00']), 13)
    });
    it('multiple tolls', function() {
        assert.equal(calculateDailyToll({type: 'car'}, ['2019-01-02T15:05:00', '2019-01-02T08:40:00']), 21)
    });
    it('max fee', function() {
        assert.equal(calculateDailyToll({type: 'car'}, ['2019-01-02T07:35:00', '2019-01-02T15:35:00', '2019-01-02T17:25:00', '2019-01-02T08:25:00']), 60)
    });
  });
});