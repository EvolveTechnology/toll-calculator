defmodule Toll.Holidays do
  @moduledoc """
  This module implements functionality for checking whether or not a given date
  is a holiday.

  Holidays:
  * New Years Day YYYY-01-01
  * Ephiphany Day YYYY-01-06
  * Good Friday
  * Easter Saturday
  * Easter Sunday
  * Easter Monday
  * May Day YYYY-05-01
  * Ascension Day - 39 days after Easter Sunday
  * Christmas day YYYY-12-25
  * Boxing Day YYYY-12-26

  Pentecostal, Midsummer, and All Saints Day all occur on weekends and need not
  be taken into account.
  """

  @doc """
  Returns whether or not the given datetime is a holiday.
  """
  @spec include?(Date.t()) :: boolean()
  def include?(date) do
    regular_holiday?(date) or irregular_holiday?(date)
  end

  @doc """
  Given a year, this function returns the date of the Easter Sunday for that
  year.

  https://dzone.com/articles/algorithm-calculating-date
  http://en.wikipedia.org/wiki/Computus#Meeus.2FJones.2FButcher_Gregorian_algorithm

  Public for testability reasons.
  """
  @spec easter_sunday(integer()) :: Date.t()
  def easter_sunday(year) do
    a = rem(year, 19)
    b = div(year, 100)
    c = rem(year, 100)
    d = div(b, 4)
    e = rem(b, 4)
    f = div(b + 8, 25)
    g = div(b - f + 1, 3)
    h = rem(19 * a + b - d - g + 15, 30)
    i = div(c, 4)
    k = rem(c, 4)
    l = rem(32 + 2 * e + 2 * i - h - k, 7)
    m = div(a + 11 * h + 22 * l, 451)
    month = div(h + l - 7 * m + 114, 31)
    day = rem(h + l - 7 * m + 114, 31) + 1

    Date.from_erl!({year, month, day})
  end

  defp regular_holiday?(date) do
    case Date.to_erl(date) do
      {_year, 1, 1} -> true
      {_year, 1, 6} -> true
      {_year, 5, 1} -> true
      {_year, 6, 6} -> true
      {_year, 12, 25} -> true
      {_year, 12, 26} -> true
      _other -> false
    end
  end

  defp irregular_holiday?(date) do
    {year, _month, _day} = Date.to_erl(date)
    easter_sunday = easter_sunday(year)
    easter?(date, easter_sunday) or ascension?(date, easter_sunday)
  end

  defp easter?(date, easter_sunday) do
    Enum.any?(-2..1, &(date == Date.add(easter_sunday, &1)))
  end

  defp ascension?(date, easter_sunday) do
    date == Date.add(easter_sunday, 39)
  end
end
