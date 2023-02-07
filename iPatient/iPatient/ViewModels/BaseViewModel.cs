using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace iPatient.ViewModels
{
    public abstract class BaseViewModel<M> : INotifyPropertyChanged
    {
        protected string _title;
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
