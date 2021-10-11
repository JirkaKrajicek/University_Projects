using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Xamarin_Eshop_Krajicek.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
