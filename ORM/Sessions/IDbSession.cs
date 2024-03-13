using System.Data;

namespace Vitvlasanek.Cs2.Project.Backend.ORM.Sessions
{
    public interface IDbSession
    {
        IDbConnection Connection { get; init; }
        public bool IsDbCreated();
        public void Insert<T>(T instance) where T : RowDataGatewayBase<T>;
        public void Update<T>(T instance) where T : RowDataGatewayBase<T>;
        public void Delete<T>(T instance) where T : RowDataGatewayBase<T>;
    }
}
