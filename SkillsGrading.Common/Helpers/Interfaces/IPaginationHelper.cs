using SkillsGrading.Common.Models;

namespace SkillsGrading.Common.Helpers.Interfaces
{
    public interface IPaginationHelper<T> where T : class
    {
        Task<PaginationResponse<T>> PaginateAsync(IQueryable<T> source, int? pageNumber, int? limit);
    }
}
