using AsyncAwaitBestPractices.MVVM;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WinformMVVM
{
    public class SampleVModel : ViewModel
    {
        [Range(1, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
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
        }
    }
}
