using AutoMapper;
using BookstoreApplication.DTO;
using BookstoreApplication.Models;

namespace BookstoreApplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author != null ? src.Author.FullName : string.Empty))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Name : string.Empty))
                .ForMember(dest => dest.YearsAgoPublished, opt => opt.MapFrom(src => DateTime.Now.Year - src.PublishedDate.Year));

            CreateMap<Book, BookDetailsDto>()
                .ForMember(dest => dest.AuthorFullName, opt => opt.MapFrom(src => src.Author != null ? src.Author.FullName : string.Empty))
                .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Name : string.Empty));

            CreateMap<Author, AuthorDto>();
            CreateMap<Award, AwardDto>();
            CreateMap<Publisher, PublisherDto>();

        }
    }
}
