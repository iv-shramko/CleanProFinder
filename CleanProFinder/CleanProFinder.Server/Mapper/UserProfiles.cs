using AutoMapper;
using CleanProFinder.Db.Models;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Dto.SavedProviders;

namespace CleanProFinder.Server.Mapper
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<UserProfileDto, ServiceUser>();
            CreateMap<ServiceUser, UserProfileDto>();
            CreateMap<ProviderProfileDto, CleaningServiceProvider>();
            CreateMap<CleaningServiceProvider, ProviderProfileDto>();
            CreateMap<UserProfileViewInfoDto, ServiceUser>();
            CreateMap<ServiceUser, UserProfileViewInfoDto>();
            CreateMap<ProviderProfileViewInfoDto, CleaningServiceProvider>();
            CreateMap<CleaningServiceProvider, ProviderProfileViewInfoDto>();
            CreateMap<CleaningServiceProvider, ProviderPreviewDto>();
            CreateMap<ProviderPreviewDto, CleaningServiceProvider>();
            CreateMap<SavedProvider, SavedProviderDto>()
                .ForMember(sP => sP.Name, otp => otp.MapFrom(src => src.CleaningServiceProvider.Name));
        }
    }
}
