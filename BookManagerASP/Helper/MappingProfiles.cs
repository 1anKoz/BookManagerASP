using AutoMapper;
using BookManagerASP.Dto;
using BookManagerASP.Models;
using BookManagerASP.Queries;

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

            CreateMap<ReviewEditDto, Review>();
            CreateMap<ShelfEditDto, Shelf>();
            CreateMap<QuoteEditDto, Quote>();
            CreateMap<BookPrivateEditDto, BookPrivate>();

            CreateMap<BookQuery, BookDto>();
            CreateMap<BookDto, BookQuery>();
        }
    }
}
