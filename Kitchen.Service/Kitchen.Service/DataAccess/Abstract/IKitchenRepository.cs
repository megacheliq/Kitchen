using Kitchen.Service.DataAccess.Models;
using Kitchen.Service.Domain.Kitchen.Models;
using Kitchen.Service.Domain.Kitchen.UseCases.Queries;

namespace Kitchen.Service.DataAccess.Abstract
{
    /// <summary>
    /// Интерфейс репозитория для работы с кухнями
    /// </summary>
    public interface IKitchenRepository
    {
        /// <summary>
        /// Получить все кухни
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Коллекция всех кухонь</returns>
        Task<List<KitchenResponse>> GetAllAsync(AllKitchenQuery request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить информацию по конкретной кухне
        /// </summary>
        /// <param name="id">Идентификатор кухни</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Информация о кухне</returns>
        Task<KitchenResponse> GetByIdOrDefaultAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Создать новую кухню
        /// </summary>
        /// <param name="kitchen">Кухня</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task CreateAsync(TKitchen kitchen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновить существующую кухню
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="dto">Модель для обновления</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task UpdateAsync(string id, AddOrUpdateKitchenDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удалить кухню по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор кухни</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
