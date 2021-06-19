using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WinformMVVM
{
    /// <summary>
    /// NotifyPropertyChanged Extension
    /// </summary>
    public static class NotifyPropertyChangedExtension
    {
        /// <summary>
        /// MutateVerbose
        /// </summary>
        /// <typeparam name="TField">T</typeparam>
        /// <param name="instance">Model</param>
        /// <param name="field">Feild</param>
        /// <param name="newValue">New Value</param>
        /// <param name="raise">Raise Event</param>
        /// <param name="propertyName">Feild Name</param>
        /// <returns></returns>
        public static bool MutateVerbose<TField>(this INotifyPropertyChanged instance, ref TField field, TField newValue, Action<PropertyChangedEventArgs> raise, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<TField>.Default.Equals(field, newValue)) return false;
            field = newValue;
            raise?.Invoke(new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }
}
