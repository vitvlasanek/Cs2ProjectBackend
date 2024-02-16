using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitvlasanek.Cs2.Project.Backend.ORM.SqlSyntaxes
{
    internal class SqlSyntaxPostrges : ISqlSyntax
    {
        string ISqlSyntax.StartBracket => "\"";

        string ISqlSyntax.EndBracket => "\"";
    }
}
