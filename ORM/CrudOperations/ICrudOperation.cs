using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitvlasanek.Cs2.Project.Backend.ORM.CrudOperations
{
    internal interface ICrudOperation<T> where T : RowDataGatewayBase<T>
    {
        public Type ElementType { get; }

    }
}
