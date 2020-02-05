defmodule TollTest do
  use ExUnit.Case

  describe "calculate_fee/2" do
    test "no passages, returns 0" do
      assert Toll.calculate_fee(:car, []) == 0
    end

    test "toll-free vehicle, returns 0" do
      passages = [~N[2020-02-05 16:00:00]]

      assert Toll.calculate_fee(:motorbike, passages) == 0
      assert Toll.calculate_fee(:tractor, passages) == 0
      assert Toll.calculate_fee(:emergency, passages) == 0
      assert Toll.calculate_fee(:diplomat, passages) == 0
      assert Toll.calculate_fee(:foreign, passages) == 0
      assert Toll.calculate_fee(:military, passages) == 0
    end

    test "before 06:00, returns 0" do
      assert Toll.calculate_fee(:car, [~N[2020-02-05 05:59:59]]) == 0
    end

    test "between 06:00 and 06:30, returns 8" do
      assert Toll.calculate_fee(:car, [~N[2020-02-05 06:00:00]]) == 8
      assert Toll.calculate_fee(:car, [~N[2020-02-05 06:29:59]]) == 8
    end

    test "between 06:30 and 07:00, returns 13" do
      assert Toll.calculate_fee(:car, [~N[2020-02-05 06:30:00]]) == 13
      assert Toll.calculate_fee(:car, [~N[2020-02-05 06:59:59]]) == 13
    end

    test "between 07:00 and 08:00, returns 18" do
      assert Toll.calculate_fee(:car, [~N[2020-02-05 07:00:00]]) == 18
      assert Toll.calculate_fee(:car, [~N[2020-02-05 07:59:59]]) == 18
    end

    test "between 08:00 and 08:30, returns 13" do
      assert Toll.calculate_fee(:car, [~N[2020-02-05 08:00:00]]) == 13
      assert Toll.calculate_fee(:car, [~N[2020-02-05 08:29:59]]) == 13
    end

    test "between 08:30 and 15:00, returns 8" do
      assert Toll.calculate_fee(:car, [~N[2020-02-05 08:30:00]]) == 8
      assert Toll.calculate_fee(:car, [~N[2020-02-05 14:59:59]]) == 8
    end

    test "between 15:00 and 15:30, returns 13" do
      assert Toll.calculate_fee(:car, [~N[2020-02-05 15:00:00]]) == 13
      assert Toll.calculate_fee(:car, [~N[2020-02-05 15:29:59]]) == 13
    end

    test "between 15:30 and 17:00, returns 18" do
      assert Toll.calculate_fee(:car, [~N[2020-02-05 15:30:00]]) == 18
      assert Toll.calculate_fee(:car, [~N[2020-02-05 16:59:59]]) == 18
    end

    test "between 17:00 and 18:00, returns 13" do
      assert Toll.calculate_fee(:car, [~N[2020-02-05 17:00:00]]) == 13
      assert Toll.calculate_fee(:car, [~N[2020-02-05 17:59:59]]) == 13
    end

    test "between 18:00 and 18:30, returns 8" do
      assert Toll.calculate_fee(:car, [~N[2020-02-05 18:00:00]]) == 8
      assert Toll.calculate_fee(:car, [~N[2020-02-05 18:29:59]]) == 8
    end

    test "after 18:30, returns 0" do
      assert Toll.calculate_fee(:car, [~N[2020-02-05 18:30:00]]) == 0
    end

    test "weekend, returns 0" do
      assert Toll.calculate_fee(:car, [~N[2020-02-08 15:30:00]]) == 0
      assert Toll.calculate_fee(:car, [~N[2020-02-09 15:30:00]]) == 0
    end
  end
end
