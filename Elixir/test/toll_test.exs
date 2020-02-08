defmodule TollTest do
  use ExUnit.Case

  describe "validation" do
    test "date outside valid range, returns error" do
      passages = [~N[2020-01-01 00:23:48], ~N[2040-01-01 00:00:00]]
      assert Toll.calculate(:car, passages) == {:error, :invalid_datetime}
    end
  end

  describe "corner cases" do
    test "no passages, returns 0" do
      assert Toll.calculate(:car, []) == {:ok, 0}
    end
  end

  describe "exempt vehicles" do
    test "motorbike, returns 0" do
      passages = [~N[2020-02-05 16:00:00]]
      assert Toll.calculate(:motorbike, passages) == {:ok, 0}
    end

    test "tractor, returns 0" do
      passages = [~N[2020-02-05 16:00:00]]
      assert Toll.calculate(:tractor, passages) == {:ok, 0}
    end

    test "emergency, returns 0" do
      passages = [~N[2020-02-05 16:00:00]]
      assert Toll.calculate(:emergency, passages) == {:ok, 0}
    end

    test "diplomat, returns 0" do
      passages = [~N[2020-02-05 16:00:00]]
      assert Toll.calculate(:diplomat, passages) == {:ok, 0}
    end

    test "foreign, returns 0" do
      passages = [~N[2020-02-05 16:00:00]]
      assert Toll.calculate(:foreign, passages) == {:ok, 0}
    end

    test "military, returns 0" do
      passages = [~N[2020-02-05 16:00:00]]
      assert Toll.calculate(:military, passages) == {:ok, 0}
    end
  end

  describe "variable rate" do
    test "before 06:00, returns 0" do
      assert Toll.calculate(:car, [~N[2020-02-05 05:59:59]]) == {:ok, 0}
    end

    test "between 06:00 and 06:30, returns 8" do
      assert Toll.calculate(:car, [~N[2020-02-05 06:00:00]]) == {:ok, 8}
      assert Toll.calculate(:car, [~N[2020-02-05 06:29:59]]) == {:ok, 8}
    end

    test "between 06:30 and 07:00, returns 13" do
      assert Toll.calculate(:car, [~N[2020-02-05 06:30:00]]) == {:ok, 13}
      assert Toll.calculate(:car, [~N[2020-02-05 06:59:59]]) == {:ok, 13}
    end

    test "between 07:00 and 08:00, returns 18" do
      assert Toll.calculate(:car, [~N[2020-02-05 07:00:00]]) == {:ok, 18}
      assert Toll.calculate(:car, [~N[2020-02-05 07:59:59]]) == {:ok, 18}
    end

    test "between 08:00 and 08:30, returns 13" do
      assert Toll.calculate(:car, [~N[2020-02-05 08:00:00]]) == {:ok, 13}
      assert Toll.calculate(:car, [~N[2020-02-05 08:29:59]]) == {:ok, 13}
    end

    test "between 08:30 and 15:00, returns 8" do
      assert Toll.calculate(:car, [~N[2020-02-05 08:30:00]]) == {:ok, 8}
      assert Toll.calculate(:car, [~N[2020-02-05 14:59:59]]) == {:ok, 8}
    end

    test "between 15:00 and 15:30, returns 13" do
      assert Toll.calculate(:car, [~N[2020-02-05 15:00:00]]) == {:ok, 13}
      assert Toll.calculate(:car, [~N[2020-02-05 15:29:59]]) == {:ok, 13}
    end

    test "between 15:30 and 17:00, returns 18" do
      assert Toll.calculate(:car, [~N[2020-02-05 15:30:00]]) == {:ok, 18}
      assert Toll.calculate(:car, [~N[2020-02-05 16:59:59]]) == {:ok, 18}
    end

    test "between 17:00 and 18:00, returns 13" do
      assert Toll.calculate(:car, [~N[2020-02-05 17:00:00]]) == {:ok, 13}
      assert Toll.calculate(:car, [~N[2020-02-05 17:59:59]]) == {:ok, 13}
    end

    test "between 18:00 and 18:30, returns 8" do
      assert Toll.calculate(:car, [~N[2020-02-05 18:00:00]]) == {:ok, 8}
      assert Toll.calculate(:car, [~N[2020-02-05 18:29:59]]) == {:ok, 8}
    end

    test "after 18:30, returns 0" do
      assert Toll.calculate(:car, [~N[2020-02-05 18:30:00]]) == {:ok, 0}
    end
  end

  describe "exempt days" do
    test "weekend, returns 0" do
      assert Toll.calculate(:car, [~N[2020-02-08 15:30:00]]) == {:ok, 0}
      assert Toll.calculate(:car, [~N[2020-02-09 15:30:00]]) == {:ok, 0}
    end

    test "holiday, returns 0" do
      assert Toll.calculate(:car, [~N[2020-06-06 15:30:00]]) == {:ok, 0}
      assert Toll.calculate(:car, [~N[2020-05-01 15:30:00]]) == {:ok, 0}
      assert Toll.calculate(:car, [~N[2020-01-01 15:30:00]]) == {:ok, 0}
    end
  end
end
