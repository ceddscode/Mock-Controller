using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using mock.depart.Controllers;
using mock.depart.Models;
using mock.depart.Services;
using Moq;


namespace mock.tESTS
{
    [TestClass]
    public class TTTTests
    {
        [TestMethod]
        public void DeleteCat()
        {
            Mock<CatsService> ServiceMock = new Mock<CatsService>();

            Mock<CatsController> ControllerMock = new Mock<CatsController>(ServiceMock.Object) { CallBase = true };

            ControllerMock.Setup(c => c.UserId).Returns("11111");

            CatOwner JM = new CatOwner()
            {
                Id = "11111",
            };
            Cat diddy = new Cat()
            {
                CatOwner = JM,
                Id = 1,
                CuteLevel = Cuteness.BarelyOk,
            };

            ServiceMock.Setup(cp => cp.Get(It.IsAny<int>())).Returns(diddy);

            ServiceMock.Setup(cp => cp.Delete(It.IsAny<int>())).Returns(diddy);

            var ar = ControllerMock.Object.DeleteCat(1);
            var result = ar.Result as OkObjectResult;
            Assert.IsNotNull(result);
            Cat? cat = (Cat?)result!.Value;
            Assert.AreEqual(diddy.Id, cat!.Id);



        }

        [TestMethod]
        public void CatOwnerError()
        {
            Mock<CatsService> ServiceMock = new Mock<CatsService>();

            Mock<CatsController> ControllerMock = new Mock<CatsController>(ServiceMock.Object) { CallBase = true };

            ControllerMock.Setup(c => c.UserId).Returns("11111");

            CatOwner JM = new CatOwner()
            {
                Id = "2",
            };
            Cat diddy = new Cat()
            {
                CatOwner = JM,
                Id = 1,
                CuteLevel = Cuteness.BarelyOk,
            };

            ServiceMock.Setup(cp => cp.Get(It.IsAny<int>())).Returns(diddy);

            var ar = ControllerMock.Object.DeleteCat(1);
            var result = ar.Result as BadRequestObjectResult;
            Assert.IsNotNull(result);

            Assert.AreEqual("Cat is not yours",result.Value );
            



        }

        [TestMethod]
        public void TooCute()
        {
            Mock<CatsService> ServiceMock = new Mock<CatsService>();

            Mock<CatsController> ControllerMock = new Mock<CatsController>(ServiceMock.Object) { CallBase = true };

            ControllerMock.Setup(c => c.UserId).Returns("11111");

            CatOwner JM = new CatOwner()
            {
                Id = "11111",
            };
            Cat diddy = new Cat()
            {
                CatOwner = JM,
                Id = 1,
                CuteLevel = Cuteness.Amazing,
            };

            ServiceMock.Setup(cp => cp.Get(It.IsAny<int>())).Returns(diddy);

            ServiceMock.Setup(cp => cp.Delete(It.IsAny<int>())).Returns(diddy);

            var ar = ControllerMock.Object.DeleteCat(1);
            var result = ar.Result as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Cat is too cute", result.Value );

        }

        [TestMethod]
        public void CatNonExistent()
        {
            Mock<CatsService> ServiceMock = new Mock<CatsService>();

            Mock<CatsController> ControllerMock = new Mock<CatsController>(ServiceMock.Object) { CallBase = true };

            ControllerMock.Setup(c => c.UserId).Returns("11111");

            CatOwner JM = new CatOwner()
            {
                Id = "11111",
            };
           

            ServiceMock.Setup(cp => cp.Get(It.IsAny<int>())).Returns(value : null);

            ServiceMock.Setup(cp => cp.Delete(It.IsAny<int>())).Returns(value: null);

            var ar = ControllerMock.Object.DeleteCat(1);
            var result = ar.Result as NotFoundResult;
            Assert.IsNotNull(result);
            
        }
    }

    
}
