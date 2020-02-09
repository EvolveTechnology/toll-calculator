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
    {_, _, total} = Enum.reduce(passages, {:initial, 0, 0}, &reduction/2)
    total
  end

  defp reduction(%{date: date, fee: fee}, {date, date_total, total}) do
    adjusted = min(@day_max - date_total, fee)
    {date, date_total + adjusted, total + adjusted}
  end

  defp reduction(%{date: date, fee: fee}, {_date, _date_total, total}) do
    {date, fee, total + fee}
  end
end
