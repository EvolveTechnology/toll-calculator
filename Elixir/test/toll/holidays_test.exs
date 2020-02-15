defmodule Toll.HolidaysTest do
  use ExUnit.Case, async: true
  alias Toll.Holidays

  describe "include?/1" do
    test "ascension day, returns true" do
      assert Holidays.include?(~D[2020-05-21])
    end

    test "good friday, returns true" do
      assert Holidays.include?(~D[2020-04-10])
    end
  end

  test "easter_sunday/1" do
    assert Holidays.easter_sunday(2013) == ~D[2013-03-31]
    assert Holidays.easter_sunday(2014) == ~D[2014-04-20]
    assert Holidays.easter_sunday(2015) == ~D[2015-04-05]
    assert Holidays.easter_sunday(2016) == ~D[2016-03-27]
    assert Holidays.easter_sunday(2017) == ~D[2017-04-16]
    assert Holidays.easter_sunday(2018) == ~D[2018-04-01]
    assert Holidays.easter_sunday(2019) == ~D[2019-04-21]
    assert Holidays.easter_sunday(2020) == ~D[2020-04-12]
    assert Holidays.easter_sunday(2021) == ~D[2021-04-04]
    assert Holidays.easter_sunday(2022) == ~D[2022-04-17]
    assert Holidays.easter_sunday(2023) == ~D[2023-04-09]
    assert Holidays.easter_sunday(2024) == ~D[2024-03-31]
    assert Holidays.easter_sunday(2025) == ~D[2025-04-20]
    assert Holidays.easter_sunday(2026) == ~D[2026-04-05]
    assert Holidays.easter_sunday(2027) == ~D[2027-03-28]
    assert Holidays.easter_sunday(2028) == ~D[2028-04-16]
    assert Holidays.easter_sunday(2029) == ~D[2029-04-01]
    assert Holidays.easter_sunday(2030) == ~D[2030-04-21]
    assert Holidays.easter_sunday(2031) == ~D[2031-04-13]
  end
end
