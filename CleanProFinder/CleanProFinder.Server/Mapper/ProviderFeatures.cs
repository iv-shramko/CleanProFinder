using AutoMapper;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Features.CleaningServices;
using CleanProFinder.Shared.Dto.CleaningServices;

namespace CleanProFinder.Server.Mapper
{
    public class ProviderFeatures : Profile
    {
        public ProviderFeatures()
        {
            CreateMap<AddProviderServiceDto, CleaningService>();
            CreateMap<CleaningService, AddProviderServiceDto>();            CreateMap<AddProviderServiceDto, CleaningServiceServiceProvider>();
            CreateMap<CleaningServiceServiceProvider, AddProviderServiceDto>();

        }
    }
}
