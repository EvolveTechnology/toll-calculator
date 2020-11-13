using System;
using System.Collections.Generic;
using System.Linq;

namespace toll_calculator
{
    public class TollFeeAggregator : ITollFeeAggregator
    {
        private readonly ITollFeePeriod _feePeriod;

        public TollFeeAggregator(ITollFeePeriod feePeriod)
        {
            _feePeriod = feePeriod;
        }
        public int GetTotalToll(List<DateTime> tollTimeStamps)
        {
            if (tollTimeStamps.Count == 0 || tollTimeStamps == null)
                return 0;

            foreach (DateTime time in tollTimeStamps.OrderBy(x => x)) ;

            List<List<DateTime>> splitList = SplitListOnTimePeriod(tollTimeStamps.OrderBy(x => x).ToList());
            var highestTolls = splitList.Select(x => _feePeriod.GetHighestFeeInPeriod(x));
            return highestTolls.Sum(x => x) > 60 ? 60 : highestTolls.Sum(x => x);
        }

        private List<List<DateTime>> SplitListOnTimePeriod(List<DateTime> dateList)
        {
            var splitLists = new List<List<DateTime>>();
            var partList = new List<DateTime>();
            DateTime partialListStart = dateList[0];
            for (int i = 0; i < dateList.Count; i++)
            {
                if ((dateList[i] - partialListStart).TotalMinutes <= 60)
                {
                    partList.Add(dateList[i]);
                }
                else
                {
                    splitLists.Add(partList);
                    partialListStart = dateList[i];
                    partList = new List<DateTime>() { dateList[i] };
                }
            }
            splitLists.Add(partList);

            return splitLists;
        }
    }
}
