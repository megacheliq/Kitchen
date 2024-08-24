using FluentValidation;
using Kitchen.Service.DataAccess.Abstract;
using Kitchen.Service.Domain.Kitchen.Models;
using MediatR;

namespace Kitchen.Service.Domain.Kitchen.UseCases.Commands
{
    /// <summary>
    /// Команда обновления кухни
    /// </summary>
    public class UpdateKitchenCommand : IRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Данные для обновления
        /// </summary>
        public AddOrUpdateKitchenDto Dto { get; set; }
    }

    public class UpdateKitchenCommandHandler : IRequestHandler<UpdateKitchenCommand> 
    {
        private IKitchenRepository Repository { get; }
        private ILogger<UpdateKitchenCommandHandler> Logger { get; }
        private IValidator<UpdateKitchenCommand> Validator { get; }

        public UpdateKitchenCommandHandler(IKitchenRepository repository, ILogger<UpdateKitchenCommandHandler> logger, IValidator<UpdateKitchenCommand> validator)
        {
            Repository = repository;
            Logger = logger;
            Validator = validator;
        }

        /// <summary>
        /// Обработать команду обновления кухни
        /// </summary>
        /// <param name="request">Команда обновления кухни</param>
        /// <param name="cancellationToken">Токен отмены</param>
        public async Task Handle(UpdateKitchenCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

            await Repository.UpdateAsync(request.Id, request.Dto, cancellationToken);

            Logger.LogInformation($"The kitchen with id {request.Id} was updated");
        }
    }
}
