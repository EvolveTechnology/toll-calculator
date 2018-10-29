import { expect } from 'chai';
import 'mocha';
import Motorbike from './Motorbike';
import Car from './Car';
import TollCalculator from './TollCalculator';

const motorbike = new Motorbike();
describe('Class: Motorbike, Function getType', () => {
  it('Should return its type', () => {

    const motorbikeType: string = motorbike.getType();
    expect(motorbikeType).to.equal('Motorbike');
  });
});

const car = new Car();
describe('Class Car, Function getType', () => {
  it('Should return its type', () => {

    const carType: string = car.getType();
    expect(carType).to.equal('Car');
  });
});

const tollCalculator: TollCalculator = new TollCalculator();
describe('Class: TollCalculator, Function: getDayTollFee', () => {
  it('Should be free for motorbikes', () => {

    let motorbikeFee: number = tollCalculator.getDayTollFee(motorbike, [new Date()]);
    expect(motorbikeFee).to.equal(0);
  });
});

describe('Class: TollCalculator, Function: getDayTollFee', () => {
  it('Should count multiple timestamps in 1 hour as one', () => {

    let timeStamps = [new Date('2018-01-02 16:00'), new Date('2018-01-02 16:01'), new Date('2018-01-02 18:00')]
    let fee: number = tollCalculator.getDayTollFee(car, timeStamps);
    expect(fee).to.equal(18 + 8);
  });
});

describe('Class: TollCalculator, Function: getDayTollFee', () => {
  it('Should max out the fee at 60', () => {

    let timeStamps = [];
    for(let h = 6; h < 19; h++) {
      timeStamps.push(new Date(`2018-01-02 ${h}:00`));
    }
    let maxFee: number = tollCalculator.getDayTollFee(car, timeStamps);
    expect(maxFee).to.equal(60);
  });
});

describe('Class: TollCalculator, Function: getDayTollFee', () => {
  it('Should be free on weekends', () => {

    let timeStamps = [new Date('2018-01-06 7:00')];
    let weekendFee: number = tollCalculator.getDayTollFee(car, timeStamps);
    expect(weekendFee).to.equal(0);
  });
});

describe('Class: TollCalculator, Function: getDayTollFee', () => {
  it('Should be free on a special holiday', () => {

    let timeStamps = [new Date('2018-01-01 7:00')];
    let holidayFee: number = tollCalculator.getDayTollFee(car, timeStamps);
    expect(holidayFee).to.equal(0);
  });
});

describe('Class: TollCalculator, Function: getTollFee', () => {
  it('Should have higher fees in rush hour', () => {

    let rushHourFee: number = tollCalculator.getTollFee(car, new Date('2018-01-02 7:00'));
    let calmHourFee: number = tollCalculator.getTollFee(car, new Date('2018-01-02 10:00'));
    expect(rushHourFee).to.be.above(calmHourFee);
  });
});