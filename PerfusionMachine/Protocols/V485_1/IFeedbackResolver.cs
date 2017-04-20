using PerfusionMachine.Models;

namespace PerfusionMachine.Protocols.V485_1
{
    internal interface IFeedbackResolver
    {
        DirectiveResult ResolveFeedback(byte[] bytes);
    }
}
