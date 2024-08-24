namespace Kitchen.Service.DataAccess.Models
{
    /// <summary>
    /// Класс для конфигурации базы данных MongoDB
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Название базы данных
        /// </summary>
        public string DatabaseName { get; set; }
    }
}