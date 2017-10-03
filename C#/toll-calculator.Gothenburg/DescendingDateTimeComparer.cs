using System;
using System.Collections.Generic;

namespace TollCalculator.Gothenburg
{
    /// <summary>
    /// Comparer for <see cref="DateTime"/> in descending order.
    /// </summary>
    internal class DescendingDateTimeComparer : IComparer<DateTime>
    {
        /// <summary>
        /// Compare <see cref="DateTime"/> and return in descending order.
        /// </summary>
        /// <param name="x">First comparand.</param>
        /// <param name="y">Second comparand.</param>
        /// <returns>Returns <b>-1</b> if <paramref name="x"/> is greater than <paramref name="y"/>,
        /// returns <b>0</b> if they are equal and <b>1</b> otherwise.
        /// </returns>
        public int Compare(DateTime x, DateTime y) => x > y ? -1 : x == y ? 0 : 1;
    }
}
