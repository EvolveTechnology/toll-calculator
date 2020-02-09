defmodule TollTest do
  use ExUnit.Case, async: true

  describe "validation" do
    test "date outside valid range, returns error" do
      passages = [~N[2020-01-01 00:23:48], ~N[2040-01-01 00:00:00]]
      assert Toll.fee(:car, passages) == {:error, :invalid_datetime}
    end
  end

  describe "corner cases" do
    test "no passages, returns 0" do
      assert Toll.fee(:car, []) == {:ok, 0}
    end

    test "does not charge more than 60 SEK per day" do
      passages = [
        ~N[2020-02-05 06:00:00],
        ~N[2020-02-05 07:01:00],
        ~N[2020-02-05 08:02:00],
        ~N[2020-02-05 09:03:00],
        ~N[2020-02-05 10:04:00],
        ~N[2020-02-05 11:05:00],
        ~N[2020-02-06 11:10:00]
      ]

      assert Toll.fee(:car, passages) == {:ok, 68}
    end
  end
end
