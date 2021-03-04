<?php

namespace App\Tests\Unit;

use App\Car;
use App\Motorbike;
use App\TollCalculator;
use App\Tractor;
use App\VehicleInterface;
use DateTime;
use PHPUnit\Framework\TestCase;

/**
 * @coversDefaultClass \App\TollCalculator
 */
class TollCalculatorTest extends TestCase {

  /**
   * Tests toll fees.
   *
   * @param \DateTime[] $dates
   * @param int $expected_fee
   *
   * @dataProvider providerTestTollFees
   */
  public function testTollFees(array $dates, int $expected_fee) {
    $toll_calculator = new TollCalculator();
    $vehicle = new Car();
    $toll_fee = $toll_calculator->calculateTollFee($vehicle, $dates);
    self::assertEquals($expected_fee, $toll_fee);
  }

  /**
   * Provides data for toll fees test.
   *
   * @see \App\Tests\Unit\TollCalculatorTest::testTollFees()
   */
  public function providerTestTollFees(): array {
    return [
      'One time with no fee' => [
        [new DateTime('2021-02-01 19:00')],
        0,
      ],
      'One time with fee' => [
        [new DateTime('2021-02-01 06:15')],
        8,
      ],
      'Two times in same hour, same fee' => [
        [
          new DateTime('2021-02-01 06:15'),
          new DateTime('2021-02-01 06:20'),
        ],
        8,
      ],
      'Two times in same hour, different fee' => [
        [
          new DateTime('2021-02-01 06:15'),
          new DateTime('2021-02-01 06:45'),
        ],
        13,
      ],
      'Two times in different hours' => [
        [
          new DateTime('2021-02-01 06:45'),
          new DateTime('2021-02-01 17:45'),
        ],
        26,
      ],
      'Multiple times in one hour and then multiple times next hour' => [
        [
          // Hour 1.
          new DateTime('2021-02-01 08:30'),
          new DateTime('2021-02-01 08:35'),
          new DateTime('2021-02-01 08:40'),
          new DateTime('2021-02-01 08:45'),
          new DateTime('2021-02-01 08:50'),
          new DateTime('2021-02-01 08:55'),
          new DateTime('2021-02-01 09:25'),
          // Hour 2.
          new DateTime('2021-02-01 09:50'),
          new DateTime('2021-02-01 10:49'),
          // Hour 3.
          new DateTime('2021-02-01 11:00'),
          new DateTime('2021-02-01 11:59'),
          // Hour 4.
          new DateTime('2021-02-01 12:00'),
          new DateTime('2021-02-01 12:30'),
          new DateTime('2021-02-01 12:55'),
          new DateTime('2021-02-01 12:59'),
        ],
        32,
      ],
      'Multiple times throughout the day reaching max fee' => [
        [
          new DateTime('2021-02-01 06:45'),
          new DateTime('2021-02-01 07:50'),
          new DateTime('2021-02-01 08:55'),
          new DateTime('2021-02-01 10:00'),
          new DateTime('2021-02-01 15:00'),
          new DateTime('2021-02-01 16:05'),
          new DateTime('2021-02-01 18:00'),
        ],
        60,
      ],
    ];
  }

  /**
   * Tests toll free vehicles.
   *
   * @param \App\VehicleInterface $vehicle
   * @param int $expected_fee
   *
   * @dataProvider providerTestFeeFreeVehicles
   */
  public function testTollFreeVehicles(VehicleInterface $vehicle, int $expected_fee) {
    $toll_calculator = new TollCalculator();
    $date = new DateTime('2021-02-01 06:00');
    $toll_fee = $toll_calculator->calculateTollFee($vehicle, [$date]);
    self::assertEquals($expected_fee, $toll_fee);
  }

  /**
   * Provides data for toll free vehicles test.
   *
   * @see \App\Tests\Unit\TollCalculatorTest::testTollFreeVehicles()
   */
  public function providerTestFeeFreeVehicles(): array {
    return [
      'Car' => [
        new Car(),
        8,
      ],
      'Motorbike' => [
        new Motorbike(),
        0,
      ],
      'Tractor' => [
        new Tractor(),
        0,
      ],
    ];
  }

  /**
   * Tests toll free dates.
   *
   * @param \DateTime $date
   * @param int $expected_toll_fee
   *
   * @dataProvider providerTestTollFreeDays
   */
  public function testTollFreeDays(DateTime $date, int $expected_toll_fee) {
    $vehicle = new Car();
    $toll_calculator = new TollCalculator();

    $toll_fee = $toll_calculator->calculateTollFee($vehicle, [$date]);

    self::assertEquals($expected_toll_fee, $toll_fee);
  }

  /**
   * Provides data for toll free days test.
   *
   * @see \App\Tests\Unit\TollCalculatorTest::testTollFreeDays()
   */
  public function providerTestTollFreeDays(): array {
    return [
      'Bank holiday' => [
        new DateTime('2021-01-01'),
        0,
      ],
      'Regular date' => [
        new DateTime('2021-02-01 06:00'),
        8,
      ],
      'Sunday' => [
        new DateTime('2021-01-09'),
        0,
      ],
    ];
  }

}
