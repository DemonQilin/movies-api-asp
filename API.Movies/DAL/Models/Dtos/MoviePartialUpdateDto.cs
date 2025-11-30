using System.ComponentModel.DataAnnotations;

namespace API.Movies.DAL.Models.Dtos
{
    public class MoviePartialUpdateDto
    {
        [MaxLength(100, ErrorMessage = "The name cannot exceed 100 characters")]
        public string? Name { get; set; }

        public int? Duration { get; set; }

        public string? Description { get; set; }

        [MaxLength(10, ErrorMessage = "The clasification cannot exceed 10 characters")]
        public string? Clasification { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The categoryId must be greater than 0")]
        public int? CategoryId { get; set; }
    }
}
