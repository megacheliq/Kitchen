using FluentValidation;
using Kitchen.Service.DataAccess.Abstract;
using Kitchen.Service.DataAccess.Models;
using Kitchen.Service.Domain.Module.Models;
using MediatR;

namespace Kitchen.Service.Domain.Module.UseCases.Commands
{
    public class CreateModuleCommand : IRequest
    {
        public AddOrUpdateModuleDto CommandDto { get; set; }
    }

    public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand> 
    {
        private IModuleRepository ModuleRepository { get; }
        private IValidator<CreateModuleCommand> Validator { get; }
        private ILogger<CreateModuleCommandHandler> Logger { get; }

        public CreateModuleCommandHandler(IModuleRepository moduleRepository,
                                            IValidator<CreateModuleCommand> validator,
                                            ILogger<CreateModuleCommandHandler> logger) 
        {
            ModuleRepository = moduleRepository;
            Validator = validator;
            Logger = logger;
        }

        public async Task Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

            var module = new TModule
            {
                Name = request.CommandDto.Name,
                Width = request.CommandDto.Width,
                Height = request.CommandDto.Height,
                IsCorner = request.CommandDto.IsCorner,
                RequiresWater = request.CommandDto.RequiresWater,
            };

            await ModuleRepository.CreateAsync(module, cancellationToken);

            Logger.LogInformation($"Module with id {module.Id} was inserted");
        }

    }
}
