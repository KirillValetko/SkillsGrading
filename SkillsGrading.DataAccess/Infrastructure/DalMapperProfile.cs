using AutoMapper;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Infrastructure
{
    public class DalMapperProfile : Profile
    {
        public DalMapperProfile()
        {
            CreateMap<SkillGroup, SkillGroupDataModel>().ReverseMap();
            CreateMap<SkillLevel, SkillLevelDataModel>().ReverseMap();
        }
    }
}
