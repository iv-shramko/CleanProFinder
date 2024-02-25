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
            CreateMap<EditProviderServiceDto, CleaningService>();
            CreateMap<CleaningService, EditProviderServiceDto>();            
            CreateMap<EditProviderServiceDto, CleaningServiceServiceProvider>();
            CreateMap<CleaningServiceServiceProvider, EditProviderServiceDto>();

            CreateMap<CleaningServiceServiceProvider, ProviderServiceFullInfoDto>()
                .ForMember(pS => pS.Name, otp => otp.MapFrom(src => src.CleaningService.Name))
                .ForMember(pS => pS.Price, otp => otp.MapFrom(src => src.Price));

            CreateMap<EditProviderServiceDto, CleaningServiceServiceProvider >()
                .ForMember(cSSP => cSSP.CleaningServiceId, otp => otp.MapFrom(src => src.CleaningServiceId))
                .ForMember(pS => pS.Description, otp => otp.MapFrom(src => src.Description))
                .ForMember(cSSP => cSSP.Price, otp => otp.MapFrom(src => src.Price));
            CreateMap<CreateCleaningServiceCommand, CleaningService>();
            CreateMap<EditCleaningServiceCommand, CleaningService>();
            CreateMap<CleaningService, CleaningServiceDto>();            
        }
    }
}
