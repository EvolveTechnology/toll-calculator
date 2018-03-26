<? require_once('PHP/Vehicle.php'); ?>
<? require_once('PHP/Car.php'); ?>
<? require_once('PHP/Motorbike.php'); ?>
<? require_once('PHP/TollCalculator.php'); ?>
<!DOCTYPE html>
<html>
  <head>
    <meta charset="utf-8">
    <title>Toll Calculator</title>
  </head>
  <body>
    <?
      $tollCalculator = new TollCalculator();
      echo $tollCalculator->GetTollFees(new Car(), [
        new DateTime('2018-01-01 13:30'),
        new DateTime('2018-01-01 16:30'),
        new DateTime('2018-01-02 13:30'),
        new DateTime('2018-01-02 13:40'),
        new DateTime('2018-01-03 12:30'),
        new DateTime('2018-01-03 14:30'),
        new DateTime('2018-01-03 14:31'),
        new DateTime('2018-01-03 14:32'),
        new DateTime('2018-01-03 14:33')
      ]);
    ?>
  </body>
</html>
