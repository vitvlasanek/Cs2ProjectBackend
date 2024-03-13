using System.Text;
using Vitvlasanek.Cs2.Project.Backend.ORM.SqlSyntaxes;

namespace Vitvlasanek.Cs2.Project.Backend.ORM
{
    public class SqlCommandSelect<T> where T : class
    {
        public SqlCommandSelect(SqlSyntaxType syntax)
        {
            Syntax = new SqlSyntax(syntax);
            this.SetToAllAttributes();
        }


        public Type ElementType => typeof(T);
        public ISqlSyntax Syntax {  get; set; } = new SqlSyntax(SqlSyntaxType.SqlServer);

        #region QueryClauses

        internal string What { get; set; } = " *";
        internal HashSet<object> Wheres { get; } = new HashSet<object>();
        internal string? GroupBy { get; set; }
        internal string? Having { get; set; }
        internal string? OrderBy { get; set; }

        #endregion


        public void SetToAllAttributes()
        {
            var properties = ElementType.GetProperties();
            What = string.Join(", ", properties.Select(_ => $"{Syntax.StartBracket}{_.Name}{Syntax.EndBracket}"));
        }
        #region Aggregations
        //public int Count()
        //{
        //    What = " COUNT(*)";
        //    var result = RowDataGatewayBase<T>.ExecuteSelectScalarQuery<Int32>(this);
        //    What = " *";
        //    return result;
        //}

        //public int Avg(string field)
        //{
        //    What = $" AVG({field})";
        //    var result = RowDataGatewayBase<T>.ExecuteSelectScalarQuery<float>(this);
        //    What = " *";
        //    return result;

        //}
        //public int Sum(string field)
        //{
        //    What = $" SUM({field})";
        //    var result = RowDataGatewayBase<T>.ExecuteSelectScalarQuery<int>(this);
        //    What = " *";
        //    return result;
        //}
        #endregion

        public override string ToString()
        {
            var properties = ElementType.GetProperties();

            var sb = new StringBuilder("SELECT ")
                .Append(What)
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

    }
}
