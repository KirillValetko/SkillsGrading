namespace SkillsGrading.DataAccess.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
