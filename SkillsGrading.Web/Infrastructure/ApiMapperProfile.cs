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
            CreateMap<EmployeeDto, EmployeeModel>();
            CreateMap<EmployeeModel, EmployeeViewModel>();
            CreateMap<GradeModel, GradeViewModel>();
            CreateMap<PaginationResponse<GradeTemplateModel>, PaginationResponse<GradeTemplateViewModel>>();
            CreateMap<GradeTemplateDto, GradeTemplateModel>()
                .AfterMap((dto, model) =>
                    model.GradedSkillSets.ForEach(gradedSkillSet => gradedSkillSet.GradeTemplateId = dto.Id));
            CreateMap<GradeTemplateModel, GradeTemplateViewModel>();
            CreateMap<PaginationResponse<GradedSkillSetModel>, PaginationResponse<GradedSkillSetViewModel>>();
            CreateMap<GradedSkillSetDto, GradedSkillSetModel>();
            CreateMap<GradedSkillSetModel, GradedSkillSetViewModel>();
            CreateMap<PaginationResponse<GradeLevelModel>, PaginationResponse<GradeLevelViewModel>>();
            CreateMap<GradeLevelDto, GradeLevelModel>();
            CreateMap<GradeLevelModel, GradeLevelViewModel>();
            CreateMap<PaginationResponse<SkillModel>, PaginationResponse<SkillViewModel>>();
            CreateMap<SkillModel, SkillViewModel>();
            CreateMap<SkillDto, SkillModel>();
            CreateMap<PaginationResponse<SkillGroupModel>, PaginationResponse<SkillGroupViewModel>>();
            CreateMap<SkillGroupModel, SkillGroupViewModel>();
            CreateMap<SkillGroupDto, SkillGroupModel>()
                .AfterMap((dto, model) =>
                    model.SkillLevels.ForEach(skillLevel => skillLevel.GroupId = dto.Id));
            CreateMap<PaginationResponse<SkillLevelModel>, PaginationResponse<SkillLevelViewModel>>();
            CreateMap<SkillLevelModel, SkillLevelViewModel>();
            CreateMap<SkillLevelDto, SkillLevelModel>();
            CreateMap<GradingDto, GradingModel>();
            CreateMap<SkillSetDto, SkillSetModel>();
        }
    }
}
