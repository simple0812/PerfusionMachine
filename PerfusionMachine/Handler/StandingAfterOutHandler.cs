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
    public class StandingAfterOutHandler : BaseHandler
    {
        public override async Task HandleRequest(ActionParams param, Operate lastOperate, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var span = param.BottomInterval - lastOperate.CalctimeBeforePause();
            lastOperate.ActionChange(ActionEnum.StandingAfterOut);

            await Task.Delay(((int)(span > 0 ? span : 0) * 1000), token);
            var handleRequest = next?.HandleRequest(param, lastOperate, token);
            if (handleRequest != null) await handleRequest;
        }
    }
}
