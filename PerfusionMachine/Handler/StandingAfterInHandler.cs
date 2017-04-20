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
    public class StandingAfterInHandler : BaseHandler
    {
        public override async Task HandleRequest(ActionParams param, Operate lastOperate, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var span = param.TopInterval - lastOperate.CalctimeBeforePause();
            lastOperate.ActionChange(ActionEnum.StandingAfterIn);

            await Task.Delay(((int)(span > 0 ?span : 0) * 1000), token);
            var handleRequest = next?.HandleRequest(param, lastOperate, token); ;
            if (handleRequest != null) await handleRequest;
        }
    }
}
