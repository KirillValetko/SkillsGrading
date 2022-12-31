using AutoMapper;
using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.Common.Models;
using SkillsGrading.DataAccess.DataModels;

namespace SkillsGrading.BusinessLogic.Infrastructure
{
    public class BllMapperProfile : Profile
    {
        public BllMapperProfile()
        {
            CreateMap<PaginationResponse<SkillGroupDataModel>, PaginationResponse<SkillGroupModel>>();
            CreateMap<SkillGroupDataModel, SkillGroupModel>().ReverseMap();
            CreateMap<PaginationResponse<SkillLevelDataModel>, PaginationResponse<SkillLevelModel>>();
            CreateMap<SkillLevelDataModel, SkillLevelModel>().ReverseMap();
        }
    }
}
