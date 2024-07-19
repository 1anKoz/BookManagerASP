namespace BookManagerASP.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        //public string? PhotoUrl { get; set; }

        public virtual ICollection<Shelf> Shelves { get; set; }

        public virtual ICollection<BookPrivate> BookPrivates { get; set; }
    }
}
