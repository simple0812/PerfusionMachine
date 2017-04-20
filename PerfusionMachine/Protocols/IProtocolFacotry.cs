using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfusionMachine.Protocols
{
    public interface IProtocolFacotry
    {
        IProtocol Create();
    }
}
