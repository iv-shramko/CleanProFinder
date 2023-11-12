using AutoMapper;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Features.CleaningServices;
using CleanProFinder.Server.Features.Premises;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Premises;

namespace CleanProFinder.Server.Mapper
{
    public class UserFeatures : Profile
    {
        public UserFeatures()
        {
            CreateMap<CreatePremiseCommand, Premise>();
            CreateMap<EditPremiseCommand, Premise>();
            CreateMap<Premise, OwnPremiseFullInfoDto>();
            CreateMap<Premise, OwnPremiseShortInfoDto>();

        }
    }
}
