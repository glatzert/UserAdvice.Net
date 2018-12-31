using AutoMapper;

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
        {
            var result = _mapper.Map<ViewModel.PostTeaser>(post);

            return result;
        }

        internal class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<Entities.Post, ViewModel.PostTeaser>();
                CreateMap<Entities.Comment, ViewModel.PostTeaser.StatusComment>();
                CreateMap<Entities.Category, ViewModel.Category>();
                CreateMap<Entities.Category, ViewModel.CategoryRef>();
            }
        }
    }
}
