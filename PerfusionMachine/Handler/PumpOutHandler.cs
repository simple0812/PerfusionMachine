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
    public class PumpOutHandler : BaseHandler
    {
        public override async Task HandleRequest(ActionParams param, Operate lastOperate, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var span = lastOperate.CalctimeBeforePause();

            var volume = param.Volume - span * param.Flowrate / 60;
            lastOperate.ActionChange(ActionEnum.PumpOut);
            await Logic.Instance.pump1.SetParams(param.Flowrate, volume > 0 ? volume : 0, DirectionEnum.Out).StartAsync();

            var handleRequest = next?.HandleRequest(param, lastOperate, token); ;
            if (handleRequest != null) await handleRequest;
        }
    }
}
