using System;

namespace TollFeeCalculator.Contracts.TaxationTimespan
{
	/// <summary>
	/// Represents a timespan with a cost
	/// </summary>
	public class TaxationTimespan
	{
		/// <summary>
		/// Start of the timespan
		/// </summary>
		public TimeSpan TimespanStart { get; set; }
		/// <summary>
		/// End of the timespan
		/// </summary>
		public TimeSpan TimespanEnd { get; set; }
		/// <summary>
		/// Cost of the timespan
		/// </summary>
		public float TimespanFee { get; set; }
	}
}
