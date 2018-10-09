import * as tollCalculator from '../tollCalculator';

describe('Check time and fee calculations', () => {

  it('Is a toll free day (SUNDAY)', () => {
    let sunday = new Date('2018-10-07T07:33:00Z');
    expect(tollCalculator.isTollFreeDay(sunday)).toBeTruthy();
  });

  it('Is a toll free day (SATURDAY)', () => {
    let saturday = new Date('2018-10-06T07:33:00Z');
    expect(tollCalculator.isTollFreeDay(saturday)).toBeTruthy();
  });

  it('Is a toll free time (18:30-05:59)', () => {
    let dateBefore = new Date();
    dateBefore.setUTCHours(5, 0, 0, 0);
    const timeBefore = tollCalculator.getTimeFromISOString(dateBefore);

    let dateAfter = new Date();
    dateAfter.setUTCHours(19, 0, 0, 0);
    const timeAfter = tollCalculator.getTimeFromISOString(dateAfter);

    expect(tollCalculator.isTollFreeTime(timeBefore)).toBeTruthy();
    expect(tollCalculator.isTollFreeTime(timeAfter)).toBeTruthy();
  });

  it('Is not a toll free time (06:00-18:30)', () => {
    let tollDay = new Date('2018-10-08T07:33:00Z');
    let tollTime = tollCalculator.getTimeFromISOString(tollDay);
    expect(tollCalculator.isTollFreeTime(tollTime)).toBeFalsy();
  });

  it('Is a toll free date', () => {
    let holiday = new Date('2018-01-01T07:33:00Z');
    let vacation = new Date('2018-07-10T07:22:00Z');

    expect(tollCalculator.isTollFreeDate(holiday)).toBeTruthy();
    expect(tollCalculator.isTollFreeDate(vacation)).toBeTruthy();
  });

  it('Should return the right amount given a date', () => {
    let dateFeeNine = new Date('2018-10-05T07:30:00Z');
    let dateFeeTwentyTwo = new Date(dateFeeNine.toUTCString());
    dateFeeNine.setUTCHours(6, 26, 0, 0);
    dateFeeTwentyTwo.setUTCHours(15, 45, 0, 0);
    expect(tollCalculator.getTollFromDate(dateFeeNine)).toBe(9);
    expect(tollCalculator.getTollFromDate(dateFeeTwentyTwo)).toBe(22);
  });

  it('Returns a zero fee when time is outside of the toll times', () => {
    let timeBefore = new Date();
    timeBefore.setUTCHours(5, 0, 0, 0);
    let timeAfter = new Date();
    timeAfter.setUTCHours(19, 0, 0, 0);

    expect(tollCalculator.getTollFromDate(timeBefore)).toBe(0);
    expect(tollCalculator.getTollFromDate(timeAfter)).toBe(0);
  });

});
