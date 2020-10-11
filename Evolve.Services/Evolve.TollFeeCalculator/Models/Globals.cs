using Evolve.TollFeeCalculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.TollFeeCalculator.Models
{
    /// <summary>
    /// Config Class for global variables.
    /// </summary>
    public static class Globals
    {        
        public static IAppConfiguration AppConfiguration { get; set; }
    }
}
