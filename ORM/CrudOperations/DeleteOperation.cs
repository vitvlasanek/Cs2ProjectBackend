using System.Data;
using System.Text;

namespace Vitvlasanek.Cs2.Project.Backend.ORM.CrudOperations
{
    internal class DeleteOperation<T> : ICrudOperation<T> where T : RowDataGatewayBase<T>
    {
        public required RowDataGatewayBase<T> Instance { get; set; }
        public Type ElementType => typeof(T);

        public void Execute(IDbConnection dbConnection)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = this.ToString();
                command.ExecuteNonQuery();
            }
        }

        public override string ToString()
        {
            var list = RowDataGatewayBase<T>.GetPropertyNames(true).ToList();

            var sb = new StringBuilder("DELETE FROM ")
                .Append(this.ElementType.Name)
                .Append(" WHERE Id = ")
                .Append(Instance.Id)
                .Append(';');

            return sb.ToString();
        }
    }
}
