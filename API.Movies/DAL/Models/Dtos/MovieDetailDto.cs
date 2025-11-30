namespace API.Movies.DAL.Models.Dtos
{
    public class MovieDetailDto : MovieDto
    {
        public CategorySummaryDto Category { get; set; }
    }
}
