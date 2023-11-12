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
            CreateMap<CreateCleaningServiceCommand, CleaningService>();
            CreateMap<EditCleaningServiceCommand, CleaningService>();
            CreateMap<CleaningService, CleaningServiceDto>();
        }
    }
}
