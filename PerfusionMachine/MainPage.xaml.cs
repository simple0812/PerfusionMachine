using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PerfusionMachine.Enums;
using PerfusionMachine.Handler;
using PerfusionMachine.Libs;
using PerfusionMachine.Models;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace PerfusionMachine
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private Operate _lastOperate;
        private PumpOutHandler outHandler = new PumpOutHandler();
        private PumpInHandler inHandler = new PumpInHandler();
        private StandingAfterInHandler afterInHandler = new StandingAfterInHandler();
        private StandingAfterOutHandler afterOutHandler = new StandingAfterOutHandler();
        private ActionParams aParams;
        public MainPage()
        {
            this.InitializeComponent();
            _lastOperate = new Operate();
            Debug.WriteLine(_lastOperate.Type);
            InitHandler();
            Logic.Instance.CommunicationHandler += Instance_CommunicationHandler; ;
        }

        private async void Instance_CommunicationHandler(object arg1, Models.CommunicationEventArgs arg2)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var data = arg2.Data as PumpDirectiveData;

                if(data == null) return;
                if (data.DirectiveType == DirectiveTypeEnum.Running)
                {
                    if (data.Direction == DirectionEnum.In)
                    {
                        txtStatus.Text = data.FlowRate <=0 ? "standing after in" :"pump in";
                    }
                    else
                    {
                        txtStatus.Text = data.FlowRate <= 0 ? "standing after out" : "pump out";
                    }
                }
                else if(data.DirectiveType == DirectiveTypeEnum.TryPause)
                {
                    txtStatus.Text = "pause";
                }
            });
        }

        private void InitHandler()
        {
            inHandler.SetNext(afterInHandler);
            afterInHandler.SetNext(outHandler);
            outHandler.SetNext(afterOutHandler);
            afterOutHandler.SetNext(inHandler);
        }

        private async void BtnStart_OnClick(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            cancellationTokenSource = new CancellationTokenSource();
            int volume = int.TryParse(txtVolume.Text, out volume) ? volume : 10;
            int flowrate = int.TryParse(txtFlowrate.Text, out flowrate) ? flowrate : 10;
            int topInterval = int.TryParse(txtTopInterval.Text, out topInterval) ? topInterval : 10;
            int bottomInterval = int.TryParse(txtBottomInterval.Text, out bottomInterval) ? bottomInterval : 10;
            aParams = new ActionParams()
            {
                BottomInterval = bottomInterval,
                TopInterval =  topInterval,
                Flowrate = flowrate,
                Volume = volume
            };

            try
            {
                var handler = GetHandler(_lastOperate.Type);
                await handler.HandleRequest(aParams, _lastOperate, cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public BaseHandler GetHandler(ActionEnum action)
        {
            switch (action)
            {
                case ActionEnum.None:
                    return inHandler;
                case ActionEnum.PumpIn:
                    return inHandler;
                case ActionEnum.PumpOut:
                    return outHandler;
                case ActionEnum.StandingAfterIn:
                    return afterInHandler;
                case ActionEnum.StandingAfterOut:
                    return afterOutHandler;
                default: return inHandler;
            }
        }

        private async void BtnStop_OnClick(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource.Cancel();
            await Logic.Instance.Close();
            btnStart.IsEnabled = true;
            _lastOperate.Clear();
            txtStatus.Text = "";
        }

        private async void BtnPause_OnClick(object sender, RoutedEventArgs e)
        {
            _lastOperate.ActionEnd();
            await Logic.Instance.pump1.StopAsync();
            btnStart.IsEnabled = true;
        }
    }
}
