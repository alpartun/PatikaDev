namespace payCoreHW3.Models
{
    public class Vehicle
    {
        public virtual long Id { get; set; }
        public virtual string VehicleName { get; set; } = null!;
        public virtual string VehiclePlate { get; set; } = null!;
    }
}

