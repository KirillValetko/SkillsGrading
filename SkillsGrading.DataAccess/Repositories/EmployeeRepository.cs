using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkillsGrading.Common.Helpers.Interfaces;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Infrastructure;
using SkillsGrading.DataAccess.Models;
using SkillsGrading.DataAccess.Repositories.Interfaces;

namespace SkillsGrading.DataAccess.Repositories
{
    public class EmployeeRepository :
        BaseRepository<Employee, EmployeeDataModel, EmployeeFilter>,
        IEmployeeRepository
    {
        public EmployeeRepository(GradingContext gradingContext,
            IPaginationHelper<Employee> paginationHelper,
            IMapper mapper) : base(gradingContext, paginationHelper, mapper)
        {
        }

        protected override void PrepareForCreation(Employee item)
        {
            item.IsActive = true;
        }

        protected override IQueryable<Employee> AddFilterConditions(IQueryable<Employee> items, EmployeeFilter filter)
        {
            if (filter.GraderId.HasValue)
            {
                items = items.Where(employee => employee.GraderId.Equals(filter.GraderId));
            }

            items = items
                .Include(employee => employee.Grader)
                .Include(employee => employee.Grades)
                .ThenInclude(grade => grade.GradeTemplate)
                .Include(employee => employee.Grades)
                .ThenInclude(grade => grade.GradeLevel);

            return items;
        }
    }
}