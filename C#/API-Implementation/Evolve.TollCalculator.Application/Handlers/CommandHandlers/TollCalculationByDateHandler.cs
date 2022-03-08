using Evolve.TollCalculator.Application.Commands;
using Evolve.TollCalculator.Application.Extenstions;
using Evolve.TollCalculator.Application.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Evolve.TollCalculator.Application.Handlers.CommandHandlers
{
    public class TollCalculationByDateHandler : IRequestHandler<TollCalculationByDateCommand, TollCalcultionResponse>
    {
        public Task<TollCalcultionResponse> Handle(TollCalculationByDateCommand request, CancellationToken cancellationToken = default)
        {
            CalculationBehaviour calculationBehaviour = new CalculationBehaviour();

            var tollAmount = calculationBehaviour.GetTollFeeByDate(request.TollDate, request.Vehicle);

            var response = new TollCalcultionResponse()
            {
                TollAmount = tollAmount
            };

            return Task.FromResult(response);
        }
    }
}
