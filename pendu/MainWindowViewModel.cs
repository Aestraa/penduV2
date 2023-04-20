using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace pendu
{
    public class MainWindowViewModel : Notifier
    {
        public MainWindowViewModel()
        {
            InitializeGame();
            ValidateWordCommand = new DelegateCommand(ValidateWord, CanValidateWord);
            NewGameCommand = new DelegateCommand(NewGame, CanNewGame);

        }


        private Word _currentWord;
        private StringBuilder _hiddenWord;
        private ObservableCollection<char> _guessedLetters;

        public ObservableCollection<char> GuessedLetters
        {
            get => _guessedLetters;
            set
            {
                _guessedLetters = value;
                OnPropertyChanged();
            }
        }

        public string HiddenWord
        {
            get => _hiddenWord.ToString();
            set
            {
                _ = _hiddenWord.Clear();
                _ = _hiddenWord.Append(value);
                OnPropertyChanged();
            }
        }

        public void InitializeGame()
        {
            _currentWord = WordGenerator.GetRandomWord();
            _hiddenWord = new StringBuilder(new string('_', _currentWord.Length));
            _guessedLetters = new ObservableCollection<char>();
            HiddenWord = _hiddenWord.ToString();
        }


        private ICommand _validateWordCommand;
        public ICommand ValidateWordCommand
        {
            get => _validateWordCommand;
            set
            {
                _validateWordCommand = value;
                OnPropertyChanged();
            }
        }


        private void ValidateWord(object parameter)
        {
            string input = WordToFind?.ToUpper();
            if (!string.IsNullOrWhiteSpace(input) && input.Length == 1)
            {
                char letter = input[0];
                if (!_guessedLetters.Contains(letter))
                {
                    GuessedLetters.Add(letter);
                    int index = _currentWord.GetIndexOf(letter);
                    bool correctGuess = false;

                    while (index != -1)
                    {
                        _hiddenWord[index] = letter;
                        correctGuess = true;
                        index = _currentWord.GetIndexOf(letter, index + 1);
                    }

                    if (correctGuess)
                    {
                        HiddenWord = _hiddenWord.ToString();
                        if (!HiddenWord.Contains('_'))
                        {
                            _ = MessageBox.Show("Félicitations, vous avez deviné le mot!", "Victoire");
                            InitializeGame();
                        }
                    }
                }
            }
            WordToFind = string.Empty;
        }

        private bool CanValidateWord(object parameter)
        {
            return true;
        }

        private ICommand _newGameCommand;
        public ICommand NewGameCommand
        {
            get => _newGameCommand;
            set
            {
                _newGameCommand = value;
                OnPropertyChanged();
            }
        }


        private void NewGame(object parameter)
        {
            InitializeGame();
        }

        private bool CanNewGame(object parameter)
        {
            return true;
        }

        private string _wordToFind;
        public string WordToFind
        {
            get => _wordToFind;
            set
            {
                if (_wordToFind == value)
                {
                    return;
                }

                _wordToFind = value;
                OnPropertyChanged();
            }
        }
    }
}
