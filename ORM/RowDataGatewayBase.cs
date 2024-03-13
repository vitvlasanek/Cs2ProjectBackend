using System.Data.Common;
using System.Text;
using Vitvlasanek.Cs2.Project.Backend.ORM.SqlSyntaxes;

namespace Vitvlasanek.Cs2.Project.Backend.ORM
{
    public abstract class RowDataGatewayBase<T> where T : class
    {

        public ISqlSyntax Syntax { get; set; } = new SqlSyntax(SqlSyntaxType.None);

        public string GetName()
        {
            return this.GetType().Name;
        }

        public IEnumerable<string> GetPropertyNames()
        {
            return this.GetType()
                .GetProperties()
                .Select(p => p.Name);
        }


        public string GenerateSelectQuery()
        {
            var query = new SqlCommandSelect<T>(Syntax);
            return query.ToString();
        }

        internal void Insert(IDbSession session)
        {

            Type type = typeof(T);

            Type type1 = GetType();


            DbConnection conn = null;
        }

        //internal static IEnumerable<RowDataGatewayBase<T>> ExecuteSelectQuery(SqlQuery<T> query)
        //{

        //    var queryString = query.Get();
        //    return new List<RowDataGatewayBase<T>>();
        //}


        //internal static int ExecuteSelectScalarQuery<S>(SqlQuery<T> query) where S : struct
        //{

        //    var queryString = query.Get();
        //    return 0;
        //}

        //public static SqlQuery<T> SelectAsQuery(IDbSession session)
        //{
        //    return new SqlQuery<T>(Syntax);
        //}

    }
}