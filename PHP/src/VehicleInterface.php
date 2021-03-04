<?php

namespace App;

/**
 * Represents the interface that all vehicle classes must implement.
 */
interface VehicleInterface {

  /**
   * Returns the vehicle type.
   *
   * @return string
   */
  public function getType(): string;

  /**
   * Returns whether the vehicle is toll free or not.
   *
   * @return bool
   */
  public function isTollFree(): bool;

}
