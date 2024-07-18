namespace BookManagerASP.Models
{
    public class BookPrivates
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<Quote>? Quotes { get; set; }
        public bool IsFavourite { get; set; }
    }
}
