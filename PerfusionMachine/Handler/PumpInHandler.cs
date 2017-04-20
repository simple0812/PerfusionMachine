using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PerfusionMachine.Enums;
using PerfusionMachine.Libs;

namespace PerfusionMachine.Handler
{
    public class PumpInHandler:BaseHandler
    {
        public override async Task HandleRequest(ActionParams param, Operate lastOperate, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            //已经运行的时间
            var span = lastOperate.CalctimeBeforePause();

            var volume = param.Volume - span * param.Flowrate / 60;
            lastOperate.ActionChange(ActionEnum.PumpIn);
            await Logic.Instance.pump1.SetParams(param.Flowrate, volume > 0 ? volume: 0, DirectionEnum.In).StartAsync();
            var handleRequest = next?.HandleRequest(param, lastOperate, token); ;
            if (handleRequest != null) await handleRequest;
        }
    }
}
