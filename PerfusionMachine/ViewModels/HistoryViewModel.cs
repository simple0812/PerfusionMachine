using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfusionMachine.Enums;
using PerfusionMachine.Models;
using PerfusionMachine.Services;

namespace PerfusionMachine.ViewModels
{
    public class HistoryViewModel:ViewModel
    {
        private ObservableCollection<WashFlow> _washFlows;

        public ObservableCollection<WashFlow> WashFlows
        {
            get { return _washFlows; }
            set
            {
                _washFlows = value;
                OnPropertyChanged();
            }
        }

        public HistoryViewModel()
        {
            _washFlows = new ObservableCollection<WashFlow>(WashFlowService.Instance.GetList());
        }

        private RelayCommand remove;
        public RelayCommand Remove => remove ?? (remove = new RelayCommand(ExecuteRemove));

        private void ExecuteRemove(object obj)
        {
            var flow = obj as WashFlow;
            if(flow == null) return;

            if (WashFlowService.Instance.Remove(flow))
            {
                WashFlows.Remove(flow);
            }
        }
    }
}
