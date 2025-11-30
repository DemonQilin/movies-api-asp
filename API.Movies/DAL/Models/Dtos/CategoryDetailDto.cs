namespace API.Movies.DAL.Models.Dtos
{
    public class CategoryDetailDto : CategoryDto
    {
        public ICollection<MovieSummaryDto> Movies { get; set; }
    }
}
