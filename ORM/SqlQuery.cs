using System.Collections;
using System.Text;

namespace Vitvlasanek.Cs2.Project.Backend.ORM
{
    public class SqlQuery<T> : IEnumerable<RowDataGatewayBase<T>> where T : class
    {
        internal StringBuilder QueryBuilder { get; } = new StringBuilder();
        public Type ElementType => typeof(T);
        internal string Get() => QueryBuilder.ToString();

        #region QueryClauses

        internal string What { get; private set; } = " *";
        internal HashSet<object> Wheres { get; } = new HashSet<object>();
        internal string? GroupBy { get; set; }
        internal string? Having { get; set; }
        internal string? OrderBy { get; set; }

        #endregion

        #region Aggregations
        public int Count()
        {
            What = " COUNT(*)";
            var result = RowDataGatewayBase<T>.ExecuteSelectScalarQuery<Int32>(this);
            What = " *";
            return result;
        }

        public int Avg(string field)
        {
            What = $" AVG({field})";
            var result = RowDataGatewayBase<T>.ExecuteSelectScalarQuery<float>(this);
            What = " *";
            return result;

        }
        public int Sum(string field)
        {
            What = $" SUM({field})";
            var result = RowDataGatewayBase<T>.ExecuteSelectScalarQuery<int>(this);
            What = " *";
            return result;
        }
        #endregion

        public IEnumerator<RowDataGatewayBase<T>> GetEnumerator()
        {
            return RowDataGatewayBase<T>.ExecuteSelectQuery(this).GetEnumerator();
            
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
