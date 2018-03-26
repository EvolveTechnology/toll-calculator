<?

// echo "<pre>";

abstract class TollFreeVehicles {
  const Motorbike = "Motorbike";
  const Tractor = "Tractor";
  const Emergency = "Emergency";
  const Diplomat = "Diplomat";
  const Foreign = "Foreign";
  const Military = "Military";
}

abstract class DayOfWeek {
  const Sunday = 0;
  const Monday = 1;
  const Tuesday = 2;
  const Wednesday = 3;
  const Thursday = 4;
  const Friday = 5;
  const Saturday = 6;
  // etc.
}

class TollCalculator {
  /**
   * Calculate the total toll fee
   *
   * @param vehicle - the vehicle
   * @param dates   - date and time of all passes
   * @return - the total toll fee
   */

  public function GetTollFees($vehicle, $dates = []) {
    $totalFee = 0;
    $dailyFee = 0;
    $lastDate = $dates[0];
    $dailyFees = [];

    foreach ($dates as $date) {
      $fee = $this->GetTollFee($date, $vehicle);

      $minuteDiff = $date->diff($lastDate)->i;

      if (date('Ymd', $date->getTimestamp()) != date('Ymd', $lastDate->getTimestamp())) {
        array_push($dailyFees, $dailyFee);
      }

      if ($minuteDiff > 60 || $minuteDiff == 0) {
        $dailyFee += $fee;
      }

      $lastDate = $date;
    }
    array_push($dailyFees, $dailyFee);

    $dfl = count($dailyFees);
    for ($i = 0; $i < $dfl; $i++) {
      $totalFee += ($dailyFees[$i] <= 60) ? $dailyFees[$i] : 60;
    }

    return $totalFee;
  }

  private function IsTollFreeVehicle($vehicle) {
    if ($vehicle == null) return false;
    $vehicleType = $vehicle->getType();
    return $vehicleType == TollFreeVehicles::Motorbike ||
           $vehicleType == TollFreeVehicles::Tractor ||
           $vehicleType == TollFreeVehicles::Emergency ||
           $vehicleType == TollFreeVehicles::Diplomat ||
           $vehicleType == TollFreeVehicles::Foreign ||
           $vehicleType == TollFreeVehicles::Military;
  }

  public function GetTollFee($date, $vehicle) {
    if ($this->IsTollFreeDate($date) || $this->IsTollFreeVehicle($vehicle)) return 0;
    $hour = intval(date("H", $date->getTimestamp()));
    $minute = intval(date("i", $date->getTimestamp()));

    if ($hour == 6 && $minute >= 0 && $minute <= 29) return 8;
    else if ($hour == 6 && $minute >= 30 && $minute <= 59) return 13;
    else if ($hour == 7 && $minute >= 0 && $minute <= 59) return 18;
    else if ($hour == 8 && $minute >= 0 && $minute <= 29) return 13;
    else if ($hour >= 8 && $hour <= 14 && $minute >= 30 && $minute <= 59) return 8;
    else if ($hour == 15 && $minute >= 0 && $minute <= 29) return 13;
    else if ($hour == 15 && $minute >= 0 || $hour == 16 && $minute <= 59) return 18;
    else if ($hour == 17 && $minute >= 0 && $minute <= 59) return 13;
    else if ($hour == 18 && $minute >= 0 && $minute <= 29) return 8;
    else  return 0;
  }

  private function IsTollFreeDate($date) {
      $year = intval(date("Y", $date->getTimestamp()));
      $month = intval(date("m", $date->getTimestamp()));
      $day = intval(date("d", $date->getTimestamp()));
      $dayOfWeek = intval(date("w", $date->getTimestamp()));

      if ($dayOfWeek == DayOfWeek::Saturday || $dayOfWeek == DayOfWeek::Sunday) return true;

      if ($year == 2018) {
          if ($month == 1 && $day == 1 ||
              $month == 3 && ($day == 29 || $day == 30) ||
              $month == 4 && ($day == 2 || $day == 30) ||
              $month == 5 && ($day == 10 || $day == 19) ||
              $month == 6 && ($day == 6 || $day == 22) ||
              $month == 7 ||
              $month == 11 && $day == 2 ||
              $month == 12 && ($day == 24 || $day == 25 || $day == 26 || $day == 31)) {
              return true;
          }
      }
      return false;
  }
}
