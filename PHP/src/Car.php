<?php

namespace App;

/**
 * Defines a car vehicle.
 */
class Car implements VehicleInterface {

  /**
   * @inheritDoc
   */
  public function getType(): string {
    return 'Car';
  }

  /**
   * @inheritDoc
   */
  public function isTollFree(): bool {
    return false;
  }

}
