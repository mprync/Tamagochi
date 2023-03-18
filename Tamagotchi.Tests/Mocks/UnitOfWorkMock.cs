using MockQueryable.NSubstitute;
using Moq;
using Moq.EntityFrameworkCore;
using Tamagotchi.Data;
using Tamagotchi.Data.Models;
using Tamagotchi.Data.Repositories;
using Tamagotchi.Data.UnitOfWork;

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
        
        dbContext.Setup(db => db.Pets).ReturnsDbSet(new List<Pet>().AsQueryable().BuildMockDbSet());
        dbContext.Setup(db => db.Foods).ReturnsDbSet(new List<Food>().AsQueryable().BuildMockDbSet());
        dbContext.Setup(db => db.Species).ReturnsDbSet(new List<Species>().AsQueryable().BuildMockDbSet());
        dbContext.Setup(db => db.Users).ReturnsDbSet(new List<User>().AsQueryable().BuildMockDbSet());

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
    }
}