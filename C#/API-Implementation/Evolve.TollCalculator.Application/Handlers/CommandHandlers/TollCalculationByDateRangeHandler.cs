using Evolve.TollCalculator.Application.Commands;
using Evolve.TollCalculator.Application.Extenstions;
using Evolve.TollCalculator.Application.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Evolve.TollCalculator.Application.Handlers.CommandHandlers
{
    public class TollCalculationByDateRangeHandler : IRequestHandler<TollCalculationByDateRangeCommand, TollCalcultionResponse>
    {
        public Task<TollCalcultionResponse> Handle(TollCalculationByDateRangeCommand request, CancellationToken cancellationToken = default)
        {
            CalculationBehaviour calculationBehaviour = new CalculationBehaviour();

            var tollAmount = calculationBehaviour.GetTollFee(request.Vehicle, request.TollDate);

            var response = new TollCalcultionResponse()
            {
                TollAmount = tollAmount
            };

            return Task.FromResult(response);
        }
    }
}
