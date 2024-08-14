namespace BookManagerASP.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? Page {  get; set; }
        public bool IsFavourite { get; set; }

        public int BookPrivateId { get; set; }
        public virtual BookPrivate BookPrivate { get; set; }
    }
}
