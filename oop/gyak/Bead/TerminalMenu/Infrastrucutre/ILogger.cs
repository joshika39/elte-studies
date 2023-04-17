using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrucutre
{
    public interface ILogger
    {
        string LogWrite(string message);
        string LogWriteLine(string message);
    }
}
