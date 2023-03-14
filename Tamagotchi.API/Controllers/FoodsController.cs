using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tamagotchi.API.Actions;
using Tamagotchi.API.Helpers;
using Tamagotchi.API.Services.Interfaces;
using Tamagotchi.DataAccess.Models.Food;
using Tamagotchi.DataAccess.Models.Pagination;
using Tamagotchi.DataAccess.Responses;
using Tamagotchi.DataAccess.Responses.Errors;
using Tamagotchi.DataAccess.Responses.Pagination.Filter;

namespace Tamagotchi.API.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Produces("application/json")]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class FoodsController : ApiControllerBase
{
    private readonly IFoodsService _foodService;
    private readonly ILogger<FoodsController> _logger;

    public FoodsController(IFoodsService foodService, ILogger<FoodsController> logger)
    {
        _foodService = foodService;
        _logger = logger;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedModel<FoodDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpGet]
    public async Task<HttpActionResult<PagedModel<FoodDto>>> Get([FromQuery] PaginationFilters pageFilter)
    {
        try
        {
            _logger.LogInformation("Getting foods all foods");
            return await QueryHelper.ExecuteQuery(() => _foodService.Get(pageFilter));
        }
        catch (Exception e)
        {
            _logger.LogError("Error getting foods: {0}", e.Message);
            throw;
        }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<FoodDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpGet("{id:int}")]
    public async Task<HttpActionResult<FoodDto>> GetById(int id)
    {
        try
        {
            _logger.LogInformation("Getting food by id");
            return await QueryHelper.ExecuteQuery(() => _foodService.GetById(id));
        }
        catch (Exception e)
        {
            _logger.LogError("Error getting food by id: {0}", e.Message);
            throw;
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<FoodDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpPost]
    public async Task<HttpActionResult<FoodDto>> Create([FromBody] CreateFoodDto createFoodDto)
    {
        try
        {
            _logger.LogInformation("Creating food");
            return await QueryHelper.ExecuteQuery(() => _foodService.Create(createFoodDto));
        }
        catch (Exception e)
        {
            _logger.LogError("Error creating food: {0}", e.Message);
            throw;
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<FoodDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpPut("{id:int}")]
    public async Task<HttpActionResult<FoodDto>> Update(int id, [FromBody] UpdateFoodDto updateFoodDto)
    {
        try
        {
            _logger.LogInformation("Updating food");
            return await QueryHelper.ExecuteQuery(() => _foodService.Update(id, updateFoodDto));
        }
        catch (Exception e)
        {
            _logger.LogError("Error updating food: {0}", e.Message);
            throw;
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Response>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpDelete("{id:int}")]
    public async Task<HttpActionResult<Response>> Delete(int id)
    {
        try
        {
            _logger.LogInformation("Deleting food");
            return await QueryHelper.ExecuteQuery(() => _foodService.Delete(id));
        }
        catch (Exception e)
        {
            _logger.LogError("Error deleting food: {0}", e.Message);
            throw;
        }
    }
}