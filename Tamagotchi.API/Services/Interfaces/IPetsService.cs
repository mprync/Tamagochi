using Tamagotchi.API.Actions;
using Tamagotchi.Data.Models;
using Tamagotchi.DataAccess.Models.Pagination;
using Tamagotchi.DataAccess.Models.Pet;
using Tamagotchi.DataAccess.Responses;
using Tamagotchi.DataAccess.Responses.Pagination.Filter;

namespace Tamagotchi.API.Services.Interfaces;

/// <summary>
/// Interface for <see cref="PetService"/>
/// </summary>
public interface IPetsService
{
    /// <summary>s
    /// Get all <see cref="Pet"/>'s a user has as a paginated result
    /// </summary>
    /// <param name="pageFilter"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<HttpActionResult<PagedModel<PetDto>>> Get(PaginationFilters pageFilter, int userId);
    
    Task<HttpActionResult<PetDto>> GetById(int id, int userId);

    Task<HttpActionResult<PetDto>> Create(CreatePetDto createPetDto, int userId);

    Task<HttpActionResult<Response>> Delete(int id, int userId);
    
    Task<HttpActionResult<PetDto>> Feed(int petDto, FeedPetDto feedPetDto, int userId);
    
    Task<HttpActionResult<PetDto>> Affection(int id, int userId);
    
    Task<HttpActionResult<PetDto>> Update(int petId, UpdatePetDto updatePetDto, int userId);
}