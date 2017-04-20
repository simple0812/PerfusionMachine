using System;
using PerfusionMachine.Models;
using PerfusionMachine.Protocols.Directives;
using PerfusionMachine.Protocols.Enums;

namespace PerfusionMachine.Protocols
{
    [VersionList(new Type[] { typeof(V485_1.V485_1) })]
    public interface IProtocol
    {
        byte[] GenerateDirectiveBuffer(BaseDirective directive);
        DirectiveResult ResolveFeedback(byte[] directive);
    }
}
