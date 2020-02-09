defmodule Toll.PassageTest do
  use ExUnit.Case, async: true
  alias Toll.Passage

  describe "exempt vehicles" do
    test "does not charge fee" do
      datetime = ~N[2020-02-05 16:00:00]

      assert Passage.new(:motorbike, datetime).fee == 0
      assert Passage.new(:tractor, datetime).fee == 0
      assert Passage.new(:emergency, datetime).fee == 0
      assert Passage.new(:diplomat, datetime).fee == 0
      assert Passage.new(:foreign, datetime).fee == 0
      assert Passage.new(:military, datetime).fee == 0
    end
  end

  describe "weekends" do
    test "does not charge fee" do
      assert Passage.new(:car, ~N[2020-02-08 15:30:00]).fee == 0
      assert Passage.new(:car, ~N[2020-02-09 15:30:00]).fee == 0
    end
  end

  describe "holidays" do
    test "does not charge fee" do
      assert Passage.new(:car, ~N[2020-01-01 15:30:00]).fee == 0
      assert Passage.new(:car, ~N[2020-05-01 15:30:00]).fee == 0
      assert Passage.new(:car, ~N[2020-06-06 15:30:00]).fee == 0
    end
  end

  describe "rate" do
    test "before 06:00 is 0" do
      assert Passage.new(:car, ~N[2020-02-05 05:59:59]).fee == 0
    end

    test "between 06:00 and 06:30 is 8" do
      assert Passage.new(:car, ~N[2020-02-05 06:00:00]).fee == 8
      assert Passage.new(:car, ~N[2020-02-05 06:29:59]).fee == 8
    end

    test "between 06:30 and 07:00 is 13" do
      assert Passage.new(:car, ~N[2020-02-05 06:30:00]).fee == 13
      assert Passage.new(:car, ~N[2020-02-05 06:59:59]).fee == 13
    end

    test "between 07:00 and 08:00 is 18" do
      assert Passage.new(:car, ~N[2020-02-05 07:00:00]).fee == 18
      assert Passage.new(:car, ~N[2020-02-05 07:59:59]).fee == 18
    end

    test "between 08:00 and 08:30 is 13" do
      assert Passage.new(:car, ~N[2020-02-05 08:00:00]).fee == 13
      assert Passage.new(:car, ~N[2020-02-05 08:29:59]).fee == 13
    end

    test "between 08:30 and 15:00 is 8" do
      assert Passage.new(:car, ~N[2020-02-05 08:30:00]).fee == 8
      assert Passage.new(:car, ~N[2020-02-05 14:59:59]).fee == 8
    end

    test "between 15:00 and 15:30 is 13" do
      assert Passage.new(:car, ~N[2020-02-05 15:00:00]).fee == 13
      assert Passage.new(:car, ~N[2020-02-05 15:29:29]).fee == 13
    end

    test "between 15:30 and 17:00 is 18" do
      assert Passage.new(:car, ~N[2020-02-05 15:30:00]).fee == 18
      assert Passage.new(:car, ~N[2020-02-05 16:59:59]).fee == 18
    end

    test "between 17:00 and 18:00 is 13" do
      assert Passage.new(:car, ~N[2020-02-05 17:00:00]).fee == 13
      assert Passage.new(:car, ~N[2020-02-05 17:59:59]).fee == 13
    end

    test "between 18:00 and 18:30 is 8" do
      assert Passage.new(:car, ~N[2020-02-05 18:00:00]).fee == 8
      assert Passage.new(:car, ~N[2020-02-05 18:29:59]).fee == 8
    end

    test "after 18:30 is 0" do
      assert Passage.new(:car, ~N[2020-02-05 18:30:00]).fee == 0
    end
  end
end
