defmodule TollCalculatorTest do
  use ExUnit.Case

  describe "get_toll_fee/2" do
    test "no passages, returns 0" do
      assert TollCalculator.get_toll_fee(:car, []) == 0
    end

    test "toll-free vehicle, returns 0" do
      assert TollCalculator.get_toll_fee(:motorbike, [~N[2020-02-05 16:00:00]]) == 0
      assert TollCalculator.get_toll_fee(:tractor, [~N[2020-02-05 16:00:00]]) == 0
      assert TollCalculator.get_toll_fee(:emergency, [~N[2020-02-05 16:00:00]]) == 0
      assert TollCalculator.get_toll_fee(:diplomat, [~N[2020-02-05 16:00:00]]) == 0
      assert TollCalculator.get_toll_fee(:foreign, [~N[2020-02-05 16:00:00]]) == 0
      assert TollCalculator.get_toll_fee(:military, [~N[2020-02-05 16:00:00]]) == 0
    end
  end
end
