using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PerfusionMachine.Handler
{

    public abstract class BaseHandler
    {
        protected BaseHandler next;

        public void SetNext(BaseHandler handler)
        {
            this.next = handler;
        }

       

        public abstract Task HandleRequest(ActionParams param,  Operate lastOperate, CancellationToken token);
    }
}
