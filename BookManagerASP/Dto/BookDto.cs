namespace BookManagerASP.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string CoverUrl { get; set; }
        public int Isbn { get; set; }
    }
}
