defmodule Toll.Vehicle do
  @moduledoc """
  This module implements functionality around vehicles.
  """

  @typedoc """
  Type representing a vehicle.
  """
  @type t() :: atom()

  @doc """
  Given a vehicle, this function returns whether or not the vehicle is exempt
  from toll fees.
  """
  @spec exempt?(t()) :: boolean()
  def exempt?(vehicle) do
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
