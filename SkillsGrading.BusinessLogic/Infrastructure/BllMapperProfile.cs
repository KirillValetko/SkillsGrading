using AutoMapper;
using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.DataAccess.DataModels;

namespace SkillsGrading.BusinessLogic.Infrastructure
{
    public class BllMapperProfile : Profile
    {
        public BllMapperProfile()
        {
            CreateMap<SkillGroupDataModel, SkillGroupModel>().ReverseMap();
            CreateMap<SkillLevelDataModel, SkillLevelModel>().ReverseMap();
        }
    }
}
