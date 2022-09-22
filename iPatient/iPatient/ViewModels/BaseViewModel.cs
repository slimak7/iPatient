using iPatient.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        string _title;
        protected Action<WaitingPopupPage> _showWaitingPopup;

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel(Action<WaitingPopupPage> showWaitingPopup, string title)
        {
            Title = title;
            _showWaitingPopup = showWaitingPopup;
        }

        public void OnPropertyChanged([CallerMemberName] string name = null) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public void SetProperty<T>(ref T property, T value)
        {
            property = value;
            OnPropertyChanged();
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetProperty(ref _title, value);
            }
        }
    }


}
