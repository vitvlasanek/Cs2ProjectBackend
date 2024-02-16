using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitvlasanek.Cs2.Project.Backend.ORM.SqlSyntaxes
{
    public interface ISqlSyntax
    {
        internal string StartBracket { get; }
        internal string EndBracket { get; }
    }
}
