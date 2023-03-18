using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tamagotchi.API.Actions;
using Tamagotchi.API.Helpers;
using Tamagotchi.API.Services.Interfaces;
using Tamagotchi.DataAccess.Models.Pagination;
using Tamagotchi.DataAccess.Models.Pet;
using Tamagotchi.DataAccess.Responses;
using Tamagotchi.DataAccess.Responses.Errors;
using Tamagotchi.DataAccess.Responses.Pagination.Filter;

namespace Tamagotchi.API.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Produces("application/json")]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PetsController : ApiControllerBase
{
    private readonly ILogger<PetsController> _logger;
    private readonly IPetsService _service;

    public PetsController(IPetsService service, ILogger<PetsController> logger)
    {
        _logger = logger;
        _service = service;
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedModel<PetDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpGet]
    public async Task<HttpActionResult<PagedModel<PetDto>>> Get(
        [FromQuery] PaginationFilters pageFilter)
    {
        try
        {
            _logger.LogInformation("Getting pets");
            return await QueryHelper.ExecuteQuery(() => _service.Get(pageFilter, GetUserId()));
        }
        catch (Exception e)
        {
            _logger.LogError("Error getting pets: {0}", e.Message);
            throw;
        }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PetDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpGet("{id}")]
    public async Task<HttpActionResult<PetDto>> GetById(int id)
    {
        try
        {
            _logger.LogInformation("Getting pet by id");
            return await QueryHelper.ExecuteQuery(() => _service.GetById(id, GetUserId()));
        }
        catch (Exception e)
        {
            _logger.LogError("Error getting pet by id: {0}", e.Message);
            throw;
        }
    }
    
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PetDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpPost]
    public async Task<HttpActionResult<PetDto>> Create([FromBody] CreatePetDto createPetDto)
    {
        try
        {
            _logger.LogInformation("Creating pet");
            return await QueryHelper.ExecuteQuery(() => _service.Create(createPetDto, GetUserId()));
        }
        catch (Exception e)
        {
            _logger.LogError("Error creating pet: {0}", e.Message);
            throw;
        }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PetDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpPost("{id:int}/Feed")]
    public async Task<HttpActionResult<PetDto>> Feed(int id, [FromBody] FeedPetDto feedPetDto)
    {
        try
        {
            _logger.LogInformation("Feeding pet");
            return await QueryHelper.ExecuteQuery(() => _service.Feed(id, feedPetDto, GetUserId()));
        }
        catch (Exception e)
        {
            _logger.LogError("Error feeding pet: {0}", e.Message);
            throw;
        }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PetDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpPost("{id:int}/Affection")]
    public async Task<HttpActionResult<PetDto>> Affection(int id)
    {
        try
        {
            _logger.LogInformation("Giving affection to pet");
            return await QueryHelper.ExecuteQuery(() => _service.Affection(id, GetUserId()));
        }
        catch (Exception e)
        {
            _logger.LogError("Error giving affection to pet: {0}", e.Message);
            throw;
        }
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpDelete("Delete")]
    public async Task<HttpActionResult<Response>> Delete(int id)
    {
        try
        {
            _logger.LogInformation("Deleting pet");
            return await QueryHelper.ExecuteQuery(() => _service.Delete(id, GetUserId()));
        }
        catch (Exception e)
        {
            _logger.LogError("Error deleting pet: {0}", e.Message);
            throw;
        }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatePetDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpPut("{id:int}")]
    public async Task<HttpActionResult<PetDto>> Update(int id, [FromBody] UpdatePetDto updatePetDto)
    {
        try
        {
            _logger.LogInformation("Updating pet");
            return await QueryHelper.ExecuteQuery(() => _service.Update(id, updatePetDto, GetUserId()));
        }
        catch (Exception e)
        {
            _logger.LogError("Error renaming pet: {0}", e.Message);
            throw;
        }
    }
}