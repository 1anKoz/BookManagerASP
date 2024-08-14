using AutoMapper;
using BookManagerASP.Dto;
using BookManagerASP.Models;

namespace BookManagerASP.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
            
            CreateMap<QuoteDto, Quote>();
            CreateMap<Quote, QuoteDto>();

            CreateMap<UserEntity, UserEntityDto>(MemberList.Source)
                 .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<UserEntityDto, UserEntity>();

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();

            CreateMap<Shelf, ShelfDto>();
            CreateMap<ShelfDto, Shelf>();

            CreateMap<BookPrivate, BookPrivateDto>();
            CreateMap<BookPrivateDto, BookPrivate>();
        }
    }
}
