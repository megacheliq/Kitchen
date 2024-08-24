using Kitchen.Service.DataAccess.Abstract;
using MediatR;

namespace Kitchen.Service.Domain.Module.UseCases.Commands
{
    public class DeleteModuleCommand : IRequest
    {
        public string Id { get; set; }
    }

    public class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand> 
    {
        private IModuleRepository Repository { get; }
        private ILogger<DeleteModuleCommandHandler> Logger { get; }

        public DeleteModuleCommandHandler(IModuleRepository repository, ILogger<DeleteModuleCommandHandler> logger)
        {
            Repository = repository;
            Logger = logger;
        }

        public async Task Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Repository.DeleteAsync(request.Id, cancellationToken);

            Logger.LogInformation($"The module with id {request.Id} was deleted");
        }
    }
}
