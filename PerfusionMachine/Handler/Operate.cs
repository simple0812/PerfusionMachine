using System;
using System.Diagnostics;
using PerfusionMachine.Enums;

namespace PerfusionMachine.Handler
{
    public class Operate
    {
        public ActionEnum Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public void ActionChange(ActionEnum action)
        {
            Type = action;
            StartTime = DateTime.Now;
            EndTime = DateTime.MinValue;
        }

        public void ActionEnd()
        {
            EndTime = DateTime.Now;
        }

        public void Clear()
        {
            Type = ActionEnum.None;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
        }

        public double CalctimeBeforePause()
        {
            if (Type == ActionEnum.None) return 0;
            if (StartTime == DateTime.MinValue || EndTime == DateTime.MinValue) return 0;
            if (StartTime >= EndTime) return 0;
            Debug.WriteLine("span->" + (EndTime - StartTime).TotalSeconds);

            return (EndTime - StartTime).TotalSeconds;
        }
    }
}
