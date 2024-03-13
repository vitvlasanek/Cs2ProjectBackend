using System.Data;
using Vitvlasanek.Cs2.Project.Backend.ORM.CrudOperations;

namespace Vitvlasanek.Cs2.Project.Backend.ORM.Sessions
{
    public class BaseDbSession(IDbConnection connection) : IDbSession, IDisposable
    {

        public IDbConnection Connection { get; init; } = connection;

        public void Insert<T>(T instance) where T : RowDataGatewayBase<T>
        {
            Connection.Open();

            var insertOp = new InsertOperation<T>() { RowDataGateways = new List<T> { instance } };
            insertOp.Execute(Connection);

            Connection.Close();
        }

        public bool IsDbCreated()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }

        public void Update<T>(T instance) where T : RowDataGatewayBase<T>
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T instance) where T : RowDataGatewayBase<T>
        {
            throw new NotImplementedException();
        }
    }
}
