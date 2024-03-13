using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using Vitvlasanek.Cs2.Project.Backend.ORM.Attributes;
using Vitvlasanek.Cs2.Project.Backend.ORM.Extensions;
using Vitvlasanek.Cs2.Project.Backend.ORM.Sessions;
using Vitvlasanek.Cs2.Project.Backend.ORM.SqlSyntaxes;

namespace Vitvlasanek.Cs2.Project.Backend.ORM
{
    public abstract class RowDataGatewayBase<T> where T : class
    {
        [PrimaryKey]
        public long? Id { get; internal set; }

        [DbIgnore]
        public static SqlSyntaxType Syntax { get; set; } = SqlSyntaxType.None;

        [DbIgnore]
        IDbSession? Session { get; set; }


        public string GetName() => this.GetType().Name;

        public static IEnumerable<string> GetPropertyNames(bool ignorePk = false)
        {
            var properties = typeof(T).GetProperties().ToList();

            if (ignorePk)
            {
                properties = properties.Where(_ => !Attribute.IsDefined(_, typeof(PrimaryKey))).ToList();
            }

            return properties.Select(p => p.Name);
        }

        public Dictionary<string, object?> GetPropertiesDictionary(bool ignorePk = false)
        {
            var propertiesDictionary = new Dictionary<string, object?>();

            var properties = typeof(T).GetProperties().Where(_ => !Attribute.IsDefined(_, typeof(DbIgnore)));

            if (ignorePk)
            {
                properties = properties.Where(_ => !Attribute.IsDefined(_, typeof(PrimaryKey)));
            }

            foreach (PropertyInfo property in properties)
            {
                var name = property.Name;
                var value = property.GetValue(this);


                propertiesDictionary[property.Name] = property.GetValue(this);
            }

            return propertiesDictionary;
        }

        internal static IDbDataParameter CreateParameter(IDbCommand command, KeyValuePair<string, object?> property, int? Id = null)
        {
            string idSuffix = string.Empty;

            if (Id is not null)
            {
                idSuffix = $"_{Id}";
            }

            var parameter = command.CreateParameter();
            parameter.ParameterName = $"@{property.Key}{idSuffix}";
            parameter.Value = property.Value;

            return parameter;
        }

        public bool TableExists(DbConnection connection)
        {
            string tableName = this.GetName();

            var schema = connection.GetSchema("Tables");
            foreach (DataRow row in schema.Rows)
            {
                var table = (string)row[2];
                if (table.Equals(tableName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        void CreateTable(DbConnection connection)
        {
            var propertiesDictionary = this.GetPropertiesDictionary(true);

            var mappedProps = propertiesDictionary.Select(_ => $"{_.Key} {_.Key.ToDbType()}");

            //TODO: multiple PKs
            var pk = this.GetType().GetProperties().FirstOrDefault(_ => Attribute.IsDefined(_, typeof(PrimaryKey)));

            var sb = new StringBuilder("CREATE TABLE ")
                .Append(this.GetName())
                .Append(" (")
                .Append(pk!.Name)
                .Append(" INTEGER PRIMARY KEY NOT NULL, ")
                .Append(String.Join(", ", mappedProps))
                .Append(");")
                ;


            using (var command = connection.CreateCommand())
            {
                command.CommandText = sb.ToString();
                command.ExecuteNonQuery();
            }

        }
    }
}