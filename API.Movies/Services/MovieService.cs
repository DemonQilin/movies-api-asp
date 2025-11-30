using API.Movies.DAL.Models;
using API.Movies.DAL.Models.Dtos;
using API.Movies.Repository.IRepository;
using API.Movies.Services.IServices;
using AutoMapper;

namespace API.Movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly ICaregoryRepository _categoryRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(ICaregoryRepository categoryRepository, IMovieRepository movieRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<MovieDto> CreateMovieAsync(MovieCreateDto movieDto)
        {
            var movieExists = await _movieRepository.MovieExistsAsync(movieDto.Name);
            if (movieExists)
            {
                throw new InvalidOperationException($"Movie with name {movieDto.Name} already exists");
            }

            var categoryExists = await _categoryRepository.CategoryExistsByIdAsync(movieDto.CategoryId);
            if (!categoryExists)
            {
                throw new InvalidOperationException($"Category with id {movieDto.CategoryId} does not exist");
            }

            var movie = _mapper.Map<Movie>(movieDto);
            if (!await _movieRepository.CreateMovieAsync(movie))
            {
                throw new Exception($"Something went wrong when saving the movie {movie.Name}");
            }

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<bool> DeleteMovieAsync(int movieId)
        {
            if (!await _movieRepository.MovieExistsAsync(movieId))
            {
                throw new InvalidOperationException($"Movie with id {movieId} does not exist");
            }

            if (!await _movieRepository.DeleteMovieAsync(movieId))
            {
                throw new Exception($"Something went wrong when deleting the movie {movieId}");
            }

            return true;
        }

        public async Task<MovieDetailDto?> GetMovieAsync(int movieId)
        {
            var movie = await _movieRepository.GetMovieAsync(movieId);
            return _mapper.Map<MovieDetailDto>(movie);
        }

        public async Task<ICollection<MovieDto>> GetMoviesAsync()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            return _mapper.Map<ICollection<MovieDto>>(movies);
        }

        public async Task<MovieDto> UpdateMovieAsync(int movieId, MovieCreateDto movieDto)
        {
            var movie = await _movieRepository.GetMovieAsync(movieId);
            if (movie == null)
            {
                throw new InvalidOperationException($"Movie with id {movieId} does not exist");
            }

            if (await _movieRepository.MovieExistsAsync(movieDto.Name) && movie.Name != movieDto.Name)
            {
                throw new InvalidOperationException($"Movie with name {movieDto.Name} already exists");
            }

            _mapper.Map(movieDto, movie);

            if (!await _movieRepository.UpdateMovieAsync(movie))
            {
                throw new Exception($"Something went wrong when updating the movie {movie.Name}");
            }

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> UpdateMoviePartialAsync(int movieId, MoviePartialUpdateDto movieDto)
        {
            if (movieDto.Name == null && movieDto.Duration == null && movieDto.Description == null && movieDto.Clasification == null && movieDto.CategoryId == null)
            {
                throw new ArgumentException("At least one property must be provided for update");
            }

            var movie = await _movieRepository.GetMovieAsync(movieId);
            if (movie == null)
            {
                throw new InvalidOperationException($"Movie with id {movieId} does not exist");
            }

            if (movieDto.Name != null)
            {
                if (await _movieRepository.MovieExistsAsync(movieDto.Name) && movie.Name != movieDto.Name)
                {
                    throw new InvalidOperationException($"Movie with name {movieDto.Name} already exists");
                }
                movie.Name = movieDto.Name;
            }

            if (movieDto.Duration != null)
            {
                movie.Duration = movieDto.Duration.Value;
            }

            if (movieDto.Description != null)
            {
                movie.Description = movieDto.Description;
            }

            if (movieDto.Clasification != null)
            {
                movie.Clasification = movieDto.Clasification;
            }

            if (movieDto.CategoryId != null)
            {
                var categoryExists = await _categoryRepository.CategoryExistsByIdAsync(movieDto.CategoryId.Value);
                if (!categoryExists)
                {
                    throw new InvalidOperationException($"Category with id {movieDto.CategoryId} does not exist");
                }
                movie.CategoryId = movieDto.CategoryId.Value;
            }

            if (!await _movieRepository.UpdateMovieAsync(movie))
            {
                throw new Exception($"Something went wrong when updating the movie {movie.Name}");
            }

            return _mapper.Map<MovieDto>(movie);
        }
    }
}
