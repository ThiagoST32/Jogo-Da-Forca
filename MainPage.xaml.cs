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

        private async void OnButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;


            string letter = button.Text;

            var positions = _word.Text.GetPositions(letter);

            if (positions.Count == 0)
            {
                _errors++;
                ((Button)sender).IsEnabled = false;
                LblErrors.Text = _errors.ToString();
                ImgMain.Source = ImageSource.FromFile($"forca{_errors + 1}.png");
                button.Style = (Style)App.Current.Resources.MergedDictionaries.ElementAt(1)["Fail"];

                if (_errors == 6)
                {
                    await DisplayAlert("Perdeu!", "Quer começar um novo jogo?", "OK");
                    ResetScreen();
                }

                return;
            }

            foreach (int position in positions)
            {
                LblText.Text = LblText.Text.Remove(position, 1).Insert(position, letter);
                button.Style = (Style)App.Current.Resources.MergedDictionaries.ElementAt(1)["Sucess"];
            }
        }

        private void ResetScreen()
        {
            ResetErrors();
            ResetVirtualKeyboard();
            GenerateNewWord();
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
                button.Style = (Style)App.Current.Resources.MergedDictionaries.ElementAt(1)["NormalButton"];
                button.IsEnabled = true;
            }
        }
    }

}
