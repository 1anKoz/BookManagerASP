using System.ComponentModel.DataAnnotations;

namespace BookManagerASP.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Range(0,100)]
        public byte Rating { get; set; }
        public string? Content { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public string UserEntityId { get; set; }
        public virtual UserEntity UserEntity { get; set; }
    }
}
