using AutoMapper;
using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.Web.Models.DtoModels;
using SkillsGrading.Web.Models.ViewModels;

namespace SkillsGrading.Web.Infrastructure
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<SkillGroupModel, SkillGroupViewModel>();
            CreateMap<SkillGroupDto, SkillGroupModel>();
            CreateMap<SkillLevelModel, SkillLevelViewModel>();
            CreateMap<SkillLevelDto, SkillLevelModel>();
        }
    }
}
