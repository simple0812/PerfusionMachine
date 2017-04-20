using System.Runtime.Serialization;

namespace PerfusionMachine.Enums
{
    public enum RockEnum
    {
        [EnumMember(Value = "Normal")]
        Normal = 0,
        [EnumMember(Value = "Rotate")]
        Rotate
    }
}
