using AutoMapper;
using CleanProFinder.Db.Models;
using CleanProFinder.Shared.Dto.Profile;

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
        }
    }
}
