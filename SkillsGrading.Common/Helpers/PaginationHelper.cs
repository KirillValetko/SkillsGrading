using Microsoft.EntityFrameworkCore;
using SkillsGrading.Common.Constants;
using SkillsGrading.Common.Helpers.Interfaces;
using SkillsGrading.Common.Models;

namespace SkillsGrading.Common.Helpers
{
    public class PaginationHelper<T> : IPaginationHelper<T> where T : class
    {
        public async Task<PaginationResponse<T>> PaginateAsync(IQueryable<T> source, int? pageNumber, int? limit)
        {
            if (pageNumber is not > 0)
            {
                pageNumber = PaginationDefaultConstants.PageNumber;
            }

            if (limit is not > 0)
            {
                limit = PaginationDefaultConstants.Limit;
            }

            var itemCount = await source.CountAsync();
            var pageCount = (int)Math.Ceiling((double)itemCount / limit.Value);
            var paginatedItems = await source.Skip(pageCount*(pageNumber.Value - 1)).Take(pageCount).ToListAsync();

            var response = new PaginationResponse<T>
            {
                CurrentPage = pageNumber.Value,
                PageCount = pageCount,
                PaginatedData = paginatedItems
            };

            return response;
        }
    }
}
