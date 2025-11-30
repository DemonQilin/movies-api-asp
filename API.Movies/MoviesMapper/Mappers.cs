using API.Movies.DAL.Models;
using API.Movies.DAL.Models.Dtos;
using AutoMapper;

namespace API.Movies.MoviesMapper
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategorySummaryDto>().ReverseMap();
            CreateMap<Category, CategoryDetailDto>().ReverseMap();

            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();
            CreateMap<Movie, MovieCreateDto>().ReverseMap();
            CreateMap<Movie, MovieSummaryDto>().ReverseMap();
            CreateMap<Movie, MovieDetailDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();
        }
    }
}