using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrucutre.IOManager
{
    public interface IIOManagerFactory
    {
        IIOManager CreateIOManager(ILogger logger);
    }
}
