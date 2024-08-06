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
        }
    }
}
