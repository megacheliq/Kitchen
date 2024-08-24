namespace Kitchen.Service.DataAccess.Models
{
    /// <summary>
    /// Класс конфигурации настроек CORS
    /// </summary>
    public class CorsSettings
    {
        /// <summary>
        /// Разрешенные источники
        /// </summary>
        public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
    }
}
