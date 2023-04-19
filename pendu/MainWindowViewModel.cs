using System.Windows.Input;

namespace pendu
{
    public class MainWindowViewModel : Notifier
    {
        public MainWindowViewModel()
        {
                
        }

        private string _wordToFind;
        public string WordToFind
        {
            get => _wordToFind;
            set
            {
                if (_wordToFind == value) return;
                _wordToFind = value;
                OnPropertyChanged();
            }

        }

        private ICommand _doSomething;
        public ICommand DoSomethingCommand
        {
            get
            {
                if (_doSomething == null)
                {
                    _doSomething = new DelegateCommand(DoSomething, CanDoSomething);
                }
                return _doSomething;
            }
        }

        private void DoSomething(object parameter)
        {
            ;
        }

        private bool CanDoSomething(object parameter)
        {
            return true;
        }
    }
}
