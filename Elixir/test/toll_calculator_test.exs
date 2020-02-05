defmodule TollCalculatorTest do
  use ExUnit.Case

  describe "get_toll_fee/2" do
    test "no passages, returns 0" do
      assert TollCalculator.get_toll_fee(:car, []) == 0
    end
  end
end
