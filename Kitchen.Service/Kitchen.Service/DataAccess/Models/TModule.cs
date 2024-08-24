using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Kitchen.Service.DataAccess.Models
{
    /// <summary>
    /// Класс ModuleCollection представляет коллекцию модулей
    /// </summary>
    public class TModule
    {
        /// <summary>
        /// Идентификатор модуля
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Название модуля
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ширина модуля
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Высота модуля
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Является ли модуль угловым
        /// </summary>
        public bool IsCorner { get; set; }

        /// <summary>
        /// Нужна ли модулю вода
        /// </summary>
        public bool RequiresWater { get; set; }
    }
}
