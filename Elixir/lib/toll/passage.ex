defmodule Toll.Passage do
  @moduledoc """
  This module implements a data type for passages.
  """
  alias Toll.{
    Holidays,
    Vehicle
  }

  defstruct [:date, :time, :fee]

  @typedoc """
  Type representing a passage:
  * date - the date of passage, as a Date struct
  * time - unix timestamp of the passage (seconds since 1970-01-01)
  * fee - the nominal passage fee (not adjusted for previous passages within an
          hour etc.)
  """
  @type t() :: %__MODULE__{
          date: Date.t(),
          time: integer(),
          fee: integer()
        }

  @doc """
  Given a vehicle and a datetime, returns the corresponding passage struct.
  """
  @spec new(Vehicle.t(), DateTime.t()) :: t()
  def new(vehicle, datetime) do
    date = NaiveDateTime.to_date(datetime)
    time = NaiveDateTime.to_time(datetime)
    unix = NaiveDateTime.diff(datetime, ~N[1970-01-01 00:00:00], :second)
    fee = fee(vehicle, date, time)
    %__MODULE__{date: date, time: unix, fee: fee}
  end

  defp fee(vehicle, date, time) do
    cond do
      exempt_vehicle?(vehicle) -> 0
      weekend?(date) -> 0
      holiday?(date) -> 0
      true -> fee(time)
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
  defp fee(time) do
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
