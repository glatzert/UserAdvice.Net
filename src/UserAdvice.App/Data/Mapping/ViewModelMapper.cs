using AutoMapper;
using System.Linq;

namespace UserAdvice.Data.Mapping
{
    internal static class ViewModelMapper
    {
        private static IMapper _mapper;

        static ViewModelMapper()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            }).CreateMapper();
        }

        public static ViewModel.PostTeaser ToViewModel(this Entities.Post post) 
            => _mapper.Map<ViewModel.PostTeaser>(post);

        public static ViewModel.Category ToViewModel(this Entities.Category cat)
            => _mapper.Map<ViewModel.Category>(cat);

        internal class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<Entities.Post, ViewModel.PostTeaser>()
                    .ForMember(d => d.Tags, m => m.MapFrom(s => s.PostTags.Select(p => p.Tag)))
                    .ForMember(d => d.StatusComments, m => m.MapFrom(s => s.Comments));

                CreateMap<Entities.Category, ViewModel.Category>();
                CreateMap<Entities.Category, ViewModel.CategoryRef>();

                CreateMap<Entities.Comment, ViewModel.Comment>();
                CreateMap<Entities.Status, ViewModel.Status>();
                CreateMap<Entities.Tag, ViewModel.Tag>();
            }
        }
    }
}
