using Microsoft.EntityFrameworkCore;
using Tamagotchi.API.Actions;
using Tamagotchi.API.Extentions;
using Tamagotchi.API.Services.Interfaces;
using Tamagotchi.Data;
using Tamagotchi.Data.Enums;
using Tamagotchi.Data.Models;
using Tamagotchi.DataAccess.Models.Pagination;
using Tamagotchi.DataAccess.Models.Pet;
using Tamagotchi.DataAccess.Responses;
using Tamagotchi.DataAccess.Responses.Pagination.Filter;

namespace Tamagotchi.API.Services;

/// <summary>
/// The pet service
///
/// TODO: Add logging
/// </summary>
public class PetService : IPetsService
{
    private readonly TamagotchiDbContext _db;

    public PetService(TamagotchiDbContext db)
    {
        _db = db;
    }

    /// <inheritdoc/>
    public async Task<HttpActionResult<PagedModel<PetDto>>> Get(PaginationFilters pageFilter, int userId)
    {
        var pets = await _db.Pets
            .Include(p => p.Species)
            .Where(p => p.UserId == userId)
            .ToListAsync();

        foreach (var pet in pets)
        {
            pet.CalculateStats();
        }

        await _db.SaveChangesAsync();

        var result = await pets.Select(PetDto.FromPet)
            .PaginateAsync(pageFilter.Page, pageFilter.Limit);

        return await HttpActionResult<PagedModel<PetDto>>.Success(
            StatusCodes.Status200OK,
            result);
    }

    /// <inheritdoc/>
    public async Task<HttpActionResult<PetDto>> GetById(int id, int userId)
    {
        var pet = await _db.Pets
            .Include(p => p.Species)
            .FirstOrDefaultAsync(p => p.UserId == userId && p.Id == id);

        if (pet == null)
        {
            return await HttpActionResult<PetDto>.Error(
                StatusCodes.Status400BadRequest,
                $"No pet with id: {id} found");
        }
        
        pet.CalculateStats();
        await _db.SaveChangesAsync();

        return await HttpActionResult<PetDto>.Success(
            StatusCodes.Status200OK,
            PetDto.FromPet(pet));
    }

    ///<inheritdoc/>
    public async Task<HttpActionResult<PetDto>> Create(CreatePetDto createPetDto, int userId)
    {
        var pet = await _db.Pets.AddAsync(new Pet
        {
            Name = createPetDto.Name,
            SpeciesId = createPetDto.SpeciesId,
            UserId = userId
        });

        await _db.SaveChangesAsync();

        return await HttpActionResult<PetDto>.Success(
            StatusCodes.Status200OK,
            PetDto.FromPet(pet.Entity));
    }

    ///<inheritdoc/>
    public async Task<HttpActionResult<Response>> Delete(int id, int userId)
    {
        var pet = await _db.Pets
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

        if (pet == null)
        {
            return await HttpActionResult<Response>.Error(
                StatusCodes.Status400BadRequest,
                $"No createPetDto with id: {id} found");
        }

        _db.Pets.Remove(pet);
        await _db.SaveChangesAsync();

        return await HttpActionResult<Response>.Success(
            StatusCodes.Status204NoContent);
    }

    ///<inheritdoc/>
    public async Task<HttpActionResult<PetDto>> Feed(int petId, FeedPetDto feedPetDto, int userId)
    {
        var pet = await _db.Pets
            .Include(p => p.Species)
            .ThenInclude(s => s.Foods)
            .FirstOrDefaultAsync(p => p.Id == petId && p.UserId == userId);

        if (pet == null)
        {
            return await HttpActionResult<PetDto>.Error(
                StatusCodes.Status400BadRequest,
                $"Pet with id: {petId} not found for userId: {userId}");
        }

        var food = await _db.Foods
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == feedPetDto.FoodId);

        if (food == null)
        {
            return await HttpActionResult<PetDto>.Error(
                StatusCodes.Status400BadRequest,
                $"No food with id: {feedPetDto.FoodId} found");
        }

        if (pet.Species?.Foods == null)
        {
            return await HttpActionResult<PetDto>.Error(
                StatusCodes.Status400BadRequest,
                $"Pet has no preferred foods? PetId: {petId}");
        }

        if (pet.Species.Foods.All(f => f.Id != feedPetDto.FoodId))
        {
            return await HttpActionResult<PetDto>.Error(
                StatusCodes.Status400BadRequest,
                $"This pet doesn't like food with id: {feedPetDto.FoodId}, try another?");
        }

        pet.Hunger = HungerLevelType.Full;
        pet.LastFed = DateTime.UtcNow;
        pet.CalculateStats();

        await _db.SaveChangesAsync();

        return await HttpActionResult<PetDto>.Success(
            StatusCodes.Status200OK,
            PetDto.FromPet(pet));
    }

    ///<inheritdoc/>
    public async Task<HttpActionResult<PetDto>> Affection(int id, int userId)
    {
        var pet = await _db.Pets
            .Include(p => p.Species)
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

        if (pet == null)
        {
            return await HttpActionResult<PetDto>.Error(
                StatusCodes.Status400BadRequest,
                $"No pet with associated with user id: {userId}");
        }

        pet.Happiness = HappinessLevelType.Happy;
        pet.LastPetting = DateTime.UtcNow;
        pet.CalculateStats();

        await _db.SaveChangesAsync();

        return await HttpActionResult<PetDto>.Success(
            StatusCodes.Status200OK,
            PetDto.FromPet(pet));
    }

    ///<inheritdoc/>
    public async Task<HttpActionResult<PetDto>> Update(int petId, UpdatePetDto updatePetDto, int userId)
    {
        var pet = await _db.Pets
            .FirstOrDefaultAsync(p => p.Id == petId && p.UserId == userId);

        if (pet == null)
        {
            return await HttpActionResult<PetDto>.Error(
                StatusCodes.Status400BadRequest,
                $"No pet with associated with user id: {userId}");
        }

        pet.Name = updatePetDto.NewName;

        await _db.SaveChangesAsync();

        return await HttpActionResult<PetDto>.Success(
            StatusCodes.Status200OK,
            PetDto.FromPet(pet));
    }
}