<?php

namespace App;

/**
 * Defines a motorbike vehicle.
 */
class Motorbike implements VehicleInterface {

  /**
   * @inheritDoc
   */
  public function getType(): string {
    return 'Motorbike';
  }

  /**
   * @inheritDoc
   */
  public function isTollFree(): bool {
    return true;
  }

}
