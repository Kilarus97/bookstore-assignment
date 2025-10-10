using AutoMapper;
using BookstoreApplication.DTO;
using BookstoreApplication.Models;

namespace BookstoreApplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDetailsDto>()
               .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Author != null ? src.Author.Id : 0))
               .ForMember(dest => dest.AuthorFullName, opt => opt.MapFrom(src => src.Author != null ? src.Author.FullName : string.Empty))
               .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Id : 0))
               .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Name : string.Empty))
               .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Website : string.Empty));

            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author != null ? src.Author.FullName : string.Empty))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Name : string.Empty))
                .ForMember(dest => dest.YearsAgoPublished, opt => opt.MapFrom(src => DateTime.UtcNow.Year - src.PublishedDate.Year));
            CreateMap<Author, AuthorDto>();
            CreateMap<Award, AwardDto>();
            CreateMap<Publisher, PublisherDto>();

        }
    }
}
