using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toll_calculator {
    public class StandardFeePolicy : IFeePolicy {

        private readonly DateTime LOW_MORNING = new DateTime(1, 1, 1, 6, 0, 0);
        private readonly DateTime MEDIUM_MORNING = new DateTime(1, 1, 1, 6, 30, 0);
        private readonly DateTime HIGH_MORNING = new DateTime(1, 1, 1, 7, 0, 0);
        private readonly DateTime MEDIUM_LATE_MORNING = new DateTime(1, 1, 1, 8, 0, 0);
        private readonly DateTime LOW_NOON = new DateTime(1, 1, 1, 8, 30, 0);
        private readonly DateTime MEDIUM_NOON = new DateTime(1, 1, 1, 15, 0, 0);
        private readonly DateTime HIGH_NOON = new DateTime(1, 1, 1, 15, 30, 0);
        private readonly DateTime MEDIUM_AFTERNOON = new DateTime(1, 1, 1, 17, 0, 0);
        private readonly DateTime LOW_AFTERNOON = new DateTime(1, 1, 1, 18, 0, 0);
        private readonly DateTime FREE_AFTERNOON = new DateTime(1, 1, 1, 18, 30, 0);

        public readonly int LOW_FEE = 8;
        public readonly int MEDIUM_FEE = 13;
        public readonly int HIGH_FEE = 18;

        public int GetFee(DateTime time) {
            if (time.TimeOfDay >= LOW_MORNING.TimeOfDay && time.TimeOfDay < MEDIUM_MORNING.TimeOfDay) {
                return LOW_FEE;
            } else if (time.TimeOfDay >= MEDIUM_MORNING.TimeOfDay && time.TimeOfDay < HIGH_MORNING.TimeOfDay) {
                return MEDIUM_FEE;
            } else if (time.TimeOfDay >= HIGH_MORNING.TimeOfDay && time.TimeOfDay < MEDIUM_LATE_MORNING.TimeOfDay) {
                return HIGH_FEE;
            } else if (time.TimeOfDay >= MEDIUM_LATE_MORNING.TimeOfDay && time.TimeOfDay < LOW_NOON.TimeOfDay) {
                return MEDIUM_FEE;
            } else if (time.TimeOfDay >= LOW_NOON.TimeOfDay && time.TimeOfDay < MEDIUM_NOON.TimeOfDay) {
                return LOW_FEE;
            } else if (time.TimeOfDay >= MEDIUM_NOON.TimeOfDay && time.TimeOfDay < HIGH_NOON.TimeOfDay) {
                return MEDIUM_FEE;
            } else if (time.TimeOfDay >= HIGH_NOON.TimeOfDay && time.TimeOfDay < MEDIUM_AFTERNOON.TimeOfDay) {
                return HIGH_FEE;
            } else if (time.TimeOfDay >= MEDIUM_AFTERNOON.TimeOfDay && time.TimeOfDay < LOW_AFTERNOON.TimeOfDay) {
                return MEDIUM_FEE;
            } else if (time.TimeOfDay >= LOW_AFTERNOON.TimeOfDay && time.TimeOfDay < FREE_AFTERNOON.TimeOfDay) {
                return LOW_FEE;
            } else {
                return 0;
            }
        }
    }
}
