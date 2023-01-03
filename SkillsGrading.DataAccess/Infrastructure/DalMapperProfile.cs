using AutoMapper;
using SkillsGrading.Common.Models;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Infrastructure
{
    public class DalMapperProfile : Profile
    {
        public DalMapperProfile()
        {
            CreateMap<PaginationResponse<Skill>, PaginationResponse<SkillDataModel>>();
            CreateMap<Skill, SkillDataModel>().ReverseMap();
            CreateMap<PaginationResponse<SkillGroup>, PaginationResponse<SkillGroupDataModel>>();
            CreateMap<SkillGroup, SkillGroupDataModel>().ReverseMap();
            CreateMap<PaginationResponse<SkillLevel>, PaginationResponse<SkillLevelDataModel>>();
            CreateMap<SkillLevel, SkillLevelDataModel>().ReverseMap();
        }
    }
}
