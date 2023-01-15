using AutoMapper;
using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.Common.Models;
using SkillsGrading.Web.Models.DtoModels;
using SkillsGrading.Web.Models.ViewModels;

namespace SkillsGrading.Web.Infrastructure
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<PaginationResponse<SkillModel>, PaginationResponse<SkillViewModel>>();
            CreateMap<SkillModel, SkillViewModel>();
            CreateMap<SkillDto, SkillModel>();
            CreateMap<PaginationResponse<SkillGroupModel>, PaginationResponse<SkillGroupViewModel>>();
            CreateMap<SkillGroupModel, SkillGroupViewModel>();
            CreateMap<SkillGroupDto, SkillGroupModel>().AfterMap((dto, model) =>
                model.SkillLevels.ForEach(skillLevel => skillLevel.GroupId = dto.Id));
            CreateMap<PaginationResponse<SkillLevelModel>, PaginationResponse<SkillLevelViewModel>>();
            CreateMap<SkillLevelModel, SkillLevelViewModel>();
            CreateMap<SkillLevelDto, SkillLevelModel>();
        }
    }
}
