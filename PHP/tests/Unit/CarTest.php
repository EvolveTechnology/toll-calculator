<?php

namespace App\Tests\Unit;

use App\Car;
use PHPUnit\Framework\TestCase;

/**
 * @coversDefaultClass \App\Car
 */
class CarTest extends TestCase {

  /**
   * @covers ::getType
   */
  public function testReturnsCorrectType() {
    $car = new Car();
    self::assertEquals('Car', $car->getType());
  }

}
