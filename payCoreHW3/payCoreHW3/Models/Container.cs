namespace payCoreHW3.Models
{
    public class Container
    {
        public virtual long Id { get; set; }
        public virtual string ContainerName { get; set; } = null!;
        public virtual decimal Latitude { get; set; }
        public virtual decimal Longitude { get; set; }
        public virtual long VehicleId { get; set; }
    }
}

