using AutoMapper;
using CleanProFinder.Db.Models;
using CleanProFinder.Shared.Dto.Requests;

namespace CleanProFinder.Server.Mapper
{
    public class RequestProfiles : Profile
    {
        public RequestProfiles()
        {
            CreateMap<Request, RequestShortInfoDto>()
                .ForMember(r => r.Services, otp => otp.MapFrom(src => src.Services))
                .ForMember(r => r.Square, otp => otp.MapFrom(src => src.Premise.Square))
                .ForMember(r => r.Address, otp => otp.MapFrom(src => src.Premise.Address))
                .ForMember(r => r.Status, otp => otp.MapFrom(src => src.Status.ToString()));

            CreateMap<Request, RequestFullInfoDto>()
                .ForMember(r => r.Description, otp => otp.MapFrom(src => src.Description))
                .ForMember(r => r.Services, otp => otp.MapFrom(src => src.Services))
                .ForMember(r => r.Square, otp => otp.MapFrom(src => src.Premise.Square))
                .ForMember(r => r.Address, otp => otp.MapFrom(src => src.Premise.Address))
                .ForMember(r => r.Status, otp => otp.MapFrom(src => src.Status.ToString()))
                .ForMember(r => r.Providers, otp => otp.MapFrom(src => src.Interactions.Select(i => i.Provider)));

            CreateMap<CleaningServiceProvider, ProviderRequestInteractionInfo>()
                .ForMember(i => i.ProviderId, otp => otp.MapFrom(src => src.Id))
                .ForMember(i => i.ProviderName, otp => otp.MapFrom(src => src.Name));
        }
    }
}
