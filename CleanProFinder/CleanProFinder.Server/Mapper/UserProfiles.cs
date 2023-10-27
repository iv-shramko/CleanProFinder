using AutoMapper;
using CleanProFinder.Db.Models;
using CleanProFinder.Shared.Dto.Profile;

namespace CleanProFinder.Server.Mapper
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<ServiceUser, UserProfileDto>();
        }
    }
}
