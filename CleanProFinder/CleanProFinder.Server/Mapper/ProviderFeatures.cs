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
            CreateMap<EditProviderServiceDto, CleaningService>();
            CreateMap<CleaningService, EditProviderServiceDto>();            
            CreateMap<EditProviderServiceDto, CleaningServiceServiceProvider>();
            CreateMap<CleaningServiceServiceProvider, EditProviderServiceDto>();

            CreateMap<CleaningServiceServiceProvider, ProviderServiceFullInfoDto>()
                .ForMember(pS => pS.Name, otp => otp.MapFrom(src => src.CleaningService.Name))
                .ForMember(pS => pS.Description, otp => otp.MapFrom(src => src.CleaningService.Description))
                .ForMember(pS => pS.Price, otp => otp.MapFrom(src => src.Price));

            CreateMap<EditProviderServiceDto, CleaningServiceServiceProvider >()
                .ForMember(cSSP => cSSP.CleaningServiceId, otp => otp.MapFrom(src => src.CleaningServiceId))
                .ForMember(cSSP => cSSP.Price, otp => otp.MapFrom(src => src.Price));
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
