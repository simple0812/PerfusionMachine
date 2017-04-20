﻿using System.Runtime.Serialization;

namespace PerfusionMachine.Enums
{
    public enum ProcessModeEnum
    {
        None = -1,
        [EnumMember(Value = "SingleMode")]
        SingleMode = 0,

        [EnumMember(Value = "FixedVolumeMode")]
        FixedVolumeMode = 1,

        [EnumMember(Value = "VariantVolumeMode")]
        VariantVolumeMode = 2,

        [EnumMember(Value = "CorelateVolumeMode")]
        CorelateVolumeMode = 3
    }
}
