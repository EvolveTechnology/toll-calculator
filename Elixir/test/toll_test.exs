defmodule TollTest do
  use ExUnit.Case, async: true

  describe "corner cases" do
    test "no passages, returns 0" do
      assert Toll.fee(:car, []) == 0
    end

    test "does not charge more than 60 SEK per day" do
      passages = [
        # 8 SEK, day total: 8
        ~N[2020-02-05 06:00:00],

        # 18 SEK, day total: 26
        ~N[2020-02-05 07:01:00],

        # 13 SEK, day total: 39
        ~N[2020-02-05 08:02:00],

        # 8 SEK, day total: 47
        ~N[2020-02-05 09:03:00],

        # 8 SEK, day total: 55
        ~N[2020-02-05 10:04:00],

        # 8 SEK, day total: 63, truncated to 60
        ~N[2020-02-05 11:05:00],

        # Next day, 8 SEK
        ~N[2020-02-06 11:10:00]
      ]

      assert Toll.fee(:car, passages) == 60 + 8
    end

    test "only applies the highest charge for passages within an hour" do
      passages = [
        # 8 SEK, ignored
        ~N[2020-02-05 06:29:54],

        # 13 SEK, ignored
        ~N[2020-02-05 06:34:43],

        # 18 SEK, applied
        ~N[2020-02-05 07:29:12],

        # 13 SEK, applied since after one hour from first passage
        ~N[2020-02-05 08:01:29]
      ]

      assert Toll.fee(:car, passages) == 18 + 13
    end
  end
end
