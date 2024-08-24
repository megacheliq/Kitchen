using Kitchen.Service.DataAccess.Models;

namespace Kitchen.Service.Domain.Kitchen.Models
{
    /// <summary>
    /// Класс для представления ответа по взаимодействию с кухнями
    /// </summary>
    public class KitchenResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Ширина
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Высота
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Координаты трубы
        /// </summary>
        public Coordinate WaterPipe { get; set; }

        /// <summary>
        /// Список модулей
        /// </summary>
        public List<PlacedModule> Modules { get; set; } = new List<PlacedModule>();
    }
}