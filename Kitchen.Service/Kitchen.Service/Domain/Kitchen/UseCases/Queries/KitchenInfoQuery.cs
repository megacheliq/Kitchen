using Kitchen.Service.DataAccess.Abstract;
using Kitchen.Service.Domain.Kitchen.Models;
using MediatR;

namespace Kitchen.Service.Domain.Kitchen.UseCases.Queries
{
    /// <summary>
    /// Запрос на получения информации по кухне по идентификатору
    /// </summary>
    public class KitchenInfoQuery : IRequest<KitchenResponse>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }
    }

    public class KitchenInfoQueryHandler : IRequestHandler<KitchenInfoQuery, KitchenResponse>
    {
        private IKitchenRepository Repository { get; }

        public KitchenInfoQueryHandler(IKitchenRepository repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Обработать запрос на получение информации по кухне
        /// </summary>
        /// <param name="request">Запрос на получение информации</param>
        /// <param name="cancellationToken">Токен отмены</param>
        public async Task<KitchenResponse> Handle(KitchenInfoQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await Repository.GetByIdOrDefaultAsync(request.Id, cancellationToken);
        }
    }
}
