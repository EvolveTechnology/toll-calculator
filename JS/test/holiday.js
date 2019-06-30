import { __test__isHoliday } from '../src/toll'
import { parseISO } from "date-fns";

var assert = require('assert');
describe('holiday', function() {
    describe('easter', function() {
        it('easter-2', function() {
            assert.equal(__test__isHoliday(parseISO('2019-04-19')), true)
        });
        it('easter-2 last year', function() {
            assert.equal(__test__isHoliday(parseISO('2018-03-30')), true)
        });
        it('easter-2 next year', function() {
            assert.equal(__test__isHoliday(parseISO('2020-04-10')), true)
        });
        it('easter+1', function() {
            assert.equal(__test__isHoliday(parseISO('2019-04-22')), true)
        });
        it('non free day', function() {
            assert.equal(__test__isHoliday(parseISO('2019-04-18')), false)
        });
        it('assencion', function() {
            assert.equal(__test__isHoliday(parseISO('2019-05-30')), true)
        });
    });
    describe('weekdays', function() {
        it('saturday', function() {
            assert.equal(__test__isHoliday(parseISO('2019-08-10')), true)
        });
        it('sunday', function() {
            assert.equal(__test__isHoliday(parseISO('2019-08-11')), true)
        });
        it('monday', function() {
            assert.equal(__test__isHoliday(parseISO('2019-08-05')), false)
        });
        it('tuseday', function() {
            assert.equal(__test__isHoliday(parseISO('2019-08-06')), false)
        });
        it('wednesday', function() {
            assert.equal(__test__isHoliday(parseISO('2019-08-07')), false)
        });
        it('thursday', function() {
            assert.equal(__test__isHoliday(parseISO('2019-08-08')), false)
        });
        it('friday', function() {
            assert.equal(__test__isHoliday(parseISO('2019-08-09')), false)
        });
    });
    describe('fixed holidays', function() {
        it('nations day', function() {
            assert.equal(__test__isHoliday(parseISO('2019-06-06')), true)
        });
        it('new years day', function() {
            assert.equal(__test__isHoliday(parseISO('2019-01-01')), true)
        });
        it('christmas day', function() {
            assert.equal(__test__isHoliday(parseISO('2019-12-25')), true)
        });
        it('christmas other day', function() {
            assert.equal(__test__isHoliday(parseISO('2019-12-26')), true)
        });
        it('thriteenday', function() {
            assert.equal(__test__isHoliday(parseISO('2020-01-06')), true)
        });
        it('first of may', function() {
            assert.equal(__test__isHoliday(parseISO('2020-01-01')), true)
        });
    });
    describe('date formats', function() {
        it('time formats - holiday', function() {
            assert.equal(__test__isHoliday(parseISO('2019-06-06T07:00')), true)
        });
        it('time formats - working day', function() {
            assert.equal(__test__isHoliday(parseISO('2019-08-08T07:00')), false)
        });
    });
});
