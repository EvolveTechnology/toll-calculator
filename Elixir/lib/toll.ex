defmodule Toll do
  @moduledoc """
  This module contains the program entry point for calculating toll fees for
  vehicles.
  """
  alias Toll.{
    Holidays,
    SingleFee,
    Vehicle
  }

  @day_max 60

  @doc """
  Given a vehicle and a list of passage times, this function returns the total
  toll fee.
  """
  @spec calculate(Vehicle.t(), list(NaiveDateTime.t())) :: integer()
  def calculate(vehicle, passages) do
    transform = fn passage ->
      date = NaiveDateTime.to_date(passage)
      time = NaiveDateTime.to_time(passage)
      {date, time}
    end

    valid? = fn {date, _time} ->
      Holidays.valid_date?(date)
    end

    passages =
      passages
      |> Enum.sort()
      |> Enum.map(transform)

    case Enum.all?(passages, valid?) do
      true -> {:ok, total_fee(vehicle, passages)}
      false -> {:error, :invalid_datetime}
    end
  end

  defp total_fee(vehicle, passages) do
    case Enum.reduce(passages, :initial, &reduction(vehicle, &1, &2)) do
      {_, _, total} -> total
      :initial -> 0
    end
  end

  defp reduction(vehicle, {date, time}, :initial) do
    nominal = SingleFee.calculate(vehicle, {date, time})
    {date, nominal, nominal}
  end

  defp reduction(vehicle, {date, time}, {date, date_total, total}) do
    nominal = SingleFee.calculate(vehicle, {date, time})
    adjusted = min(@day_max - date_total, nominal)
    {date, date_total + adjusted, total + adjusted}
  end

  defp reduction(vehicle, {date, time}, {_date, _date_total, total}) do
    nominal = SingleFee.calculate(vehicle, {date, time})
    {date, nominal, total + nominal}
  end
end
