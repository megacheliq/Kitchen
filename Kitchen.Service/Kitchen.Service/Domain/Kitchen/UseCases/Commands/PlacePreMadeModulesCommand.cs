using Kitchen.Service.DataAccess.Abstract;
using Kitchen.Service.DataAccess.Models;
using Kitchen.Service.Domain.Kitchen.Abstract;
using Kitchen.Service.Domain.Kitchen.Models;
using Kitchen.Service.Domain.Module.Models;
using MediatR;

namespace Kitchen.Service.Domain.Kitchen.UseCases.Commands
{
    public class PlacePreMadeModulesCommand : IRequest<KitchenResponse>
    {
        /// <summary>
        /// Идентификатор кухни
        /// </summary>
        public string KitchenId { get; set; }
    }

    public class PlacePreMadeModulesCommandHandler : IRequestHandler<PlacePreMadeModulesCommand, KitchenResponse>
    {
        private readonly IKitchenRepository KitchenRepository;
        private readonly IKitchenValidationService ValidationService;

        public PlacePreMadeModulesCommandHandler(IKitchenRepository kitchenRepository, IKitchenValidationService validationService)
        {
            KitchenRepository = kitchenRepository;
            ValidationService = validationService;
        }

        public async Task<KitchenResponse> Handle(PlacePreMadeModulesCommand request, CancellationToken cancellationToken)
        {
            var kitchen = await KitchenRepository.GetByIdOrDefaultAsync(request.KitchenId, cancellationToken);

            if (kitchen is null)
            {
                throw new Exception($"Кухня с id {request.KitchenId} не найдена.");
            }

            // Моковые данные для размещения
            var modulesToPlace = new List<PlacedModule>
            {
                new PlacedModule
                {
                    Module = new ModuleResponse
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Плита",
                        Width = 3,
                        Height = 1,
                        IsCorner = false,
                        RequiresWater = false
                    }
                },
                new PlacedModule
                {
                    Module = new ModuleResponse
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Раковина",
                        Width = 2,
                        Height = 1,
                        IsCorner = false,
                        RequiresWater = true
                    }
                }
            };

            // Ищем оптимальные позиции для размещения модулей
            var placedModules = ValidationService.FindOptimalPositions(kitchen, modulesToPlace);

            var command = new AddOrUpdateKitchenDto
            {
                Height = kitchen.Height,
                Width = kitchen.Width,
                WaterPipe = kitchen.WaterPipe,
                Modules = placedModules
            };

            await KitchenRepository.UpdateAsync(request.KitchenId, command, cancellationToken);

            return await KitchenRepository.GetByIdOrDefaultAsync(kitchen.Id);
        }
    }
}
