using Infrastrucutre;
using Infrastrucutre.IOManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Impl.StandardIOManager
{
    public class StandardIOManagerFactory : IIOManagerFactory
    {
        public IIOManager CreateIOManager(ILogger logger)
        {
            return new StandardIOManager(logger);
        }
    }
}
