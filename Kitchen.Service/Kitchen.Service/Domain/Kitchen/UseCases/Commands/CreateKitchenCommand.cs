using FluentValidation;
using Kitchen.Service.DataAccess.Abstract;
using Kitchen.Service.DataAccess.Models;
using Kitchen.Service.Domain.Kitchen.Models;
using MediatR;

namespace Kitchen.Service.Domain.Kitchen.UseCases.Commands
{
    /// <summary>
    /// Команда создания новой кухни
    /// </summary>
    public class CreateKitchenCommand : IRequest<CreateKitchenResponse>
    {
        /// <summary>
        /// Данные кухни
        /// </summary>
        public AddOrUpdateKitchenDto CommandDto { get; set; }
    }

    public class CreateKitchenCommandHandler : IRequestHandler<CreateKitchenCommand, CreateKitchenResponse>
    {
        private IKitchenRepository KitchenRepository { get; }
        private ILogger<CreateKitchenCommandHandler> Logger { get; }
        private IValidator<CreateKitchenCommand> Validator { get; }

        public CreateKitchenCommandHandler(IKitchenRepository kitchenRepository, ILogger<CreateKitchenCommandHandler> logger, IValidator<CreateKitchenCommand> validator)
        {
            KitchenRepository = kitchenRepository;
            Logger = logger;
            Validator = validator;
        }

        /// <summary>
        /// Обработать команду создания кухни
        /// </summary>
        /// <param name="request">Команда создания кухни</param>
        /// <param name="cancellationToken">Токен отмены</param>
        public async Task<CreateKitchenResponse> Handle(CreateKitchenCommand request,  CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

            var kitchen = new TKitchen
            {
                Width = request.CommandDto.Width,
                Height = request.CommandDto.Height,
                WaterPipe = request.CommandDto.WaterPipe
            };

            await KitchenRepository.CreateAsync(kitchen, cancellationToken);

            Logger.LogInformation($"Kitchen with id {kitchen.Id} was inserted");

            return new CreateKitchenResponse { KitchenId = kitchen.Id};
        }
    }
}
