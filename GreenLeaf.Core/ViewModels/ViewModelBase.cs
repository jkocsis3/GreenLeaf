using MvvmCross.ViewModels;
using System.ComponentModel;

namespace GreenLeaf.Core.ViewModels
{
    public class ViewModelBase : MvxViewModel, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Handles the property changed event for the observable collection
        /// </summary>
        /// <param name="propertyName">The property which has changed</param>
        protected new virtual void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
