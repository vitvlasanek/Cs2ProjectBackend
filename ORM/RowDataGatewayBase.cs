using System.Data.Common;

namespace Vitvlasanek.Cs2.Project.Backend.ORM
{
    public abstract class RowDataGatewayBase<T> where T : class
    {
        internal void Insert(IDbSession session)
        {
            Type type = typeof(T);

            Type type1 = GetType();


            DbConnection conn = null;
        }

        internal static IEnumerable<RowDataGatewayBase<T>> ExecuteSelectQuery(SqlQuery<T> query)
        {

            var queryString = query.Get();
            return new List<RowDataGatewayBase<T>>();
        }


        internal static int ExecuteSelectScalarQuery<S>(SqlQuery<T> query) where S : struct
        {

            var queryString = query.Get();
            return 0;
        }

        public static SqlQuery<T> SelectAsQuery(IDbSession session)
        {
            return new SqlQuery<T>();
        }

    }
}