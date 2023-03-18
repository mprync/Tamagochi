using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Tamagotchi.Data.UnitOfWork;
using Tamagotchi.Data.UnitOfWork.Interfaces;

namespace Tamagotchi.Tests.Builders
{
    public class ControllerBuilder<TController, TService> where TController : ControllerBase where TService : class
    {
        private readonly ClaimsIdentity _identity;
        private readonly ControllerContext _controllerContext;
        private IUnitOfWork _unitOfWork;

        public ControllerBuilder()
        {
            _identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(_identity);
            _controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };
        }

        public ControllerBuilder<TController, TService> WithClaims(IDictionary<string, string> claims)
        {
            _identity.AddClaims(claims.Select(c => new Claim(c.Key, c.Value)));
            return this;
        }

        public ControllerBuilder<TController, TService> WithUnitOfWork(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            return this;
        }

        public ControllerBuilder<TController, TService> WithIdentity(string userId, string userName)
        {
            _identity.AddClaims(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName)
            });
            return this;
        }

        public ControllerBuilder<TController, TService> WithDefaultIdentityClaims()
        {
            _identity.AddClaims(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "testId"),
                new Claim(ClaimTypes.Name, "testName")
            });
            return this;
        }

        public TController Build()
        {
            var service = new Mock<TService>(_unitOfWork).Object;
            var logger = new Mock<ILogger<TController>>().Object;
            var controller = new Mock<TController>(service, logger).Object;
            controller.ControllerContext = _controllerContext;
            
            return controller;
        }
    }
}