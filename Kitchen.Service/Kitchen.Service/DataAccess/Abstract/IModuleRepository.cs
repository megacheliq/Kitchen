using Kitchen.Service.DataAccess.Models;
using Kitchen.Service.Domain.Module.Models;
using Kitchen.Service.Domain.Module.UseCases.Queries;

namespace Kitchen.Service.DataAccess.Abstract
{
    /// <summary>
    /// Интерфейс репозитория для работы с модулями
    /// </summary>
    public interface IModuleRepository
    {
        /// <summary>
        /// Получить все модули
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Коллекция всех модулей</returns>
        Task<List<ModuleResponse>> GetAllAsync(AllModuleQuery request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить информацию по конкретному модулю
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Информация о модуле</returns>
        Task<ModuleResponse> GetByIdOrDefaultAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Создать новый модуль
        /// </summary>
        /// <param name="module">Модуль</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task CreateAsync(TModule module, CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновить существующий модуль
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="dto">Модель для обновления</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task UpdateAsync(string id, AddOrUpdateModuleDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удалить модуль по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
