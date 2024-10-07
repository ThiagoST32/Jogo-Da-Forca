namespace JogoDaForca.Models
{
    public class Words
    {
        public Words(string Tips, string Text) 
        { 
            this.Tips = Tips;
            this.Text = Text;
        }
        public String Tips { get; set; } = string.Empty;

        public String Text { get; set; } = string.Empty;
    }
}
