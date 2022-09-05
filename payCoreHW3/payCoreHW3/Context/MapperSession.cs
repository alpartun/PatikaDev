using NHibernate;
using payCoreHW3.Models;
using ISession = NHibernate.ISession;

namespace payCoreHW3.Context
{
    // fill our common operations and using ISession(NHiberNate)
    public class MapperSession : IMapperSession
    {
        private readonly ISession _session;
        private ITransaction _transaction;
        public MapperSession(ISession session)
        {
            _session = session;
        }

        public IQueryable<Vehicle> Vehicles => _session.Query<Vehicle>();
        public IQueryable<Container> Containers => _session.Query<Container>();

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            
        }

        public void CloseTransaction()
        {
            _transaction.Dispose();
        }

        public void Save<T>(T entity) where T : class
        {
            _session.Save(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _session.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _session.Delete(entity);
        }

    }
}

