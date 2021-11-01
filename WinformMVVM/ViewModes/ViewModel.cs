using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace WinformMVVM.ViewModels
{
    /// <summary>
    /// Notify Property ViewModel
    /// </summary>
    public class ViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        /// <summary>
        /// Notify Property ViewModel
        /// </summary>
        public ViewModel() { }

        #region INotifyPropertyChanged
        /// <summary>
        /// 表示將處理的方法 <see cref="System.ComponentModel.INotifyPropertyChanged.PropertyChanged"/> 在元件上的屬性變更時引發的事件。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 表示將處理的方法 <see cref="System.ComponentModel.INotifyPropertyChanged.PropertyChanged"/> 在元件上的屬性變更時引發的事件。
        /// </summary>
        /// <param name="propertyName">Feild Name</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (SynchronizationContext.Current == _synchronizationContext)
            {
                if (!string.IsNullOrWhiteSpace(propertyName))
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(propertyName))
                    _synchronizationContext.Send(_ => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)), null);
            }
        }

        /// <summary>
        /// 表示將處理的方法 <see cref="System.ComponentModel.INotifyPropertyChanged.PropertyChanged"/> 在元件上的屬性變更時引發的事件。
        /// </summary>
        /// <param name="propertyName">Feild Name</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (SynchronizationContext.Current == _synchronizationContext)
            {
                if (!string.IsNullOrWhiteSpace(propertyName))
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(propertyName))
                    _synchronizationContext.Send(_ => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)), null);
            }
        }

        /// <summary>        
        /// 表示將處理的方法 <see cref="System.ComponentModel.INotifyPropertyChanged.PropertyChanged"/> 在元件上的屬性變更時引發的事件。        
        /// </summary>
        /// <returns></returns>
        protected Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args =>
            {
                if (SynchronizationContext.Current == _synchronizationContext)
                    PropertyChanged?.Invoke(this, args);
                else
                    _synchronizationContext.Send(_ => PropertyChanged?.Invoke(this, args), null);
            };
        }
        #endregion

        #region IDataErrorInfo
        /// <summary>
        /// Error Message
        /// </summary>
        public string Error
        {
            get
            {
                var results = new List<ValidationResult>();
                var result = Validator.TryValidateObject(this,
                    new ValidationContext(this, null, null), results, true);
                if (!result)
                    return string.Join("\n", results.Select(x => x.ErrorMessage));
                else
                    return null;
            }
        }

        /// <summary>
        /// Validate Feild
        /// </summary>
        /// <param name="propertyName">Feild Name</param>
        /// <returns></returns>
        public string this[string propertyName]
        {
            get
            {
                var propertyDescriptor = TypeDescriptor.GetProperties(this)[propertyName];
                if (propertyDescriptor == null)
                    return string.Empty;

                var results = new List<ValidationResult>();
                var result = Validator.TryValidateProperty(
                                          propertyDescriptor.GetValue(this),
                                          new ValidationContext(this, null, null)
                                          { MemberName = propertyName },
                                          results);
                if (!result)
                    return results.First().ErrorMessage;
                return string.Empty;
            }
        }
        #endregion
    }
}
