using Microsoft.EntityFrameworkCore;
using Tamagotchi.API.Actions;
using Tamagotchi.API.Extentions;
using Tamagotchi.API.Services.Interfaces;
using Tamagotchi.Data;
using Tamagotchi.Data.Models;
using Tamagotchi.DataAccess.Models.Food;
using Tamagotchi.DataAccess.Models.Pagination;
using Tamagotchi.DataAccess.Responses;
using Tamagotchi.DataAccess.Responses.Pagination.Filter;

namespace Tamagotchi.API.Services;

/// <summary>
/// The food service
///
/// TODO: Add logging
/// </summary>
public class FoodService : IFoodsService
{
    private readonly TamagotchiDbContext _db;

    public FoodService(TamagotchiDbContext dbContext)
    {
        _db = dbContext;
    }

    ///<inheritdoc/>
    public async Task<HttpActionResult<PagedModel<FoodDto>>> Get(PaginationFilters pageFilter)
    {
        var foods = await _db.Foods
            .AsNoTracking()
            .Include(p => p.Species)
            .Select(p => FoodDto.FromFood(p))
            .PaginateAsync(pageFilter.Page, pageFilter.Limit);

        return await HttpActionResult<PagedModel<FoodDto>>.Success(
            StatusCodes.Status200OK,
            foods);
    }

    ///<inheritdoc/>
    public async Task<HttpActionResult<FoodDto>> GetById(int id)
    {
        var food = await _db.Foods
            .AsNoTracking()
            .Include(p => p.Species)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (food == null)
        {
            return await HttpActionResult<FoodDto>.Error(
                StatusCodes.Status400BadRequest,
                $"No food with id: {id} found");
        }

        return await HttpActionResult<FoodDto>.Success(
            StatusCodes.Status200OK,
            FoodDto.FromFood(food));
    }


    ///<inheritdoc/>
    public async Task<HttpActionResult<FoodDto>> Create(CreateFoodDto foodDto)
    {
        var food = await _db.Foods.AddAsync(new Food
        {
            Name = foodDto.Name,
            WeightGainKg = foodDto.WeightGainKg,
            SpeciesId = foodDto.SpeciesId
        });

        await _db.SaveChangesAsync();

        return await HttpActionResult<FoodDto>.Success(
            StatusCodes.Status200OK,
            FoodDto.FromFood(food.Entity));
    }

    ///<inheritdoc/>
    public async Task<HttpActionResult<FoodDto>> Update(int id, UpdateFoodDto updateFoodDto)
    {
        var food = await _db.Foods
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if (food == null)
        {
            return await HttpActionResult<FoodDto>.Error(
                StatusCodes.Status400BadRequest,
                $"No food found with id: {id}");
        }

        food.Name = updateFoodDto.Name;
        food.WeightGainKg = updateFoodDto.WeightGainKg;
        
        await _db.SaveChangesAsync();
        
        return await HttpActionResult<FoodDto>.Success(
            StatusCodes.Status200OK,
            FoodDto.FromFood(food));
    }

    ///<inheritdoc/>
    public async Task<HttpActionResult<Response>> Delete(int id)
    {
        var food = await _db.Foods
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (food == null)
        {
            return await HttpActionResult<Response>.Error(
                StatusCodes.Status400BadRequest,
                $"No food with id: {id} found");
        }

        _db.Foods.Remove(food);
        await _db.SaveChangesAsync();

        return await HttpActionResult<Response>.Success(
            StatusCodes.Status204NoContent);
    }
}