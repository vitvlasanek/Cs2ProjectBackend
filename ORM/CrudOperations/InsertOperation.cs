using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitvlasanek.Cs2.Project.Backend.ORM.CrudOperations
{
    public class InsertOperation<T> : ICrudOperation<T> where T : RowDataGatewayBase<T>
    {
        public required IEnumerable<RowDataGatewayBase<T>> RowDataGateways { get; set; }

        public Type ElementType => typeof(T);

        string GetSingleEntityString(RowDataGatewayBase<T> entityGateway, int index)
        {
            var properties = entityGateway.GetPropertiesDictionary(ignorePk: true);

            var sb = new StringBuilder("(null, ")
                .Append(string.Join(", ", properties.Select(_ => $"@{_.Key}_{index}")))
                .Append(")");

            return sb.ToString();
        }


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
                int i = 0;
                foreach (var entity in RowDataGateways)
                {
                    var properties = entity.GetPropertiesDictionary(ignorePk: true);

                    foreach (var p in properties)
                    {
                        var parameter = entity.CreateParameter(command, p, i);
                        command.Parameters.Add(parameter);
                    }
                    i++;
                }

                command.ExecuteNonQuery();
            }


        }

        public override string ToString()
        {
            var list = RowDataGatewayBase<T>.GetPropertyNames(true).ToList();
            int i = 0;

            var sb = new StringBuilder("INSERT INTO ")
                .Append(this.ElementType.Name)
                .Append(" ")
                .Append("(Id, ")
                .Append(string.Join(", ", list))
                .Append(")")
                .Append(" ")
                .Append("VALUES ")
                .Append(string.Join(", ", RowDataGateways.Select(_ => GetSingleEntityString(_, i++))))
                .Append(';');

            return sb.ToString();
        }


    }
}
