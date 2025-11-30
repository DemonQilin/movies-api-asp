using API.Movies.DAL.Models.Dtos;

namespace API.Movies.Services.IServices
{
    public interface IMovieService
    {
        Task<ICollection<MovieDto>> GetMoviesAsync();
        Task<MovieDetailDto?> GetMovieAsync(int movieId);
        Task<MovieDto> CreateMovieAsync(MovieCreateDto movieDto);
        Task<MovieDto> UpdateMovieAsync(int movieId, MovieCreateDto movieDto);
        Task<MovieDto> UpdateMoviePartialAsync(int movieId, MoviePartialUpdateDto movieDto);
        Task<bool> DeleteMovieAsync(int movieId);
    }
}
