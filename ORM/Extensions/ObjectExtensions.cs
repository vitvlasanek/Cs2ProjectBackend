using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitvlasanek.Cs2.Project.Backend.ORM.Extensions
{
    internal static class ObjectExtensions
    {
        internal static DbType ToDbType(this object obj) 
        {
            switch (obj)
            {
                case string _: return DbType.String;
                case int _: return DbType.Int32;
                case double _: return DbType.Double;
                case bool _: return DbType.Boolean;
                default: return DbType.Object;
            }
        }

    }
}
