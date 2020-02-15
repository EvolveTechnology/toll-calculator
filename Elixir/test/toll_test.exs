defmodule TollTest do
  use ExUnit.Case, async: true

  test "charges 8 SEK between 06:00 and 06:30" do
    assert Toll.fee(:car, [~N[2020-02-05 06:00:00]]) == 8
    assert Toll.fee(:car, [~N[2020-02-05 06:29:59]]) == 8
  end

  test "charges 13 SEK between 06:30 and 07:00" do
    assert Toll.fee(:car, [~N[2020-02-05 06:30:00]]) == 13
    assert Toll.fee(:car, [~N[2020-02-05 06:59:59]]) == 13
  end

  test "charges 18 SEK between 07:00 and 08:00" do
    assert Toll.fee(:car, [~N[2020-02-05 07:00:00]]) == 18
    assert Toll.fee(:car, [~N[2020-02-05 07:59:59]]) == 18
  end

  test "charges 13 SEK between 08:00 and 08:30" do
    assert Toll.fee(:car, [~N[2020-02-05 08:00:00]]) == 13
    assert Toll.fee(:car, [~N[2020-02-05 08:29:59]]) == 13
  end

  test "charges 8 SEK between 08:30 and 15:00" do
    assert Toll.fee(:car, [~N[2020-02-05 08:30:00]]) == 8
    assert Toll.fee(:car, [~N[2020-02-05 14:59:59]]) == 8
  end

  test "charges 13 SEK between 15:00 and 15:30" do
    assert Toll.fee(:car, [~N[2020-02-05 15:00:00]]) == 13
    assert Toll.fee(:car, [~N[2020-02-05 15:29:59]]) == 13
  end

  test "charges 18 SEK between 15:30 and 17:00" do
    assert Toll.fee(:car, [~N[2020-02-05 15:30:00]]) == 18
    assert Toll.fee(:car, [~N[2020-02-05 16:59:59]]) == 18
  end

  test "charges 13 SEK between 17:00 and 18:00" do
    assert Toll.fee(:car, [~N[2020-02-05 17:00:00]]) == 13
    assert Toll.fee(:car, [~N[2020-02-05 17:59:59]]) == 13
  end

  test "charges 8 SEK between 18:00 and 18:30" do
    assert Toll.fee(:car, [~N[2020-02-05 08:30:00]]) == 8
    assert Toll.fee(:car, [~N[2020-02-05 14:59:59]]) == 8
  end

  test "does not charge fee between 18:30 and 06:00 " do
    assert Toll.fee(:car, [~N[2020-02-05 18:30:00]]) == 0
    assert Toll.fee(:car, [~N[2020-02-05 05:59:59]]) == 0
  end

  test "does not charge fee on weekends" do
    assert Toll.fee(:car, [~N[2020-02-16 07:00:00]]) == 0
  end

  test "does not charge fee on holidays" do
    # New years day
    assert Toll.fee(:car, [~N[2020-01-01 07:00:00]]) == 0

    # Epiphany
    assert Toll.fee(:car, [~N[2020-01-06 07:00:00]]) == 0

    # Good Friday
    assert Toll.fee(:car, [~N[2020-04-10 07:00:00]]) == 0

    # Easter Monday
    assert Toll.fee(:car, [~N[2020-04-13 07:00:00]]) == 0

    # May day
    assert Toll.fee(:car, [~N[2020-05-01 07:00:00]]) == 0

    # Ascension
    assert Toll.fee(:car, [~N[2020-05-21 07:00:00]]) == 0

    # National day
    assert Toll.fee(:car, [~N[2020-06-06 07:00:00]]) == 0

    # Christmas day
    assert Toll.fee(:car, [~N[2020-12-25 07:00:00]]) == 0

    # Boxing day
    assert Toll.fee(:car, [~N[2020-12-26 07:00:00]]) == 0
  end

  test "does not charge fee for exempt vehicles" do
    assert Toll.fee(:motorbike, [~N[2020-02-05 08:30:00]]) == 0
    assert Toll.fee(:tractor, [~N[2020-02-05 08:30:00]]) == 0
    assert Toll.fee(:emergency, [~N[2020-02-05 08:30:00]]) == 0
    assert Toll.fee(:diplomat, [~N[2020-02-05 08:30:00]]) == 0
    assert Toll.fee(:foreign, [~N[2020-02-05 08:30:00]]) == 0
    assert Toll.fee(:military, [~N[2020-02-05 08:30:00]]) == 0
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
