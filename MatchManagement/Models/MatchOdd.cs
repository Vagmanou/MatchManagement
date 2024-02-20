namespace MatchManagement.Models
{
    public class MatchOdd
    {
        public int ID { get; set; }

        public Match Match { get; set; }

        public string Specifier { get; set; }

        public double Odd { get; set; }
    }
}
