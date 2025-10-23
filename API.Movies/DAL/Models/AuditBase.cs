using System.ComponentModel.DataAnnotations;

namespace API.Movies.DAL.Models
{
    public class AuditBase
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
    }
}
