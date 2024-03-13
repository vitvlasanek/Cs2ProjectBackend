using System.Numerics;

namespace Vitvlasanek.Cs2.Project.Backend.ORM.Extensions
{
    public static class SqlQueryClauses
    {
        public static SqlCommandSelect<T> Where<T>(this SqlCommandSelect<T> query, string expression) where T : class
        {
            query.Wheres.Add($" WHERE {expression}");
            return query;
        }

        public static SqlCommandSelect<T> OrderBy<T>(this SqlCommandSelect<T> query, string expression) where T : class
        {
            query.OrderBy = $" ORDER BY {expression}";
            return query;
        }

        public static SqlCommandSelect<T> In<T>(this SqlCommandSelect<T> query, string what, ICollection<string> values) where T : class
        {
            query.Wheres.Add($" WHERE {what} IN ({string.Join(", ", values)})");
            return query;
        }

        public static SqlCommandSelect<T> Between<T, Num>(this SqlCommandSelect<T> query, string what, Num lower, Num upper) where T : class where Num : INumber<Num>
        {
            query.Wheres.Add($" WHERE {what} BETWEEN {lower} AND {upper}");
            return query;
        }

        public static SqlCommandSelect<T> Like<T>(this SqlCommandSelect<T> query, string what, string wildcard) where T : class
        {
            query.Wheres.Add($" WHERE {what} LIKE '{wildcard}'");
            return query;
        }
    }
}
