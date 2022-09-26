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
    public abstract class BaseViewModel<M> : INotifyPropertyChanged
    {
        string _title;
        protected M _viewPage;

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel(string title, M viewPage)
        {
            Title = title;
            _viewPage = viewPage;
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

        public virtual bool ValidateData (ref string message)
        {
            return true;
        }
    }


}
