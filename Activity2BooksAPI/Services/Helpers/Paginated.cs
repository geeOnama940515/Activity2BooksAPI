using Activity2BooksAPI.Models.DTO;

namespace Activity2BooksAPI.Services.Helpers
{
    public static class Paginated
    {

        public static async Task<PaginatedResult<T>> GetPaginatedResults<T>(this IQueryable<T> queryable, int page, int pageSize)
        {
            page = page != 0 ? page : 1;
            pageSize = pageSize != 0 ? pageSize : 10;
            // Calculate the skip and take values for paging
            int skip = (page - 1) * pageSize;

            // Get the total count of records
            int totalRecords = queryable.Count();

            // Retrieve the paginated data asynchronously
            var paginatedData = queryable.Skip(skip).Take(pageSize).ToList();

            return new PaginatedResult<T>
            {
                TotalRecords = totalRecords,
                RecordsPerPage = pageSize,
                CurrentPage = page,
                Data = paginatedData
            };
        }
    }
}
