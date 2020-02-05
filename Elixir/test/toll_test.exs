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
  end
end
