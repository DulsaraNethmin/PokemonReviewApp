using AutoMapper;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Models.Pokemon, Dto.PokemonDto>();
            CreateMap<Models.Owner, Dto.OwnerDto>();
            CreateMap<Models.Category, Dto.CategoryDto>();
            CreateMap<Dto.CategoryDto, Models.Category>();
            CreateMap<Models.Country, Dto.CountryDto>();
            CreateMap<Dto.CountryDto, Models.Country>();
            CreateMap<Dto.OwnerCreateDto, Models.Owner>();
            CreateMap<Dto.PokemonDto, Models.Pokemon>();
            CreateMap<Dto.ReviewCreateDto, Models.Review>();
            CreateMap<Models.Reviewer, Dto.ReviewerDto>();
        }
    }
}
