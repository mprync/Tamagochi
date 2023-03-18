using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MockQueryable.NSubstitute;
using Moq;
using Tamagotchi.API.Controllers;
using Tamagotchi.API.Services;
using Tamagotchi.Data.Enums;
using Tamagotchi.Data.Models;
using Tamagotchi.DataAccess.Models.Pet;
using Tamagotchi.DataAccess.Responses.Pagination.Filter;
using Tamagotchi.Tests.Builders;
using Tamagotchi.Tests.Fakes.Models;
using Tamagotchi.Tests.Mocks;

namespace Tamagotchi.Tests.Controllers;

public class PetsControllerTest
{
    private readonly PetsController _controller;
    private readonly UnitOfWorkMock _unitOfWork;

    public PetsControllerTest()
    {
        _unitOfWork = new UnitOfWorkMock();
        _controller = new ControllerBuilder<PetsController, PetService>()
            .WithUnitOfWork(_unitOfWork.Mock.Object)
            .WithIdentity(userId: "1", userName: "TestUser")
            .Build();
    }

    [Fact]
    public async void GetPetById_ShouldReturn_SuccessWithExpected()
    {
        var pet = new PetFaker(userId: 1, speciesId: 1).Generate();
        var expected = new List<Pet>{pet}.AsQueryable().BuildMockDbSet();
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(expected);
        
        var response = await _controller.GetById(1);
        
        response.Result.Data.Id.Should().Be(1);
        response.Result.Status.Should().Be(StatusCodes.Status200OK);
    }
    
    [Fact]
    public async void GetPetById_ShouldReturn_FailureWithExpected()
    {
        var pet = new PetFaker(userId: 1, speciesId: 1).Generate();
        var expected = new List<Pet>{pet}.AsQueryable().BuildMockDbSet();
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(expected);
        
        var response = await _controller.GetById(2);
        
        response.Result.Status.Should().Be(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async void GetPets_ShouldReturn_SuccessWithExpected()
    {
        var pets = new PetFaker(userId: 1, speciesId: 1).Generate(5);
        pets.ForEach(p => p.CalculateStats());
        var expected = pets.AsQueryable().BuildMockDbSet();
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(expected);
        
        var response = await _controller.Get(new PaginationFilters());
        
        response.Result.Data.Items.Count.Should().Be(5);
    }
    
    [Fact]
    public async void GetPets_ShouldReturn_SuccessWithNoResults()
    {
        var expected = new List<Pet>().AsQueryable().BuildMockDbSet();
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(expected);
        
        var response = await _controller.Get(new PaginationFilters());
        
        response.Result.Data.Items.Count.Should().Be(0);
    }
    
    [Fact]
    public async void Create_ShouldReturn_SuccessWithExpected()
    {
        var expected = new PetFaker(userId: 1, speciesId: 1).Generate();
        expected.CalculateStats();
        
        _unitOfWork.PetsRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(default(Pet));
        _unitOfWork.PetsRepository.Setup(x => x.Add(It.IsAny<Pet>())).ReturnsAsync(expected);

        var response = await _controller.Create(new CreatePetDto("TestPet", 1));
        
        response.Result.Data.Id.Should().Be(1);
        response.Result.Data.Name.Should().Be(expected.Name);
        response.Result.Status.Should().Be(StatusCodes.Status201Created);
    }
    
    [Fact]
    public async void Create_ShouldReturn_FailureSpeciesDoesntExist()
    {
        var species = new SpeciesFaker().Generate();

        _unitOfWork.SpeciesRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(species);

        var response = await _controller.Create(new CreatePetDto("TestPet", 1));

        response.Result.Data.Should().BeNull();
        response.Result.Status.Should().Be(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async void Update_ShouldReturn_SuccessWithExpected()
    {
        var pets = new PetFaker(userId: 1, speciesId: 1).Generate(2);
        pets.ForEach(p => p.CalculateStats());
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(pets.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());

        var response = await _controller.Update(1, new UpdatePetDto("TESTING"));

        response.Result.Data.Name.Should().Be("TESTING");
        response.Result.Status.Should().Be(StatusCodes.Status200OK);
    }
    
    [Fact]
    public async void Update_ShouldReturn_FailurePetAlreadyExists()
    {
        var pets = new PetFaker(userId: 1, speciesId: 1).Generate(2);
        pets.ForEach(p => p.CalculateStats());
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(pets.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());

        var response = await _controller.Update(3, new UpdatePetDto("TESTING"));

        response.Result.Status.Should().Be(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async void Delete_ShouldReturn_SuccessWithExpected()
    {
        var pets = new PetFaker(userId: 1, speciesId: 1).Generate(1);
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(pets.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());
        _unitOfWork.PetsRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(default(Pet));

        var response = await _controller.Delete(1);

        response.Result.Status.Should().Be(StatusCodes.Status204NoContent);
    }
    
    [Fact]
    public async void Delete_ShouldReturn_FailureWithExpected()
    {
        var pets = new PetFaker(userId: 1, speciesId: 1).Generate(1);
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(pets.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());

        var response = await _controller.Delete(2);

        response.Result.Status.Should().Be(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async void Feed_ShouldReturn_SuccessWithExpected()
    {
        var pets = new PetFaker(userId: 1, speciesId: 1).Generate(1);
        pets[0].Hunger = HungerLevelType.Hungry;
        pets[0].LastFed = DateTime.UtcNow - TimeSpan.FromHours(3);
        var foods = new FoodsFaker(speciesId: 1).Generate(1);
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(pets.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());
        _unitOfWork.FoodsRepository.Setup(x => x.GetManyQueryable()).Returns(foods.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());

        var response = await _controller.Feed(1, new FeedPetDto(1));

        response.Result.Status.Should().Be(StatusCodes.Status200OK);
        response.Result.Data.Hunger.Should().Be(HungerLevelType.Full);
        response.Result.Data.LastFed.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
    
    [Fact]
    public async void Feed_ShouldReturn_FailurePetNotFound()
    {
        var pets = new PetFaker(userId: 1, speciesId: 1).Generate(1);
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(pets.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());

        var response = await _controller.Feed(2, new FeedPetDto(1));

        response.Result.Status.Should().Be(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async void Feed_ShouldReturn_FailureFoodNotFound()
    {
        var pets = new PetFaker(userId: 1, speciesId: 1).Generate(1);
        var foods = new FoodsFaker(speciesId: 1).Generate(1);
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(pets.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());
        _unitOfWork.FoodsRepository.Setup(x => x.GetManyQueryable()).Returns(foods.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());

        var response = await _controller.Feed(1, new FeedPetDto(2));

        response.Result.Status.Should().Be(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async void Feed_ShouldReturn_FailureSpeciesNotFound()
    {
        var pets = new PetFaker(userId: 1, speciesId: 1).Generate(1);
        pets[0].Species = null;
        var foods = new FoodsFaker(speciesId: 1).Generate(1);
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(pets.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());
        _unitOfWork.FoodsRepository.Setup(x => x.GetManyQueryable()).Returns(foods.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());

        var response = await _controller.Feed(1, new FeedPetDto(2));

        response.Result.Status.Should().Be(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async void Feed_ShouldReturn_FailurePetDoesntLikeThatFoodType()
    {
        var pets = new PetFaker(userId: 1, speciesId: 1).Generate(1);
        var foods = new FoodsFaker(speciesId: 1).Generate(1);
        pets[0].Species.Foods = new FoodsFaker(2).Generate(1);
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(pets.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());
        _unitOfWork.FoodsRepository.Setup(x => x.GetManyQueryable()).Returns(foods.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());

        var response = await _controller.Feed(1, new FeedPetDto(1));

        response.Result.Status.Should().Be(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async void Feed_ShouldReturn_FailurePetSpeciesHasNoFoodTypes()
    {
        var pets = new PetFaker(userId: 1, speciesId: 1).Generate(1);
        var foods = new FoodsFaker(speciesId: 1).Generate(1);
        pets[0].Species.Foods = null;
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(pets.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());
        _unitOfWork.FoodsRepository.Setup(x => x.GetManyQueryable()).Returns(foods.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());

        var response = await _controller.Feed(1, new FeedPetDto(1));

        response.Result.Status.Should().Be(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async void Affection_ShouldReturn_SuccessAndHappy()
    {
        var pets = new PetFaker(userId: 1, speciesId: 1).Generate(1);
        pets[0].Happiness = HappinessLevelType.Unhappy;
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(pets.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());

        var response = await _controller.Affection(1);

        response.Result.Data.Happiness.Should().Be(HappinessLevelType.Happy);
        response.Result.Status.Should().Be(StatusCodes.Status200OK);
    }
    
    [Fact]
    public async void Affection_ShouldReturn_FailurePetNotFound()
    {
        var pets = new PetFaker(userId: 1, speciesId: 1).Generate(1);
        _unitOfWork.PetsRepository.Setup(x => x.GetManyQueryable()).Returns(pets.Where(p => p.Id == 1).AsQueryable().BuildMockDbSet());

        var response = await _controller.Affection(2);

        response.Result.Status.Should().Be(StatusCodes.Status400BadRequest);
    }
}