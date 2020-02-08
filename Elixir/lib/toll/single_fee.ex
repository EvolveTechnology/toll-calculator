defmodule Toll.SingleFee do
  @moduledoc """
  This module implements calculation of the toll fee for a single passage.
  """
  alias Toll.{
    Holidays,
    Vehicle
  }

  @doc """
  Given a vehicle and a passage, this function returns the corresponding toll
  fee.
  """
  @spec calculate(Vehicle.t(), {Date.t(), Time.t()}) :: integer()
  def calculate(vehicle, {date, time}) do
    cond do
      exempt_vehicle?(vehicle) -> 0
      weekend?(date) -> 0
      holiday?(date) -> 0
      true -> calculate(time)
    end
  end

  defp exempt_vehicle?(vehicle) do
    Vehicle.exempt?(vehicle)
  end

  defp weekend?(date) do
    {year, month, day} = Date.to_erl(date)
    Calendar.ISO.day_of_week(year, month, day) in [6, 7]
  end

  defp holiday?(date) do
    Holidays.include?(date)
  end

  # credo:disable-for-lines:14
  defp calculate(time) do
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
end
