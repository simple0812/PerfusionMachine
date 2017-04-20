using System.Runtime.Serialization;

namespace PerfusionMachine.Enums
{
    public enum DirectionEnum
    {
        [EnumMember(Value = "In")]
        In = 0,
        [EnumMember(Value = "Out")]
        Out
    }
}
