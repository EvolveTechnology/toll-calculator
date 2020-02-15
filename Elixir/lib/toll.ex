defmodule Toll do
  @moduledoc """
  This module contains the program entry point for calculating toll fees for
  vehicles.
  """
  alias Toll.Holidays

  # Maximum total charge per day
  @day_max 60

  @type vehicle() :: atom()
  @type passage() :: NaiveDateTime.t()

  @exempt_vehicles [
    :motorbike,
    :tractor,
    :emergency,
    :diplomat,
    :foreign,
    :military
  ]

  @doc """
  Given a vehicle and a list of passage times, this function returns the total
  toll fee.
  """
  @spec fee(vehicle(), list(passage())) :: integer()
  def fee(vehicle, passages) do
    case vehicle in @exempt_vehicles do
      true -> 0
      false -> calculate(passages)
    end
  end

  defp calculate(passages) do
    passages
    |> group_by_day()
    |> reject_holidays_and_weekends()
    |> calculate_and_sum_days()
  end

  defp group_by_day(passages) do
    grouping = &NaiveDateTime.to_date/1
    mapping = &NaiveDateTime.to_time/1
    Enum.group_by(passages, grouping, mapping)
  end

  defp reject_holidays_and_weekends(passages_by_date) do
    exempt? = fn {date, _} -> holiday?(date) or weekend?(date) end
    Enum.reject(passages_by_date, exempt?)
  end

  defp calculate_and_sum_days(passages_by_date) do
    passages_by_date
    |> Enum.map(&calculate_day/1)
    |> Enum.sum()
  end

  defp calculate_day({_date, passages}) do
    passages
    |> reject_exempt_times()
    |> group_by_hour()
    |> calculate_and_sum_hours()
    |> min(@day_max)
  end

  # Given a list of times, removes all times outside of billable hours. This
  # must be done in order to not mess up the calculation per hour.
  defp reject_exempt_times(passages) do
    Enum.reject(passages, &(nominal_fee(&1) == 0))
  end

  # Given a list of times, this function returns a map
  # (first time in window => list of times within an hour of the first time)
  #
  # Example:
  #
  # Input: [08:00, 08:45, 09:30, 10:31, 11:29]
  # Output: %{
  #   08:00 => [08:00, 08:45],
  #   09:30 => [09:30],
  #   10:31 => [10:31, 11:29]
  # }
  defp group_by_hour(passages) do
    hour_group = fn passage, _accumulator = {start, groups} ->
      case within_hour?(passage, start) do
        true -> {start, Map.update!(groups, start, fn passages -> [passage | passages] end)}
        false -> {passage, Map.put(groups, passage, [passage])}
      end
    end

    {_, grouped} =
      passages
      |> Enum.sort()
      |> Enum.reduce(_accumulator = {nil, %{}}, hour_group)

    grouped
  end

  defp calculate_and_sum_hours(passages_by_hour) do
    passages_by_hour
    |> Enum.map(&max_fee_for_hour/1)
    |> Enum.sum()
  end

  defp max_fee_for_hour({_first, passages}) do
    passages
    |> Enum.map(&nominal_fee/1)
    |> Enum.max()
  end

  # credo:disable-for-lines:14
  defp nominal_fee(time) do
    cond do
      time_in(time, ~T[06:00:00], ~T[06:30:00]) -> 8
      time_in(time, ~T[06:30:00], ~T[07:00:00]) -> 13
      time_in(time, ~T[07:00:00], ~T[08:00:00]) -> 18
      time_in(time, ~T[08:00:00], ~T[08:30:00]) -> 13
      time_in(time, ~T[08:30:00], ~T[15:00:00]) -> 8
      time_in(time, ~T[15:00:00], ~T[15:30:00]) -> 13
      time_in(time, ~T[15:30:00], ~T[17:00:00]) -> 18
      time_in(time, ~T[17:00:00], ~T[18:00:00]) -> 13
      time_in(time, ~T[18:00:00], ~T[18:30:00]) -> 8
      true -> 0
    end
  end

  defp time_in(time, from, to) do
    (Time.compare(time, from) == :eq or Time.compare(time, from) == :gt) and
      Time.compare(time, to) == :lt
  end

  defp within_hour?(t1, t2) do
    case {t1, t2} do
      {t1, t2} when not is_nil(t1) and not is_nil(t2) -> Time.diff(t1, t2) < 3600
      _other -> false
    end
  end

  defp holiday?(date) do
    Holidays.include?(date)
  end

  defp weekend?(date) do
    {year, month, day} = Date.to_erl(date)
    Calendar.ISO.day_of_week(year, month, day) in [6, 7]
  end
end
