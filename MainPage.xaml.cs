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

            var repository = new WordsRepository();
            _word = repository.GetRandomWords();

            LblTips.Text = _word.Tips;
            LblText.Text = new string('_', _word.Text.Length);
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            string letter = ((Button)sender).Text;

            var positions = _word.Text.GetPositions(letter);

            if (positions.Count == 0)
            {
                _errors++;
                ((Button)sender).IsEnabled = false;
                return;
            }

            foreach (int position in positions) 
            {
                LblText.Text = LblText.Text.Remove(position, 1).Insert(position, letter);
            }
        }
    }

}
