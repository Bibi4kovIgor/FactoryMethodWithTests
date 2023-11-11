using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    public abstract class DatabaseConnectionFactory
    {
        public abstract IConnection CreateConnection();
    }
}
