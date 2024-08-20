namespace BookManagerASP.Dto
{
    public class QuoteEditDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? Page { get; set; }
        public bool IsFavourite { get; set; }
    }
}
