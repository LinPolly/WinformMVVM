using AsyncAwaitBestPractices.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformMVVM
{
    public class SampleVModel : ViewModel
    {
        public int Counter
        {
            get => _counter;
            set => this.MutateVerbose(ref _counter, value, RaisePropertyChanged());
        }
        private int _counter = 0;

        public IAsyncCommand IncrementCommand { get; set; }
        public IAsyncCommand ResetCommand { get; set; }

        public SampleVModel()
        {
            _counter = 0;

            IncrementCommand = new AsyncCommand(() =>
            {
                return Task.Run(() =>
                {
                    Counter++;
                });
            });
            ResetCommand = new AsyncCommand(() =>
            {
                return Task.Run(() =>
                {
                    Counter = 0;
                });
            });
        }
    }
}
