defmodule Toll do
  @moduledoc """
  This module contains the program entry point for calculating toll fees for
  vehicles.
  """
  alias Toll.{
    Holidays,
    Passage,
    Vehicle
  }

  @day_max 60
  @one_hour 3600

  # Struct passed as accumulator to the reduction function used when summing up
  # the total fee.
  defstruct(
    # Date of the previous passage.
    date: nil,

    # Unix timestamp of the previous passage (seconds since 1970-01-01).
    time: nil,

    # Total fee accumulated for the current hour window.
    hour_total: 0,

    # Total fee accumulated for the current day.
    day_total: 0,

    # Total fee accumulated.
    total: 0
  )

  @doc """
  Given a vehicle and a list of passage times, this function returns the total
  toll fee.
  """
  @spec fee(Vehicle.t(), list(NaiveDateTime.t())) :: integer()
  def fee(vehicle, passages) do
    with passages <- format(vehicle, passages),
         :ok <- validate(passages),
         result <- calculate(passages) do
      {:ok, result}
    end
  end

  defp format(vehicle, passages) do
    passages
    |> Enum.sort()
    |> Enum.map(&Passage.new(vehicle, &1))
  end

  defp validate(passages) do
    case Enum.all?(passages, &Holidays.valid_date?(&1.date)) do
      true -> :ok
      false -> {:error, :invalid_datetime}
    end
  end

  defp calculate(passages) do
    Enum.reduce(passages, %__MODULE__{}, &apply_fee/2).total
  end

  defp apply_fee(passage, accumulator) do
    cond do
      passage.date == accumulator.date and passage.time - accumulator.time < @one_hour ->
        apply_within_hour(passage, accumulator)

      passage.date == accumulator.date ->
        apply_within_day(passage, accumulator)

      true ->
        apply_separate_days(passage, accumulator)
    end
  end

  defp apply_within_hour(passage, accumulator) do
    delta = min(@day_max - accumulator.day_total, passage.fee)
    delta = max(delta - accumulator.hour_total, 0)

    accumulator
    |> Map.put(:hour_total, accumulator.hour_total + delta)
    |> Map.put(:day_total, accumulator.day_total + delta)
    |> Map.put(:total, accumulator.total + delta)
  end

  defp apply_within_day(passage, accumulator) do
    delta = min(@day_max - accumulator.day_total, passage.fee)

    accumulator
    |> Map.put(:time, passage.time)
    |> Map.put(:hour_total, passage.fee)
    |> Map.put(:day_total, accumulator.day_total + delta)
    |> Map.put(:total, accumulator.total + delta)
  end

  defp apply_separate_days(passage, accumulator) do
    accumulator
    |> Map.put(:date, passage.date)
    |> Map.put(:time, passage.time)
    |> Map.put(:hour_total, passage.fee)
    |> Map.put(:day_total, passage.fee)
    |> Map.put(:total, accumulator.total + passage.fee)
  end
end
