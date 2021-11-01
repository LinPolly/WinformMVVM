using AsyncAwaitBestPractices.MVVM;
using System.Threading;
using System.Threading.Tasks;

namespace WinformMVVM.ViewModels
{
    public class SampleVModel : ViewModel
    {
        public int Counter
        {
            get => _counter;
            set
            {
                this.MutateVerbose(ref _counter, value, RaisePropertyChanged());
            }
        }
        private int _counter = -1;

        public IAsyncCommand IncrementCommand { get; set; }
        public IAsyncCommand ResetCommand { get; set; }

        public SampleVModel()
        {
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

            ResetCommand.ExecuteAsync();

            Task.Run(() =>
            {
                while (true)
                {
                    IncrementCommand.ExecuteAsync();
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
