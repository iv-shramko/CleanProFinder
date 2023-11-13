using AutoMapper;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Features.CleaningServices;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Requests;

namespace CleanProFinder.Server.Mapper
{
    public class ProviderFeatures : Profile
    {
        public ProviderFeatures()
        {
            CreateMap<CreateCleaningServiceCommand, CleaningService>();
            CreateMap<EditCleaningServiceCommand, CleaningService>();
            CreateMap<CleaningService, CleaningServiceDto>();
            CreateMap<Request, RequestShortInfoDto>()
                .ForMember(r => r.Services, otp => otp.MapFrom(src => src.Services))
                .ForMember(r => r.Square, otp => otp.MapFrom(src => src.Premise.Square))
                .ForMember(r => r.Address, otp => otp.MapFrom(src => src.Premise.Address));
        }
    }
}
