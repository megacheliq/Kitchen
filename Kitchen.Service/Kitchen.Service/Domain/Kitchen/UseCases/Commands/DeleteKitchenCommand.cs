using Kitchen.Service.DataAccess.Abstract;
using MediatR;

namespace Kitchen.Service.Domain.Kitchen.UseCases.Commands
{
    /// <summary>
    /// Команда удаления кухни
    /// </summary>
    public class DeleteKitchenCommand : IRequest
    {
        /// <summary>
        /// Идентификатор кухни
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteKitchenCommandHandler : IRequestHandler<DeleteKitchenCommand> 
    {
        private IKitchenRepository Repository { get; }
        private ILogger<DeleteKitchenCommandHandler> Logger { get; }

        public DeleteKitchenCommandHandler(IKitchenRepository repository, ILogger<DeleteKitchenCommandHandler> logger)
        {
            Repository = repository;
            Logger = logger;
        }

        /// <summary>
        /// Обработать команду удаления кухни
        /// </summary>
        /// <param name="request">Команда удаления кухни</param>
        /// <param name="cancellationToken">Токен отмены</param>
        public async Task Handle(DeleteKitchenCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Repository.DeleteAsync(request.Id, cancellationToken);

            Logger.LogInformation($"The kitchen with id {request.Id} was deleted");
        }
    }

}
