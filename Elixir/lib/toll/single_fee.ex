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
  @spec calculate(Vehicle.t(), NaiveDateTime.t()) :: integer()
  def calculate(vehicle, datetime) do
    cond do
      exempt_vehicle?(vehicle) -> 0
      weekend?(datetime) -> 0
      holiday?(datetime) -> 0
      true -> calculate(datetime)
    end
  end

  defp exempt_vehicle?(vehicle) do
    Vehicle.exempt?(vehicle)
  end

  defp weekend?(datetime) do
    {{year, month, day}, _time} = NaiveDateTime.to_erl(datetime)
    Calendar.ISO.day_of_week(year, month, day) in [6, 7]
  end

  defp holiday?(datetime) do
    Holidays.include?(datetime)
  end

  # credo:disable-for-lines:16
  defp calculate(datetime) do
    time = NaiveDateTime.to_time(datetime)

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
