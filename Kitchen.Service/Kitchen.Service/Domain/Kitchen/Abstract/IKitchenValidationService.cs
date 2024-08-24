using Kitchen.Service.DataAccess.Models;
using Kitchen.Service.Domain.Kitchen.Models;

namespace Kitchen.Service.Domain.Kitchen.Abstract
{
    /// <summary>
    /// Интерфейс сервиса для проверки условий установки модуля
    /// </summary>
    public interface IKitchenValidationService
    {
        /// <summary>
        /// Проверка пересекаются ли модули между собой
        /// </summary>
        /// <param name="existingModule">Существующий модуль</param>
        /// <param name="newModule">Модуль для добавления</param>
        /// <returns>Пересекаются ли модули</returns>
        bool ModulesOverlap(PlacedModule existingModule, PlacedModule newModule);
        
        /// <summary>
        /// Проверка вмещается ли модуль в кухню
        /// </summary>
        /// <param name="kitchen">Кухня</param>
        /// <param name="module">Модуль</param>
        /// <returns>Вмещается ли модуль в кухню</returns>
        bool ModuleFitsInKitchen(KitchenResponse kitchen, PlacedModule module);
        
        /// <summary>
        /// Проверка находится ли модуль рядом с источником воды
        /// </summary>
        /// <param name="kitchen">Кухня</param>
        /// <param name="module">Модуль</param>
        /// <param name="radius">Радиус, в котором должен быть источник</param>
        /// <returns>Находится ли модуль рядом с источником воды</returns>
        bool IsNearWaterPipe(KitchenResponse kitchen, PlacedModule module, double radius);
        
        /// <summary>
        /// Проверка находится ли модуль рядом с углом
        /// </summary>
        /// <param name="kitchen">Кухня</param>
        /// <param name="module">Модуль</param>
        /// <param name="radius">Радиус поиска угла</param>
        /// <returns>Находится ли модуль рядом с углом</returns>
        bool IsInCorner(KitchenResponse kitchen, PlacedModule module, double radius);
    }

}
