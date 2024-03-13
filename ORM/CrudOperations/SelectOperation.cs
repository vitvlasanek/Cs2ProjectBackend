using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitvlasanek.Cs2.Project.Backend.ORM.CrudOperations
{
    public class SelectOperation<T> : ICrudOperation<T> where T : RowDataGatewayBase<T>, new()
    {
        public Type ElementType => typeof(T);

        internal string What { get; set; } = " *";
        internal HashSet<object> Wheres { get; } = [];
        internal string? GroupBy { get; set; }
        internal string? Having { get; set; }
        internal string? OrderBy { get; set; }

        public static SqlCommandSelect<T> SelectAsQuery()
        {
            return new SqlCommandSelect<T>(SqlSyntaxes.SqlSyntaxType.None);
        }


        public override string ToString()
        {
            var properties = ElementType.GetProperties();
            var propertyNames = properties.Select(_ => _.Name);



            var sb = new StringBuilder("SELECT ")
                .Append(String.Join(", ", propertyNames))
                .Append(" ")
                .Append("FROM ")
                .Append(ElementType.Name)
                .Append(String.Join(" AND ", Wheres))
                .Append(GroupBy ?? "")
                .Append(Having ?? "")
                .Append(OrderBy ?? "")
                .Append(";");


            return sb.ToString();
        }

        public IEnumerable<T> Execute(IDbConnection dbConnection)
        {
            var dbString = this.ToString();
            var properties = ElementType.GetProperties();
            var outCollection = new List<T>();

            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = dbString;

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var newInstance = new T();
                    foreach (var p in properties)
                    {
                        var propertyName = p.Name;
                        var columnIndex = reader.GetOrdinal(propertyName);
                        if (!reader.IsDBNull(columnIndex))
                        {
                            p.SetValue(newInstance, reader.GetValue(columnIndex));
                        }
                    }
                    outCollection.Add(newInstance);

                }
            }

            return outCollection;
        }
    }
}
