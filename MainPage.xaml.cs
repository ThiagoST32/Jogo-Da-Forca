using JogoDaForca.Models;
using JogoDaForca.Repositories;

namespace JogoDaForca
{
    public partial class MainPage : ContentPage
    {
        private Words _word;
        public MainPage()
        {
            InitializeComponent();

            var repository = new WordsRepository();
            _word = repository.GetRandomWords();

            LblTips.Text = _word.Tips;
            LblText.Text = new string('_', _word.Text.Length);
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            string letter = ((Button)sender).Text;
        }
    }

}
