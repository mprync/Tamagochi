using Tamagotchi.API.Actions;
using Tamagotchi.DataAccess.Models.Food;
using Tamagotchi.DataAccess.Models.Pagination;
using Tamagotchi.DataAccess.Responses;
using Tamagotchi.DataAccess.Responses.Pagination.Filter;

namespace Tamagotchi.API.Services.Interfaces;

public interface IFoodsService
{
    Task<HttpActionResult<PagedModel<FoodDto>>> Get(PaginationFilters pageFilter);

    Task<HttpActionResult<FoodDto>> GetById(int id);
    
    Task<HttpActionResult<FoodDto>> Create(CreateFoodDto foodDto);
    
    Task<HttpActionResult<FoodDto>> Update(int id, UpdateFoodDto foodDto);
    
    Task<HttpActionResult<Response>> Delete(int id);
}