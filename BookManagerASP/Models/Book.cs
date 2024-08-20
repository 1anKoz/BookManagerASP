using BookManagerASP.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace BookManagerASP.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string CoverUrl { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "ISBN has to be a 10 digit number")]
        public string Isbn { get; set; }
        public Genre Genre { get; set; }
        //dodać ocenę będącą uśrednieniem ocen ze wszystkich recenzji danej książki
        //public float Rating { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
