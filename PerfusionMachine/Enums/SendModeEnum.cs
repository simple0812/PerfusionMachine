using System.Runtime.Serialization;

namespace PerfusionMachine.Enums
{
    public enum SendModeEnum
    {
        [EnumMember(Value = "Send")]
        Send = 0,

        [EnumMember(Value = "Receive")]
        Receive
    }
}
