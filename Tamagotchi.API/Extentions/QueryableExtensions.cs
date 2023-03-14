using Tamagotchi.DataAccess.Models.Pagination;

namespace Tamagotchi.API.Extentions;

public static class QueryableExtensions
{
    /// <summary>
    /// PaginateAsync the results of a collection
    /// </summary>
    /// <param name="query"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    public static async Task<PagedModel<TModel>> PaginateAsync<TModel>(
        this IEnumerable<TModel> query,
        int page,
        int limit)
    {
        return await Task.Run(
            () =>
            {
                var currentPage = (page < 0)
                    ? 1
                    : page;
                var startRow = (currentPage - 1) * limit;

                var items = query.ToList();
                var paged = new PagedModel<TModel>
                {
                    CurrentPage = currentPage,
                    PageSize = limit,
                    Items = items
                        .Skip(startRow)
                        .Take(limit)
                        .ToList(),
                    TotalItems = items.Count
                };

                paged.TotalPages = (int) Math.Ceiling(paged.TotalItems / (double) limit);

                return paged;
            });
    }
}