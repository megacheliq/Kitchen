using Kitchen.Service.DataAccess.Abstract;
using Kitchen.Service.Domain.Kitchen.Models;
using MediatR;

namespace Kitchen.Service.Domain.Kitchen.UseCases.Queries
{
    public class AllKitchenQuery : IRequest<IEnumerable<KitchenResponse>>
    {

    }

    public class AllKitchenQueryHandler : IRequestHandler<AllKitchenQuery, IEnumerable<KitchenResponse>> 
    {
        private IKitchenRepository Repository { get; }

        public AllKitchenQueryHandler(IKitchenRepository repository)
        {
            Repository = repository;
        }

        public async Task<IEnumerable<KitchenResponse>> Handle(AllKitchenQuery request, CancellationToken cancellationToken)
        {
            return await Repository.GetAllAsync(request, cancellationToken);
        }
    }
}
