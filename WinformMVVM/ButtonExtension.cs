using AsyncAwaitBestPractices.MVVM;
using System;
using System.Reactive.Disposables;
using System.Windows.Forms;
using System.Windows.Input;

namespace WinformMVVM
{
    /// <summary>
    /// Button Extension
    /// </summary>
    public static class ButtonExtension
    {
        /// <summary>
        /// Bind Button ICommand
        /// </summary>
        /// <param name="invoker">Button</param>
        /// <param name="command">ICommand</param>
        /// <returns>提供用於釋放 Unmanaged 資源的機制。若要瀏覽此類型的.NET Framework 原始程式碼，請參閱 Reference Source。</returns>
        public static IDisposable BindCommand(this ButtonBase invoker, ICommand command)
        {
            void Click(object sender, EventArgs args) => command.Execute(null);
            void CanExecuteChanged(object sender, EventArgs args) => invoker.Enabled = command.CanExecute(null);

            invoker.Enabled = command.CanExecute(null);

            invoker.Click += Click;
            command.CanExecuteChanged += CanExecuteChanged;

            return Disposable.Create(() =>
            {
                invoker.Enabled = false;
                invoker.Click -= Click;
                command.CanExecuteChanged -= CanExecuteChanged;
            });
        }

        /// <summary>
        /// Bind Button IAsyncCommand
        /// </summary>
        /// <param name="invoker">Button</param>
        /// <param name="command">IAsyncCommand</param>
        /// <returns>提供用於釋放 Unmanaged 資源的機制。若要瀏覽此類型的.NET Framework 原始程式碼，請參閱 Reference Source。</returns>
        public static IDisposable BindCommand(this ButtonBase invoker, IAsyncCommand command)
        {
            void Click(object sender, EventArgs args) => command.ExecuteAsync();
            void CanExecuteChanged(object sender, EventArgs args) => invoker.Enabled = command.CanExecute(null);

            invoker.Enabled = command.CanExecute(null);

            invoker.Click += Click;
            command.CanExecuteChanged += CanExecuteChanged;

            return Disposable.Create(() =>
            {
                invoker.Enabled = false;
                invoker.Click -= Click;
                command.CanExecuteChanged -= CanExecuteChanged;
            });
        }
    }
}
