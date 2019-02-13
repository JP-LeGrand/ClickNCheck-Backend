using ClickNCheck.Controllers;
using ClickNCheck.Data;
using ClickNCheck.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public  void Test1Async()
        {
            //// Arrange
            
            //IQueryable<JobProfile> mockData = new List<JobProfile> {
            //   new JobProfile {ID = 1, Title = "Software Developer", Description = "Software Developer description", isCompleted = true}
            //}.AsQueryable();

            //var mockSet = new Moq.Mock<DbSet<JobProfile>>();
            //mockSet.Setup(m => m.Find(username)).Returns((mockData.Where(x => x.Username == username)).First);

            //var mockContext = new Moq.Mock<DummyContext>();
            //mockContext.Setup(c => c.DummyModel).Returns(mockSet.Object);

            //var controller = new DummyModelsController(mockContext.Object);


            ////Act
            //var login = controller.Login(username, password).Result;


            ////Assert
            //Assert.IsInstanceOf<OkObjectResult>(login);
        }
    }
}