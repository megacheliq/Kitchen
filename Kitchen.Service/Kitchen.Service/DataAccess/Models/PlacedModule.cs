using Kitchen.Service.Domain.Module.Models;

namespace Kitchen.Service.DataAccess.Models
{
    /// <summary>
    /// Класс для хранения установленного модуля в кухне
    /// </summary>
    public class PlacedModule
    {
        /// <summary>
        /// Модуль
        /// </summary>
        public ModuleResponse Module { get; set; }

        /// <summary>
        /// Координаты
        /// </summary>
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Ориентация в пространстве
        /// </summary>
        public Orientation Orientation { get; set; }
    }
}
