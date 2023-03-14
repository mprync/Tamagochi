using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tamagotchi.API.Actions;
using Tamagotchi.API.Helpers;
using Tamagotchi.API.Services.Interfaces;
using Tamagotchi.DataAccess.Models.Pagination;
using Tamagotchi.DataAccess.Models.Species;
using Tamagotchi.DataAccess.Responses;
using Tamagotchi.DataAccess.Responses.Errors;
using Tamagotchi.DataAccess.Responses.Pagination.Filter;

namespace Tamagotchi.API.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Produces("application/json")]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SpeciesController : ApiControllerBase
{
    private readonly ISpeciesService _speciesService;
    private readonly ILogger<SpeciesController> _logger;

    public SpeciesController(ISpeciesService speciesService, ILogger<SpeciesController> logger)
    {
        _speciesService = speciesService;
        _logger = logger;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedModel<SpeciesDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpGet]
    public async Task<HttpActionResult<PagedModel<SpeciesDto>>> Get([FromQuery] PaginationFilters pageFilter)
    {
        try
        {
            _logger.LogInformation("Getting species");
            return await QueryHelper.ExecuteQuery(() => _speciesService.Get(pageFilter));
        }
        catch (Exception e)
        {
            _logger.LogError("Error getting species: {0}", e.Message);
            throw;
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<SpeciesDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpGet("{id:int}")]
    public async Task<HttpActionResult<SpeciesDto>> GetById(int id)
    {
        try
        {
            _logger.LogInformation("Getting species by id");
            return await QueryHelper.ExecuteQuery(() => _speciesService.GetById(id));
        }
        catch (Exception e)
        {
            _logger.LogError("Error getting species by id: {0}", e.Message);
            throw;
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<SpeciesDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpPost]
    public async Task<HttpActionResult<SpeciesDto>> Create([FromBody] CreateSpeciesDto createSpeciesDto)
    {
        try
        {
            _logger.LogInformation("Creating species");
            return await QueryHelper.ExecuteQuery(() => _speciesService.Create(createSpeciesDto));

        }
        catch (Exception e)
        {
            _logger.LogError("Error creating species: {0}", e.Message);
            throw;
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<SpeciesDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpPut("{id:int}")]
    public async Task<HttpActionResult<SpeciesDto>> Update(int id, [FromBody] UpdateSpeciesDto updateSpeciesDto)
    {
        try
        {
            _logger.LogInformation("Updating species");
            return await QueryHelper.ExecuteQuery(() => _speciesService.Update(id, updateSpeciesDto));
        }
        catch (Exception e)
        {
            _logger.LogError("Error updating species: {0}", e.Message);
            throw;
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<SpeciesDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpDelete("{id:int}")]
    public async Task<HttpActionResult<Response>> Delete(int id)
    {
        try
        {
            _logger.LogInformation("Deleting species");
            return await QueryHelper.ExecuteQuery(() => _speciesService.Delete(id));
        }
        catch (Exception e)
        {
            _logger.LogError("Error deleting species: {0}", e.Message);
            throw;
        }
    }
}