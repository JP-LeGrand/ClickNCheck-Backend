using NUnit.Framework;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ClickNCheck.Models;
using ClickNCheck.Controllers;
using ClickNCheck.Data;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void GeneratesUniqueCode()
        {
            //Arrange
            IQueryable<Administrator> mockAdministrator = new List<Administrator> {
               new Administrator {ID = 1, Email = "nane@gmail.com" }
            }.AsQueryable();


            var mockSet = new Mock<DbSet<LinkCode>>();
            var mockContext = new Mock<ClickNCheckContext>();
            mockContext.Setup(c => c.LinkCodes).Returns(mockSet.Object);
            var controller = new AdministratorsController(mockContext.Object);
            var code = controller.generateCode();


            IQueryable<LinkCode> mockLinkCode = new List<LinkCode> {
               new LinkCode {ID = 1, Code = code, /*Admin_ID = 1,*/Valid = false}
            }.AsQueryable();


            mockSet.Setup(m => m.Find(code)).Returns(mockLinkCode.Where(x => x.Code == code).First);

            //Act
            var result = controller.generateCode();

            //Assert
            Assert.AreNotEqual(code, result);
        }


        [Test]
        public void CodeValid()
        {
            IQueryable<Administrator> mockAdministrator = new List<Administrator> {
               new Administrator {ID = 1, Email = "nane@gmail.com" }
            }.AsQueryable();


            var mockSet = new Mock<DbSet<LinkCode>>();
            var mockContext = new Mock<ClickNCheckContext>();
            mockContext.Setup(c => c.LinkCodes).Returns(mockSet.Object);
            var controller = new LinkCodesController(mockContext.Object);
            string code = "thiscodeexists";

            IQueryable<LinkCode> mockLinkCode = new List<LinkCode> {
               new LinkCode {ID = 1, Code = code, /*Admin_ID = 1,*/Valid = false}
            }.AsQueryable();


            mockContext.Setup(m => m.LinkCodes.Find(code)).Returns(mockLinkCode.Where(x => x.Code == code).First);

            //Act
            var result = controller.IsCodeValid("thiscodeexists");

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CodeInvalid()
        {
            IQueryable<Administrator> mockAdministrator = new List<Administrator> {
               new Administrator {ID = 1, Email = "nane@gmail.com" }
            }.AsQueryable();


            var mockSet = new Mock<DbSet<LinkCode>>();
            var mockContext = new Mock<ClickNCheckContext>();
            mockContext.Setup(c => c.LinkCodes).Returns(mockSet.Object);
            var controller = new LinkCodesController(mockContext.Object);
            string code = "thiscodeexists";

            IQueryable<LinkCode> mockLinkCode = new List<LinkCode> {
               new LinkCode {ID = 1, Code = code, /*Admin_ID = 1,*/Valid = false}
            }.AsQueryable();


            mockContext.Setup(m => m.LinkCodes.Find(code)).Returns(mockLinkCode.Where(x => x.Code == code).First);

            //Act
            var result = controller.IsCodeValid("thiscodedoesntexist");

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void UpdateCodeStatus()
        {
            var mockContext = new Mock<ClickNCheckContext>();
            LinkCodesController dummyController = new LinkCodesController(mockContext.Object);

            var dummyModel = new Mock<LinkCode>();
            dummyModel.Object.ID = 1;
            dummyModel.Object.Code = "thiscodeexists";
            dummyModel.Object.Valid = false;
          //  dummyModel.Object.Admin_ID = 1;

            //Act
            dummyController.UpdateCodeStatus(dummyModel.Object);

            //Assert
            mockContext.Verify(m => m.Update(dummyModel.Object), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}