using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vitvlasanek.Cs2.Project.Backend.ORM
{
    public static class SqlQueryClauses
    {
        public static SqlQuery<T> Where<T>(this SqlQuery<T> query, string expression) where T : class
        {
            query.Wheres.Add($" WHERE {expression}");
            return query;
        }

        public static SqlQuery<T> OrderBy<T>(this SqlQuery<T> query, string expression) where T : class
        {
            query.QueryBuilder.AppendLine($" ORDER BY {expression}");
            return query;
        }

        public static SqlQuery<T> In<T>(this SqlQuery<T> query, string what, ICollection<string> values) where T : class
        {
            query.QueryBuilder.AppendLine($" WHERE {what} IN ({String.Join(", ", values)})");
            return query;
        }

        public static SqlQuery<T> Between<T, Num>(this SqlQuery<T> query, string what, Num lower, Num upper ) where T : class where Num : INumber<Num>
        {
            query.QueryBuilder.AppendLine($" WHERE {what} BETWEEN {lower} AND {upper}");
            return query;
        }

        public static SqlQuery<T> In<T>(this SqlQuery<T> query, string what, string wildcard) where T : class
        {
            query.QueryBuilder.AppendLine($" WHERE {what} LIKE '{wildcard}'");
            return query;
        }
    }
}
