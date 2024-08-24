namespace Kitchen.Service.Domain.Module.Models
{
    public class AddOrUpdateModuleDto
    {
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsCorner { get; set; }
        public bool RequiresWater { get; set; }
    }
}
