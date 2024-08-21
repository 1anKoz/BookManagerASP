using BookManagerASP.Data.Enum;

namespace BookManagerASP.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string CoverUrl { get; set; }
        public string Isbn { get; set; }
        public Genre Genre { get; set; }
    }
}
