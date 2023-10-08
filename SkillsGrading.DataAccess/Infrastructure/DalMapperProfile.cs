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
            CreateMap<PaginationResponse<Employee>, PaginationResponse<EmployeeDataModel>>();
            CreateMap<Employee, EmployeeDataModel>().ForMember(dest => dest.Grades,
                opt => opt.MapFrom(
                    src => src.Grades
                        .GroupBy(grade => grade.GradeTemplateId)
                        .Select(gradeGroup => 
                            gradeGroup.OrderByDescending(grade => grade.GradeDate).First())));
            CreateMap<EmployeeDataModel, Employee>();
            CreateMap<Grade, GradeDataModel>().ReverseMap();
            CreateMap<PaginationResponse<GradeTemplate>, PaginationResponse<GradeTemplateDataModel>>();
            CreateMap<GradeTemplate, GradeTemplateDataModel>().ReverseMap();
            CreateMap<PaginationResponse<GradedSkillSet>, PaginationResponse<GradedSkillSetDataModel>>();
            CreateMap<GradedSkillSet, GradedSkillSetDataModel>().ReverseMap();
            CreateMap<PaginationResponse<GradeLevel>, PaginationResponse<GradeLevelDataModel>>();
            CreateMap<GradeLevel, GradeLevelDataModel>().ReverseMap();
            CreateMap<PaginationResponse<Skill>, PaginationResponse<SkillDataModel>>();
            CreateMap<Skill, SkillDataModel>().ReverseMap();
            CreateMap<PaginationResponse<SkillGroup>, PaginationResponse<SkillGroupDataModel>>();
            CreateMap<SkillGroup, SkillGroupDataModel>();
            CreateMap<SkillGroupDataModel, SkillGroup>()
                .ForMember(dest => dest.Skills, 
                    opt => opt.Ignore());
            CreateMap<PaginationResponse<SkillLevel>, PaginationResponse<SkillLevelDataModel>>();
            CreateMap<SkillLevel, SkillLevelDataModel>().ReverseMap();
        }
    }
}
