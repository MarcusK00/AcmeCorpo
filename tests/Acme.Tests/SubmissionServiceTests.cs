using Acme.Core.Interfaces;
using Acme.Core.Models;
using Acme.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.Tests;

[TestClass]
public class SubmissionServiceTests
{

    [TestMethod]
    public async Task GetAllSubmissions_ReturnsAllSubmissions()
    {
        // Arrange
        var mockRepository = new Mock<ISubmissionRepository>();
        var testSubmissions = new List<Submission>
        {
            new Submission { Id = 1, FirstName = "Kristian", LastName ="Herlev", Email = "kh@mail.com", BirthDate = new DateTime(2000,03,12), SerialCode = "TEST-TEST-TEST-TEST", SubmittedAt = DateTime.UtcNow},
            new Submission { Id = 2, FirstName = "Hans", LastName ="Erik", Email = "he@mymail.co", BirthDate = new DateTime(1997,05,20), SerialCode = "THIS-IS-MY-SERIAL", SubmittedAt = DateTime.UtcNow},
        };
        mockRepository.Setup(repo => repo.GetAllSubmissionsAsync()).ReturnsAsync(testSubmissions);

        var service = new SubmissionService(mockRepository.Object);

        // Act
        var result = await service.GetAllSubmissionsAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
        mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
    }
    
}
