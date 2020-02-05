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
  def calculate_fee(vehicle, passages) do
    passages
    |> Enum.map(&passage_fee(vehicle, &1))
    |> Enum.sum()
  end

  defp passage_fee(vehicle, _datetime) when vehicle in @toll_free_vehicles do
    0
  end
end
