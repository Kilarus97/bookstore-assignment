using AutoMapper;
using BookstoreApplication.DTO;
using BookstoreApplication.DTO.Login.Request;
using BookstoreApplication.DTO.Register.Request;
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
                .ForMember(dest => dest.YearsAgoPublished, opt => opt.MapFrom(src => DateTime.UtcNow.Year - src.PublishedDate.Year))
                .ForMember(dest => dest.AvarageRating, opt => opt.MapFrom(src => src.AverageRating));

            CreateMap<Author, AuthorDto>();
            CreateMap<Award, AwardDto>();
            CreateMap<Publisher, PublisherDto>();
            CreateMap<RegistrationDto,User>();


            CreateMap<User, ProfileRequestDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname));

            CreateMap<IssueDto, Issue>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.IssueNumber, opt => opt.MapFrom(src => src.IssueNumber))
            .ForMember(dest => dest.CoverDate, opt => opt.MapFrom(src => src.CoverDate))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image.OriginalUrl))
            .ForMember(dest => dest.ExternalId, opt => opt.Ignore())
            .ForMember(dest => dest.Price, opt => opt.Ignore())
            .ForMember(dest => dest.AvailableCopies, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.VolumeId, opt => opt.Ignore())
            .ForMember(dest => dest.Volume, opt => opt.Ignore());

            CreateMap<Issue, IssueDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.IssueNumber, opt => opt.MapFrom(src => src.IssueNumber))
            .ForMember(dest => dest.CoverDate, opt => opt.MapFrom(src => src.CoverDate))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => new ImageDto { OriginalUrl = src.ImageUrl }))
            .ForMember(dest => dest.Volume, opt => opt.MapFrom(src => src.Volume != null ? new VolumeDto { Id = src.Volume.ExternalId, Name = src.Volume.Name } : null));

            CreateMap<VolumeDto, Volume>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Aliases, opt => opt.MapFrom(src => src.Aliases))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.StartYear, opt => opt.MapFrom(src => src.StartYear))
            .ForMember(dest => dest.SiteUrl, opt => opt.Ignore()) // dodaj ako postoji u DTO
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()) // dodaj ako postoji u DTO
            .ForMember(dest => dest.PublisherId, opt => opt.Ignore()) // moraš da rešiš ručno
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
            .ForMember(dest => dest.Issues, opt => opt.Ignore());

            CreateMap<PublisherDto, Publisher>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // lokalni ID iz baze
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
            .ForMember(dest => dest.Address, opt => opt.Ignore()); // ako koristiš Address u modelu


        }
    }
}
