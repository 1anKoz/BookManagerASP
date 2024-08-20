using Microsoft.AspNetCore.Identity;

namespace BookManagerASP.Models
{
    public class UserEntity : IdentityUser
    {
        public string? PhotoUrl { get; set; }

        public virtual ICollection<Shelf> Shelves { get; set; }
        public virtual ICollection<BookPrivate> BookPrivates { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
