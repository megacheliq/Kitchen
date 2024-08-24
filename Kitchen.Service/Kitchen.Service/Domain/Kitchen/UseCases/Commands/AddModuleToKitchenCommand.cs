using Kitchen.Service.DataAccess.Abstract;
using Kitchen.Service.DataAccess.Models;
using Kitchen.Service.Domain.Kitchen.Abstract;
using Kitchen.Service.Domain.Kitchen.Models;
using MediatR;

namespace Kitchen.Service.Domain.Kitchen.UseCases.Commands
{
    /// <summary>
    /// Команда добавления модуля в кухню
    /// </summary>
    public class AddModuleToKitchenCommand : IRequest<KitchenResponse>
    {
        /// <summary>
        /// Идентификатор кухни
        /// </summary>
        public string KitchenId { get; set; }

        /// <summary>
        /// Модуль кухни
        /// </summary>
        public string ModuleId { get; set; }

        /// <summary>
        /// Координаты модуля
        /// </summary>
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Ориентация модуля в пространстве
        /// </summary>
        public Orientation Orientation { get; set; }
    }

    public class AddModuleToKitchenCommandHandler : IRequestHandler<AddModuleToKitchenCommand, KitchenResponse>
    {
        private readonly IKitchenRepository KitchenRepository;
        private readonly IModuleRepository ModuleRepository;
        private readonly IKitchenValidationService ValidationService;

        public AddModuleToKitchenCommandHandler(
            IKitchenRepository kitchenRepository,
            IModuleRepository moduleRepository,
            IKitchenValidationService validationService)
        {
            KitchenRepository = kitchenRepository;
            ModuleRepository = moduleRepository;
            ValidationService = validationService;
        }

        /// <summary>
        /// Обработать команду добавления модуля
        /// </summary>
        /// <param name="request">Команда добавления модуля</param>
        /// <param name="cancellationToken">Токен отмены</param>
        public async Task<KitchenResponse> Handle(AddModuleToKitchenCommand request, CancellationToken cancellationToken)
        {
            var kitchen = await KitchenRepository.GetByIdOrDefaultAsync(request.KitchenId, cancellationToken);
            var parentModule = await ModuleRepository.GetByIdOrDefaultAsync(request.ModuleId, cancellationToken);

            if (kitchen is null)
            {
                throw new Exception($"Кухня с id {request.KitchenId} не найдена.");
            }

            if (parentModule is null)
            {
                throw new Exception($"Модуль с id {request.ModuleId} не найден.");
            }

            var moduleWidth = parentModule.Width;
            var moduleHeight = parentModule.Height;

            if (request.Orientation == Orientation.Vertical)
            {
                moduleWidth = parentModule.Height;
                moduleHeight = parentModule.Width;
            }

            parentModule.Width = moduleWidth;
            parentModule.Height = moduleHeight;

            var module = new PlacedModule
            {
                Module = parentModule,
                Coordinate = request.Coordinate,
                Orientation = request.Orientation
            };

            foreach (var placedModule in kitchen.Modules)
            {
                if (ValidationService.ModulesOverlap(placedModule, module))
                {
                    throw new Exception($"Модуль пересекается с другим модулем.");
                }
            }

            if (!ValidationService.ModuleFitsInKitchen(kitchen, module))
            {
                throw new Exception($"Модуль выходит за границы кухни.");
            }

            if (module.Module.RequiresWater && !ValidationService.IsNearWaterPipe(kitchen, module, 0.5))
            {
                throw new Exception($"Модуль должен находиться в радиусе 0.5 метра от трубы.");
            }

            if (module.Module.IsCorner && !ValidationService.IsInCorner(kitchen, module, 0.5))
            {
                throw new Exception($"Угловой модуль должен находиться в радиусе 0.5 метра от угла.");
            }

            kitchen.Modules.Add(module);

            var command = new AddOrUpdateKitchenDto
            {
                Height = kitchen.Height,
                Width = kitchen.Width,
                WaterPipe = kitchen.WaterPipe,
                Modules = kitchen.Modules
            };

            await KitchenRepository.UpdateAsync(request.KitchenId, command, cancellationToken);

            return await KitchenRepository.GetByIdOrDefaultAsync(kitchen.Id);
        }
    }
}
