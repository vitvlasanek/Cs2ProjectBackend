using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitvlasanek.Cs2.Project.Backend.ORM.CrudOperations
{
    public class UpdateOperation<T> : ICrudOperation<T> where T : RowDataGatewayBase<T>
    {
        public required RowDataGatewayBase<T> Instance { get; set; }
        internal HashSet<object> Wheres { get; } = [];

        public Type ElementType => typeof(T);

        public void Execute(IDbConnection dbConnection)
        {
            //if (!this.TableExists((DbConnection)dbConnection))
            //{
            //    this.CreateTable((DbConnection)dbConnection);
            //}

            var dbString = this.ToString();
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = dbString;

                var properties = Instance.GetPropertiesDictionary(ignorePk: true);

                foreach (var p in properties)
                {
                    var parameter = Instance.CreateParameter(command, p);
                    command.Parameters.Add(parameter);
                }

                command.ExecuteNonQuery();
            }


        }

        public override string ToString()
        {
            var list = RowDataGatewayBase<T>.GetPropertyNames(true).ToList();

            var sb = new StringBuilder("UPDATE ")
                .Append(this.ElementType.Name)
                .Append(" ")
                .Append("SET ")
                .Append(string.Join(", ", list.Select(_ => $"{_} = @{_}")))
                .Append(" WHERE Id = ")
                .Append(Instance.Id)
                .Append(';');

            return sb.ToString();
        }
    }
}
