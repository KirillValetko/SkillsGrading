using FluentValidation;
using SkillsGrading.Web.Models.DtoModels;

namespace SkillsGrading.Web.Validators
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {
            RuleFor(employee => employee.Id).NotEmpty();
            RuleFor(employee => employee.GraderId).NotEmpty();
        }
    }
}
