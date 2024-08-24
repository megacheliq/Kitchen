using FluentValidation;
using Kitchen.Service.DataAccess.Abstract;
using Kitchen.Service.Domain.Module.Models;
using Kitchen.Service.Domain.Module.Validators;
using MediatR;

namespace Kitchen.Service.Domain.Module.UseCases.Commands
{
    public class UpdateModuleCommand : IRequest
    {
        public string Id { get; set; }
        public AddOrUpdateModuleDto Dto { get; set; }
    }

    public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand>
    {
        private IModuleRepository Repository { get; }
        private ILogger<UpdateModuleCommandHandler> Logger { get; }
        private IValidator<UpdateModuleCommand> Validator { get; }

        public UpdateModuleCommandHandler(IModuleRepository repository,
            ILogger<UpdateModuleCommandHandler> logger,
            UpdateModuleCommandValidator validator)
        {
            Repository = repository;
            Logger = logger;
            Validator = validator;
        }

        public async Task Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

            await Repository.UpdateAsync(request.Id, request.Dto, cancellationToken);

            Logger.LogInformation($"The module with id {request.Id} was updated");
        }
    }
}
