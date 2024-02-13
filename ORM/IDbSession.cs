using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitvlasanek.Cs2.Project.Backend.ORM
{
    public interface IDbSession
    {
        public void Insert<T>(RowDataGatewayBase<T> instance) where T : class;
        public void Update<T>(RowDataGatewayBase<T> instance) where T : class;
        public void Delete<T>(RowDataGatewayBase<T> instance) where T: class;
    }
}
