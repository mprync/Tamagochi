namespace Tamagotchi.DataAccess.Models.Pagination;

/// <summary>
/// Model used as part of the pagination queryable extension
/// </summary>
/// <typeparam name="TModel"></typeparam>
public class PagedModel<TModel>
{
    /// <summary>
    /// The size of the current page
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// The current page number
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// The total amount of items with this pagination request
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// The total amount of pages with this pagination request
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// The items for the <see cref="CurrentPage"/> of this pagination request
    /// </summary>
    public List<TModel> Items { get; set; }
}