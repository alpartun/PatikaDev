
using payCoreHW3.Models;

namespace payCoreHW3.Context
{
    public interface IMapperSession 
    {
        // creating our common operations
        void BeginTransaction();
        void Commit();
        void Rollback();
        void CloseTransaction();
        void Save<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        
        IQueryable<Vehicle> Vehicles { get; }
        IQueryable<Container> Containers { get; }


    }




}

