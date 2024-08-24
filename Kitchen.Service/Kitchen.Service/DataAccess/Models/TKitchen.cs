using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Kitchen.Service.DataAccess.Models
{
    /// <summary>
    /// Класс KitchenCollection представляет коллекцию кухонь
    /// </summary>
    public class TKitchen
    {
        /// <summary>
        /// Идентификатор кухни
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Ширина кухни
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Высота кухни
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Расположение трубы
        /// </summary>
        public Coordinate WaterPipe { get; set; }

        /// <summary>
        /// Список установленных модулей
        /// </summary>
        public List<PlacedModule> Modules { get; set; } = new List<PlacedModule>();
    }
}
