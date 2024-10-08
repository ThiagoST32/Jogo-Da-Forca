using JogoDaForca.Libaries.Text;
using JogoDaForca.Models;
using JogoDaForca.Repositories;

namespace JogoDaForca
{
    public partial class MainPage : ContentPage
    {
        private Words _word;
        private int _errors = 0;

        public MainPage()
        {
            InitializeComponent();
            ResetScreen();
        }
        #region Verify if finhiser game
        private async void OnButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;


            string letter = button.Text;

            var positions = _word.Text.GetPositions(letter);

            if (positions.Count == 0)
            {
                ErrorHandler(sender, button);
                await IsGameOver();
                return;
            }

            ReplaceLetter(button, letter, positions);
            HasWinner();
        }
        #endregion
        #region Reset Screen - Back Screen to Initial State
        private void ResetScreen()
        {
            ResetErrors();
            ResetVirtualKeyboard();
            GenerateNewWord();
        }
        private async void HasWinner()
        {
            if (!LblText.Text.Contains("_"))
            {
                await DisplayAlert("Parabens!", "Você Ganhou o jogo", "Novo jogo!");
            }
        }
        
        private async Task IsGameOver()
        {
            if (_errors == 6)
            {
                await DisplayAlert("Perdeu!", "Quer começar um novo jogo?", "OK");
                ResetScreen();
            }
        }


        private void GenerateNewWord()
        {
            var repository = new WordsRepository();
            _word = repository.GetRandomWords();

            LblTips.Text = _word.Tips;
            LblText.Text = new string('_', _word.Text.Length);
            LblErrors.Text = _errors.ToString();
        }

        private void ResetErrors()
        {
            _errors = 0;
            ImgMain.Source = ImageSource.FromFile("forca1.png");
        }

        private void ResetVirtualKeyboard()
        {
            ResetVirtualKeys((HorizontalStackLayout)KeyboardContainer.Children[0]);
            ResetVirtualKeys((HorizontalStackLayout)KeyboardContainer.Children[1]);
            ResetVirtualKeys((HorizontalStackLayout)KeyboardContainer.Children[2]);
        }

        private void ResetVirtualKeys(HorizontalStackLayout horizontal)
        {
            foreach (Button button in horizontal.Children)
            {
                button.Style = null;
                button.IsEnabled = true;
            }
        }
        #endregion
        #region Handler Error
        private void ReplaceLetter(Button button, string letter, List<int> positions)
        {
            foreach (int position in positions)
            {
                LblText.Text = LblText.Text.Remove(position, 1).Insert(position, letter);
                button.Style = (Style)App.Current.Resources.MergedDictionaries.ElementAt(1)["Sucess"];
            }
        }

        private void ErrorHandler(object sender, Button button)
        {
            _errors++;
            ((Button)sender).IsEnabled = false;
            LblErrors.Text = _errors.ToString();
            ImgMain.Source = ImageSource.FromFile($"forca{_errors + 1}.png");
            button.Style = (Style)App.Current.Resources.MergedDictionaries.ElementAt(1)["Fail"];
        }
        #endregion

        private void ButtonRestartPlay(object sender, EventArgs e)
        {
            ResetScreen();
        }
    }

}
