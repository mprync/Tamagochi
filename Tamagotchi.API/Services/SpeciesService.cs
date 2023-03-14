using Microsoft.EntityFrameworkCore;
using Tamagotchi.API.Actions;
using Tamagotchi.API.Extentions;
using Tamagotchi.API.Services.Interfaces;
using Tamagotchi.Data;
using Tamagotchi.Data.Models;
using Tamagotchi.DataAccess.Models.Pagination;
using Tamagotchi.DataAccess.Models.Species;
using Tamagotchi.DataAccess.Responses;
using Tamagotchi.DataAccess.Responses.Pagination.Filter;

namespace Tamagotchi.API.Services;

/// <summary>
/// The species service
///
/// TODO: Add logging
/// </summary>
public class SpeciesService : ISpeciesService
{
    private readonly TamagotchiDbContext _db;

    public SpeciesService(TamagotchiDbContext dbContext)
    {
        _db = dbContext;
    }

    ///<inheritdoc/>
    public async Task<HttpActionResult<PagedModel<SpeciesDto>>> Get(PaginationFilters pageFilter)
    {
        var species = await _db.Species
            .AsNoTracking()
            .Select(p => SpeciesDto.FromSpecies(p))
            .PaginateAsync(pageFilter.Page, pageFilter.Limit);

        return await HttpActionResult<PagedModel<SpeciesDto>>.Success(
            StatusCodes.Status200OK,
            species);
    }

    ///<inheritdoc/>
    public async Task<HttpActionResult<SpeciesDto>> GetById(int id)
    {
        var species = await _db.Species
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (species == null)
        {
            return await HttpActionResult<SpeciesDto>.Error(
                StatusCodes.Status400BadRequest,
                $"No species with id: {id} found");
        }

        return await HttpActionResult<SpeciesDto>.Success(
            StatusCodes.Status200OK,
            SpeciesDto.FromSpecies(species));
    }

    ///<inheritdoc/>
    public async Task<HttpActionResult<SpeciesDto>> Create(CreateSpeciesDto speciesDto)
    {
        var species = new Species
        {
            Name = speciesDto.Name,
            MaxAge = speciesDto.MaxAge,
            MaxWeight = speciesDto.MaxWeight,
            HungerRate = speciesDto.HungerRate,
            TickRateMs = speciesDto.TickRateMs,
            AgeRate = speciesDto.AgeRate
        };

        await _db.Species.AddAsync(species);
        await _db.SaveChangesAsync();

        return await HttpActionResult<SpeciesDto>.Success(
            StatusCodes.Status201Created,
            SpeciesDto.FromSpecies(species));
    }

    ///<inheritdoc/>
    public async Task<HttpActionResult<SpeciesDto>> Update(int id, UpdateSpeciesDto updateSpeciesDto)
    {
        var species = await _db.Species
            .FirstOrDefaultAsync(p => p.Id == id);

        if (species == null)
        {
            return await HttpActionResult<SpeciesDto>.Error(
                StatusCodes.Status400BadRequest,
                $"No species found with id: {id}");
        }

        species.Name = updateSpeciesDto.Name;

        await _db.SaveChangesAsync();

        return await HttpActionResult<SpeciesDto>.Success(
            StatusCodes.Status200OK,
            SpeciesDto.FromSpecies(species));
    }

    ///<inheritdoc/>
    public async Task<HttpActionResult<Response>> Delete(int id)
    {
        var species = await _db.Species
            .FirstOrDefaultAsync(p => p.Id == id);

        if (species == null)
        {
            return await HttpActionResult<Response>.Error(
                StatusCodes.Status400BadRequest,
                $"No species found with id: {id}");
        }

        _db.Species.Remove(species);
        await _db.SaveChangesAsync();

        return await HttpActionResult<Response>.Success(
            StatusCodes.Status200OK);
    }
}