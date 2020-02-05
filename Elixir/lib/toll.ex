defmodule Toll do
  @moduledoc false

  @toll_free_vehicles [
    :motorbike,
    :tractor,
    :emergency,
    :diplomat,
    :foreign,
    :military
  ]

  @type vehicle :: atom()

  @spec calculate_fee(vehicle(), list(NaiveDateTime.t())) :: integer()
  def calculate_fee(vehicle, _passages) when vehicle in @toll_free_vehicles do
    0
  end

  def calculate_fee(vehicle, passages) do
    passages
    |> Enum.map(&passage_fee(vehicle, &1))
    |> Enum.sum()
  end

  defp passage_fee(_vehicle, datetime) do
    time = NaiveDateTime.to_time(datetime)
    passage_fee_for_time(time)
  end

  # credo:disable-for-lines:14
  defp passage_fee_for_time(time) do
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
