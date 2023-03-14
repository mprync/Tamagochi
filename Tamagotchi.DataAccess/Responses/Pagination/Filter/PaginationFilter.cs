namespace Tamagotchi.DataAccess.Responses.Pagination.Filter
{
    /// <summary>
    /// A paged filter object containing all the filters
    /// </summary>
    /// <param name="Limit"></param>
    /// <param name="Page"></param>
    public record PaginationFilters(
        int Limit = 50,
        int Page = 1);
}