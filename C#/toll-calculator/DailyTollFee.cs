using System;

namespace TollCalculator
{
    /// <summary>
    /// Daily toll fee and taxable amount for a number of passages.
    /// </summary>
    public class DailyTollFee
    {
        private DateTime date;

        /// <summary>
        /// Creates daily toll fee result.
        /// </summary>
        /// <param name="date">Date to create result for.</param>
        /// <param name="numberOfPassages">Number of passages in total for the day.</param>
        /// <param name="totalAmount">
        /// Total amount of toll fees in local currency without taking maximum charges into account.
        /// </param>
        /// <param name="taxableAmount">
        /// Total taxable amount of toll fees in local currency when taking maximum charges into account.
        /// </param>
        public DailyTollFee(DateTime date, int numberOfPassages, decimal totalAmount, decimal taxableAmount)
        {
            Date = date;
            NumberOfPassages = numberOfPassages;
            TotalAmount = totalAmount;
            TaxableAmount = taxableAmount;
        }

        /// <summary>
        /// Gets date that the fee applies for.
        /// </summary>
        public DateTime Date
        {
            get => date;
            private set => date = value.Date;
        }

        /// <summary>
        /// Gets number of passages through toll stations.
        /// </summary>
        public int NumberOfPassages { get;}
        
        /// <summary>
        /// Gets total taxable amount of toll fee in local currency for all toll passages.
        /// </summary>
        /// <remarks>
        /// This DOES take into account maximum charges per day and hour.
        /// </remarks>
        public decimal TaxableAmount { get; }

        /// <summary>
        /// Gets total amount of toll fee in local currency for all toll passages.
        /// </summary>
        /// <remarks>
        /// This DOES NOT take into account highest maximum charges per day and hour.
        /// For the actual amount to be taxed, see <see cref="TaxableAmount"/>.
        /// </remarks>
        public decimal TotalAmount { get; }
    }
}
