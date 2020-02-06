defmodule Toll do
  @moduledoc false
  alias Toll.Holidays

  @type vehicle :: atom()

  @spec calculate_fee(vehicle(), list(NaiveDateTime.t())) :: integer()
  def calculate_fee(vehicle, passages) do
    with :ok <- validate_passages(passages),
         fee <- calculate_fee_wo_validation(vehicle, passages) do
      {:ok, fee}
    end
  end

  defp validate_passages(passages) do
    case Enum.all?(passages, &datetime_in_range?/1) do
      true -> :ok
      false -> {:error, :invalid_datetime}
    end
  end

  defp calculate_fee_wo_validation(vehicle, passages) do
    passages
    |> Enum.map(&passage_fee(vehicle, &1))
    |> Enum.sum()
  end

  defp passage_fee(vehicle, datetime) do
    cond do
      exempt_vehicle?(vehicle) -> 0
      weekend?(datetime) -> 0
      holiday?(datetime) -> 0
      true -> passage_fee_for_time(datetime)
    end
  end

  # credo:disable-for-lines:16
  defp passage_fee_for_time(datetime) do
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

  defp weekend?(datetime) do
    {{year, month, day}, _time} = NaiveDateTime.to_erl(datetime)
    Calendar.ISO.day_of_week(year, month, day) in [6, 7]
  end

  defp holiday?(datetime) do
    datetime
    |> NaiveDateTime.to_date()
    |> Holidays.include?()
  end

  defp datetime_in_range?(datetime) do
    {{year, _month, _day}, _time} = NaiveDateTime.to_erl(datetime)
    year == 2020
  end

  defp exempt_vehicle?(vehicle) do
    vehicle in [
      :motorbike,
      :tractor,
      :emergency,
      :diplomat,
      :foreign,
      :military
    ]
  end
end
