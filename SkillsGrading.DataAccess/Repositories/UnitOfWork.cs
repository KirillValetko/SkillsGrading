using SkillsGrading.DataAccess.Infrastructure;
using SkillsGrading.DataAccess.Repositories.Interfaces;

namespace SkillsGrading.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GradingContext _gradingContext;

        public UnitOfWork(GradingContext gradingContext)
        {
            _gradingContext = gradingContext;
        }

        public async Task SaveAsync()
        {
            await _gradingContext.SaveChangesAsync();
        }
    }
}
