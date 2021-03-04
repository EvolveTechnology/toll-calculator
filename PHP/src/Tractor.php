<?php

namespace App;

/**
 * Defines a tractor vehicle.
 */
class Tractor implements VehicleInterface {

  /**
   * @inheritDoc
   */
  public function getType(): string {
    return 'Tractor';
  }

  /**
   * @inheritDoc
   */
  public function isTollFree(): bool {
    return true;
  }

}
