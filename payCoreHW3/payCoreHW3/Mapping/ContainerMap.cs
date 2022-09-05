using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using payCoreHW3.Models;

namespace payCoreHW3.Mapping
{
    public class ContainerMap : ClassMapping<Container>
    {
        public ContainerMap() 
        {
            // Table name
            Table("container");
            //Id->long type, autoincrement, default 0
            Id(x => x.Id,
                x =>
                {
                    x.Type(NHibernateUtil.Int64);
                    x.Column("id");
                    x.UnsavedValue(0);
                    x.Generator(Generators.Increment);
                } );
            // ContainerName -> maxlength 50, string type
            Property(x => x.ContainerName,
                x =>
                {
                    x.Length(50);
                    x.Type(NHibernateUtil.String);
                });
            // Latitude -> decimal type(currency), precision 10, scale 6
            Property(x => x.Latitude,
                x =>
                {
                    x.Type(NHibernateUtil.Currency);
                    x.Precision(10);
                    x.Scale(6);
                });
            // Longitude -> decimal type(currency), precision 10,scale 6
            Property(x => x.Longitude,
                x =>
                {
                    x.Type(NHibernateUtil.Currency);
                    x.Precision(10);
                    x.Scale(6);
                });
            // VehicleId -> long type
            Property(x => x.VehicleId,
                x =>
                {
                    x.Type(NHibernateUtil.Int64);
                });


        }
    }
}

