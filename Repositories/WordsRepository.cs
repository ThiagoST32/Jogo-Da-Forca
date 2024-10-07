using JogoDaForca.Models;

namespace JogoDaForca.Repositories
{
    public class WordsRepository
    {
        private List<Words> _words;

        public WordsRepository()
        {
            _words = new List<Words>();
            _words.Add(new Words("Nome", "João".ToUpper()));
            _words.Add(new Words("Comida", "Banana".ToUpper()));
            _words.Add(new Words("Cidade", "São Paulo".ToUpper()));
            _words.Add(new Words("Carro", "Mclares".ToUpper()));
            _words.Add(new Words("Moto", "H2R".ToUpper()));
        }

        public Words GetRandomWords()
        {
            Random rand = new Random();
            var number = rand.Next(0, _words.Count);
            return _words[number];
        }
    }
}
