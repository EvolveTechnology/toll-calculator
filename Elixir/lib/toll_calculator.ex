defmodule TollCalculator do
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

  @spec get_toll_fee(vehicle(), list(NaiveDateTime.t())) :: integer()
  def get_toll_fee(vehicle, passages) do
    passages
    |> Enum.map(&passage_fee(vehicle, &1))
    |> Enum.sum()
  end

  defp passage_fee(vehicle, _datetime) when vehicle in @toll_free_vehicles do
    0
  end
end
