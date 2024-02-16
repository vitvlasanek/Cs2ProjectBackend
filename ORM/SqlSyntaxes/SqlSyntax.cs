using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitvlasanek.Cs2.Project.Backend.ORM.SqlSyntaxes
{
    internal class SqlSyntax : ISqlSyntax
    {
        ISqlSyntax _sqlSyntaxReferecnce = new SqlSyntaxNone();

        public SqlSyntax(SqlSyntaxType type) 
        { 
            switch (type)
            {
                case SqlSyntaxType.None:
                    _sqlSyntaxReferecnce = new SqlSyntaxNone();
                    break;
                case SqlSyntaxType.SqlServer:
                    _sqlSyntaxReferecnce = new SqlSyntaxSqlServer();
                    break;
                case SqlSyntaxType.Postgres:
                    _sqlSyntaxReferecnce = new SqlSyntaxPostrges();
                    break;
            }
        }
        string ISqlSyntax.StartBracket => _sqlSyntaxReferecnce.StartBracket;

        string ISqlSyntax.EndBracket => _sqlSyntaxReferecnce.EndBracket;
    }
}
