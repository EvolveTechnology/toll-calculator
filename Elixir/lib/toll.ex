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

  @doc """
  Given a vehicle and a list of passage times, this function returns the total
  toll fee.
  """
  @spec calculate(Vehicle.t(), list(NaiveDateTime.t())) :: integer()
  def calculate(vehicle, passages) do
    with {:ok, passages} <- validate(passages) do
      {:ok, total_fee(vehicle, passages)}
    end
  end

  defp validate(passages) do
    case Enum.all?(passages, &Holidays.valid_datetime?/1) do
      true -> {:ok, Enum.sort(passages)}
      false -> {:error, :invalid_datetime}
    end
  end

  defp total_fee(vehicle, passages) do
    passages
    |> Enum.map(&single_fee(vehicle, &1))
    |> Enum.sum()
  end

  defp single_fee(vehicle, datetime) do
    SingleFee.calculate(vehicle, datetime)
  end
end
