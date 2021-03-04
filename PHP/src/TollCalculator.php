<?php

namespace App;

use DateTime;

class TollCalculator {

  public const NO_FEE = 0;

  public const LOW_FEE = 8;

  public const MEDIUM_FEE = 13;

  public const HIGH_FEE = 18;

  public const MAX_FEE = 60;

  /**
   * An array of toll free dates.
   *
   * @var array
   * @todo This should ideally not be hard-coded but rather fetch from an
   *   API. That is however out of scope.
   *
   */
  protected array $tollFreeDates = [
    '2021' => [
      '1' => ['1', '5', '6'],
      '4' => ['1', '2', '5', '30'],
      '5' => ['12', '13'],
      '6' => ['25'],
      '11' => ['5'],
      '12' => ['24', '31'],
    ],
  ];

  /**
   * Calculate the total toll fee for one day.
   *
   * @param \App\VehicleInterface $vehicle
   * @param \DateTime[] $date_times
   *
   * @return int
   *   Fee in SEK.
   */
  public function calculateTollFee(VehicleInterface $vehicle, array $date_times): int {
    if ($vehicle->isTollFree()) {
      return self::NO_FEE;
    }

    if ($this->isTollFreeDate($date_times[0])) {
      return self::NO_FEE;
    }

    $total_fee = 0;

    $interval_start = $date_times[0];
    foreach ($date_times as $index => $date_time) {
      $fee = $this->getTollFee($date_time);

      $diff_hours = $date_time->diff($interval_start)->h;

      if ($index !== 0 && $diff_hours < 1) {
        $previous_fee = $this->getTollFee($interval_start);

        if ($fee <= $previous_fee) {
          continue;
        }

        $total_fee -= $previous_fee;
      }

      if ($diff_hours >= 1) {
        $interval_start = $date_time;
      }

      $total_fee += $fee;
    }

    if ($total_fee > self::MAX_FEE) {
      return self::MAX_FEE;
    }

    return $total_fee;
  }

  /**
   * Get toll fee for a single passage.
   *
   * @param \DateTime $date_time
   *
   * @return int
   */
  public function getTollFee(DateTime $date_time): int {
    $date_from_input = $date_time->format('Y-m-d');

    $fee_intervals = [
      ['00:00', '05:59', self::NO_FEE],
      ['06:00', '06:29', self::LOW_FEE],
      ['06:30', '06:59', self::MEDIUM_FEE],
      ['07:00', '07:59', self::HIGH_FEE],
      ['08:00', '08:29', self::MEDIUM_FEE],
      ['08:30', '14:59', self::LOW_FEE],
      ['15:00', '15:29', self::MEDIUM_FEE],
      ['15:30', '16:59', self::HIGH_FEE],
      ['17:00', '17:59', self::MEDIUM_FEE],
      ['18:00', '18:29', self::LOW_FEE],
      ['18:30', '23:59', self::NO_FEE],
    ];

    foreach ($fee_intervals as [$start_time, $end_time, $fee]) {
      $interval_start = new DateTime("$date_from_input $start_time");
      $interval_end = new DateTime("$date_from_input $end_time");

      if ($date_time >= $interval_start && $date_time <= $interval_end) {
        return $fee;
      }
    }

    return self::NO_FEE;
  }

  /**
   * Check whether the date of passage is a toll free date.
   *
   * Saturdays, Sundays and bank holidays are toll free.
   *
   * @param \DateTime $date
   *
   * @return bool
   */
  protected function isTollFreeDate(DateTime $date): bool {
    $year = $date->format('Y');
    $month = $date->format('n');
    $day = $date->format('j');
    $weekday = $date->format('l');

    if ($weekday === 'Saturday' || $weekday === 'Sunday') {
      return TRUE;
    }

    return isset($this->tollFreeDates[$year][$month][$day]);
  }

}
