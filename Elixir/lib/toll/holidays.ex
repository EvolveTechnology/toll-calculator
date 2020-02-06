defmodule Toll.Holidays do
  @moduledoc """
  This module implements functionality for checking whether or not a given date
  is a holiday.
  """

  @holidays [
    # 2020
    ~D[2020-01-01],
    ~D[2020-01-06],
    ~D[2020-04-10],
    ~D[2020-04-11],
    ~D[2020-04-12],
    ~D[2020-04-13],
    ~D[2020-05-01],
    ~D[2020-05-21],
    ~D[2020-05-30],
    ~D[2020-05-31],
    ~D[2020-06-06],
    ~D[2020-06-19],
    ~D[2020-06-20],
    ~D[2020-10-31],
    ~D[2020-12-24],
    ~D[2020-12-25],
    ~D[2020-12-26],
    ~D[2020-12-31]
  ]

  @doc """
  Returns true if the given date is a holiday
  """
  @spec include?(Date.t()) :: boolean()
  def include?(date) do
    date in @holidays
  end
end
