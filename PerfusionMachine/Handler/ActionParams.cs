using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfusionMachine.Handler
{
    public class ActionParams
    {
        public int Volume { get; set; }
        public int Flowrate { get; set; }
        public int TopInterval { get; set; }
        public int BottomInterval { get; set; }
    }
}
