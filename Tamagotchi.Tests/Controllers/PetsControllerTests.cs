using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using Moq;
using Moq.EntityFrameworkCore;
using Tamagotchi.API.Actions;
using Tamagotchi.API.Services;
using Tamagotchi.Data;
using Tamagotchi.Data.Models;
using Tamagotchi.DataAccess.Models.Pet;
using Tamagotchi.Tests.Fakes;

namespace Tamagotchi.Tests.Controllers;

public class PetsControllerTest
{
    private readonly Mock<TamagotchiDbContext> _dbMock;
    private readonly PetService _service;
    private readonly DbSet<Pet> _pets;

    public PetsControllerTest()
    {
        _dbMock = new Mock<TamagotchiDbContext>();
        _service = new PetService(_dbMock.Object);
        _pets = new PetFaker().Generate(1).AsQueryable().BuildMockDbSet();
    }

    [Fact]
    public async void GetPetById_ShouldReturn_SuccessWithExpected()
    {
        var expected = new HttpActionResult<PetDto>(200, PetDto.FromPet(_pets.First()));
        _dbMock.Setup(x => x.Pets).ReturnsDbSet(_pets);

        var result = await _service.GetById(1, 1);
        
        result.Should().NotBeNull();
        result.Should().BeOfType<HttpActionResult<PetDto>>();
        result.Should().Be(expected);
    }
}