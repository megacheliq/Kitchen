namespace Kitchen.Service.Domain.Module.Models
{
    public class ModuleResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsCorner { get; set; }
        public bool RequiresWater { get; set; }
    }
}