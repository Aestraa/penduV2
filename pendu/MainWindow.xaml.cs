using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace pendu
{
    public partial class MainWindow : Window
    {
        private Word currentWord;
        private StringBuilder hiddenWord;
        private ObservableCollection<char> guessedLetters;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
            DataContext = new MainWindowViewModel();
        }

        private void InitializeGame()
        {
            currentWord = WordGenerator.GetRandomWord();
            hiddenWord = new StringBuilder(new string('_', currentWord.Length));
            guessedLetters = new ObservableCollection<char>();
            listeDeMots.ItemsSource = guessedLetters;
            resultat.Text = hiddenWord.ToString();
        }

        private void nouvellePartieBouton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }

        private void validationMotBouton_Click(object sender, RoutedEventArgs e)
        {
            string input = motatrouver.Text.ToUpper();
            if (!string.IsNullOrWhiteSpace(input) && input.Length == 1)
            {
                char letter = input[0];
                if (!guessedLetters.Contains(letter))
                {
                    guessedLetters.Add(letter);
                    int index = currentWord.GetIndexOf(letter);
                    bool correctGuess = false;

                    while (index != -1)
                    {
                        hiddenWord[index] = letter;
                        correctGuess = true;
                        index = currentWord.GetIndexOf(letter, index + 1);
                    }

                    if (correctGuess)
                    {
                        resultat.Text = hiddenWord.ToString();
                        if (!hiddenWord.ToString().Contains('_'))
                        {
                            _ = MessageBox.Show("Félicitations, vous avez deviné le mot!", "Victoire");
                            InitializeGame();
                        }
                    }
                }
            }
            motatrouver.Clear();
        }
    }
}

