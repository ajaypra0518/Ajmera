using Assessment.ViewModel;
using AutoMapper;
using DataAccessLayer.Models;

namespace Assessment.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.Name, y => y.MapFrom(x => x.Names))
                .ForMember(x => x.AuthorName, y => y.MapFrom(x => x.AuthorName))
                .ReverseMap();
        }
    }
}
