using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toll_calculator {

    interface ITollFeePolicy {

        int GetFee(DateTime time);

    }
}
