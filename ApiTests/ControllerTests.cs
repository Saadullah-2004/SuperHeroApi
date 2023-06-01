using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SuperHeroApi.Controllers;
using SuperHeroApi.Models;
using SuperHeroApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTests
{
    [TestClass]
    public class ControllerTests
    {
        private Mock<ISuperHeroService> _service; // Mocking the repository
        private Fixture _fixture; // Creating Objects will Need when giving the repository fake data
        private SuperHeroController _controller; // Which will get fed the mock repository

        public ControllerTests()
        {
            _fixture = new Fixture();
            _service = new Mock<ISuperHeroService>();
            _controller = new SuperHeroController(_service.Object);
        }

        [TestMethod]

        public void Get_Hero_ReturnOK()
        {
            var heroList = _fixture.CreateMany<SuperHero>(3).ToList();
            _service.Setup(repo => repo.GetHeroList()).Returns(heroList);
            _controller = new SuperHeroController(_service.Object);

            var result = _controller.Get();
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
            
        }

        [TestMethod]

        public void Get_HeroID_ReturnOK()
        {
            _service.Setup(repo => repo.GetHeroDetailsById(It.IsAny<int>())).Returns(It.IsAny<SuperHero>);
            _controller = new SuperHeroController(_service.Object);

            var result = _controller.Get(It.IsAny<int>());
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);

        }

        [TestMethod]
        public void Get_Hero_ThrowException()
        {
            _service.Setup(repo => repo.GetHeroList()).Throws(new Exception());
            _controller = new SuperHeroController(_service.Object);
            var result = _controller.Get();
            var obj = result as ObjectResult;
            Assert.AreEqual(400, obj.StatusCode);
        }

        [TestMethod]
        public void Post_Hero_ReturnOk()
        {
            var hero = _fixture.Create<SuperHero>();
            _service.Setup(repo => repo.InsertHero(It.IsAny<SuperHero>())).Returns(hero);
            _controller = new SuperHeroController(_service.Object);

            var result = _controller.AddHero(hero);
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);   
        }

        [TestMethod]
        public void Put_Hero_ReturnOk()
        {
            var hero = _fixture.Create<SuperHero>();
            _service.Setup(repo => repo.ChangeHero(It.IsAny<SuperHero>())).Returns(hero);
            _controller = new SuperHeroController(_service.Object);

            var result = _controller.UpdateHero(hero);
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public void Delete_Hero_ReturnOk()
        {
            _service.Setup(repo => repo.DeleteHero(It.IsAny<int>())).Returns(true);
            _controller = new SuperHeroController(_service.Object);

            var result = _controller.Delete(It.IsAny<int>());
                var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }

    }
}
