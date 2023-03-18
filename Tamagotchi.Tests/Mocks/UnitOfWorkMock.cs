using MockQueryable.NSubstitute;
using Moq;
using Moq.EntityFrameworkCore;
using Tamagotchi.Data;
using Tamagotchi.Data.Repositories;
using Tamagotchi.Data.UnitOfWork;
using Tamagotchi.Tests.Fakes.Models;

namespace Tamagotchi.Tests.Mocks;

public class UnitOfWorkMock
{
    public Mock<UnitOfWork> Mock { get; }
    public Mock<PetsRepository> PetsRepository { get; }
    public Mock<FoodsRepository> FoodsRepository { get; }
    public Mock<SpeciesRepository> SpeciesRepository { get; }
    public Mock<UsersRepository> UsersRepository { get; }

    public UnitOfWorkMock()
    {
        var dbContext = new Mock<TamagotchiDbContext>();
        var petsRepository = new Mock<PetsRepository>(dbContext.Object);
        var foodsRepository = new Mock<FoodsRepository>(dbContext.Object);
        var speciesRepository = new Mock<SpeciesRepository>(dbContext.Object);
        var usersRepository = new Mock<UsersRepository>(dbContext.Object);
        var unitOfWork = new Mock<UnitOfWork>(dbContext.Object);
        
        dbContext.Setup(db => db.Pets).ReturnsDbSet(new PetFaker(1, 1).Generate(10).AsQueryable().BuildMockDbSet());
        dbContext.Setup(db => db.Foods).ReturnsDbSet(new FoodsFaker(1).Generate(15).AsQueryable().BuildMockDbSet());
        dbContext.Setup(db => db.Species).ReturnsDbSet(new SpeciesFaker().Generate(3).AsQueryable().BuildMockDbSet());
        dbContext.Setup(db => db.Users).ReturnsDbSet(new UserFaker().Generate(2).AsQueryable().BuildMockDbSet());

        petsRepository.Setup(r => r.GetManyQueryable()).ReturnsDbSet(dbContext.Object.Pets);
        foodsRepository.Setup(r => r.GetManyQueryable()).ReturnsDbSet(dbContext.Object.Foods);
        speciesRepository.Setup(r => r.GetManyQueryable()).ReturnsDbSet(dbContext.Object.Species);
        usersRepository.Setup(r => r.GetManyQueryable()).ReturnsDbSet(dbContext.Object.Users);

        unitOfWork.Setup(p => p.Pets).Returns(petsRepository.Object);
        unitOfWork.Setup(p => p.Foods).Returns(foodsRepository.Object);
        unitOfWork.Setup(p => p.Species).Returns(speciesRepository.Object);
        unitOfWork.Setup(p => p.Users).Returns(usersRepository.Object);

        unitOfWork.Setup(p => p.SaveAsync()).ReturnsAsync(1);
        
        Mock = unitOfWork;
        PetsRepository = petsRepository;
        FoodsRepository = foodsRepository;
        SpeciesRepository = speciesRepository;
        UsersRepository = usersRepository;
        
        // Lets calculate the stats for the pets above, this will set the expected values to match the tests
        foreach (var pet in dbContext.Object.Pets.ToList())
        {
            pet.CalculateStats();
        }
    }
}