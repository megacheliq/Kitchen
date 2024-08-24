using Kitchen.Service.DataAccess.Models;

namespace Kitchen.Service.Domain.Kitchen.Models
{
    /// <summary>
    /// Класс для представления команды кухни
    /// </summary>
    public class AddOrUpdateKitchenDto
    {
        /// <summary>
        /// Ширина
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Высота
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Расположение трубы
        /// </summary>
        public Coordinate WaterPipe { get; set; }

        /// <summary>
        /// Список модулей
        /// </summary>
        public List<PlacedModule> Modules { get; set; } = new List<PlacedModule>();
    }
}
