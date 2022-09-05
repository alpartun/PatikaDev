using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

using payCoreHW3.Models;

namespace payCoreHW3.Mapping
{
    public class VehicleMap : ClassMapping<Vehicle>
    {
        public VehicleMap()
        {
            //Table name
            Table("vehicle");
            // Id -> long type, default value 0, autoincrement
            Id(x => x.Id,
                x =>
            {
                x.Type(NHibernateUtil.Int64);
                x.Column("id");
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
            } );
            // VehicleName -> maxLength 50, string type
            Property(x => x.VehicleName,
                x =>
                {
                    x.Length(50);
                    x.Type(NHibernateUtil.String);
                });
            // VehiclePlate -> maxLength 14, string type
            Property(x => x.VehiclePlate,
                x =>
                {
                    x.Length(14);
                    x.Type(NHibernateUtil.String);
                });
        }
    }
}

