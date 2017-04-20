using System;
using PerfusionMachine.Models;
using PerfusionMachine.Enums;

namespace PerfusionMachine.Models
{
    public class CommunicationEventArgs : EventArgs
    {
        public DeviceStatusEnum DeviceStatus { get; set; }
        public byte[] Command { get; set; }
        public TargetDeviceTypeEnum DeviceType { get; set; }
        public int DeviceId { get; set; }
        public int DirectiveId { get; set; }
        public DirectiveData Data { get; set; }

        public string Description { get; set; }


    }
}
