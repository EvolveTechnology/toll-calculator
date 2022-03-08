using Evolve.TollCalculator.Application.Response;
using Evolve.TollCalculator.Core.Common;
using MediatR;
using System;

namespace Evolve.TollCalculator.Application.Commands
{
    public class TollCalculationByDateCommand : IRequest<TollCalcultionResponse>
    {
        public Vehicle Vehicle { get; set; }
        public DateTime TollDate { get; set; }
    }
}
