﻿using AutoMapper;
using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.Common.Models;
using SkillsGrading.DataAccess.DataModels;

namespace SkillsGrading.BusinessLogic.Infrastructure
{
    public class BllMapperProfile : Profile
    {
        public BllMapperProfile()
        {
            CreateMap<PaginationResponse<EmployeeDataModel>, PaginationResponse<EmployeeModel>>();
            CreateMap<EmployeeDataModel, EmployeeModel>().ReverseMap();
            CreateMap<GradeDataModel, GradeModel>().ReverseMap();
            CreateMap<PaginationResponse<GradeTemplateDataModel>, PaginationResponse<GradeTemplateModel>>();
            CreateMap<GradeTemplateDataModel, GradeTemplateModel>().ReverseMap();
            CreateMap<PaginationResponse<GradedSkillSetDataModel>, PaginationResponse<GradedSkillSetModel>>();
            CreateMap<GradedSkillSetDataModel, GradedSkillSetModel>();
            CreateMap<GradedSkillSetModel, GradedSkillSetDataModel>()
                .AfterMap((model, dataModel) => 
                    dataModel.IsActive = true);
            CreateMap<PaginationResponse<GradeLevelDataModel>, PaginationResponse<GradeLevelModel>>();
            CreateMap<GradeLevelDataModel, GradeLevelModel>();
            CreateMap<GradeLevelModel, GradeLevelDataModel>()
                .AfterMap((model, dataModel) => 
                    dataModel.IsActive = true);
            CreateMap<PaginationResponse<SkillDataModel>, PaginationResponse<SkillModel>>();
            CreateMap<SkillDataModel, SkillModel>().ReverseMap();
            CreateMap<PaginationResponse<SkillGroupDataModel>, PaginationResponse<SkillGroupModel>>();
            CreateMap<SkillGroupDataModel, SkillGroupModel>();
            CreateMap<SkillGroupModel, SkillGroupDataModel>()
                .AfterMap((model, dataModel) =>
                    dataModel.IsActive = true);
            CreateMap<PaginationResponse<SkillLevelDataModel>, PaginationResponse<SkillLevelModel>>();
            CreateMap<SkillLevelDataModel, SkillLevelModel>();
            CreateMap<SkillLevelModel, SkillLevelDataModel>()
                .AfterMap((model, dataModel) => 
                    dataModel.IsActive = true);
        }
    }
}
