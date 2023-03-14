using Tamagotchi.API.Actions;
using Tamagotchi.DataAccess.Models.Pagination;
using Tamagotchi.DataAccess.Models.Species;
using Tamagotchi.DataAccess.Responses;
using Tamagotchi.DataAccess.Responses.Pagination.Filter;

namespace Tamagotchi.API.Services.Interfaces;

public interface ISpeciesService
{
    Task<HttpActionResult<PagedModel<SpeciesDto>>> Get(PaginationFilters pageFilter);

    Task<HttpActionResult<SpeciesDto>> GetById(int id);
    
    Task<HttpActionResult<SpeciesDto>> Create(CreateSpeciesDto speciesDto);
    
    Task<HttpActionResult<SpeciesDto>> Update(int id, UpdateSpeciesDto speciesDto);
    
    Task<HttpActionResult<Response>> Delete(int id);
}