namespace API.Movies.DAL.Models
{
    public class Category : AuditBase
    {
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
