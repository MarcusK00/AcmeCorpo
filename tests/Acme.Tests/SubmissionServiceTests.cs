using Acme.Core.Interfaces;
using Acme.Core.Models;
using Acme.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.Shared.DTOs;
using Microsoft.Extensions.DependencyModel;

namespace Acme.Tests;

[TestClass]
public class SubmissionServiceTests
{
    [TestMethod] // Tests whether GetAllSubmissionsAsync() method works in src/Acme.Core/Services/SubmissionService.cs
    public async Task GetAllSubmissionsAsync_ReturnsAllSubmissions() 
    {
        // Arrange
        var mockRepository = new Mock<ISubmissionRepository>();
        var testSubmissions = new List<Submission>
        {
            new Submission
            {
                Id = 1, FirstName = "Kristian", LastName = "Helse", Email = "kh@mail.com",
                BirthDate = new DateTime(2000, 03, 12), SerialCode = "THIS-IS-TEST-SERIAL",
                SubmittedAt = DateTime.UtcNow
            },
            new Submission
            {
                Id = 2, FirstName = "Hans", LastName = "Erik", Email = "he@mymail.co",
                BirthDate = new DateTime(1997, 05, 20), SerialCode = "THIS-IS-MY-SERIAL", SubmittedAt = DateTime.UtcNow
            },
        };
        mockRepository.Setup(repo => repo.GetAllSubmissionsAsync())
            .ReturnsAsync(testSubmissions); //When we call GetAll method we return with the test list.

        var service =
            new SubmissionService(mockRepository
                .Object);

        // Act
        var result = await service.GetAllSubmissionsAsync();

        // Assert
        Assert.IsNotNull(result); 
        Assert.AreEqual(2, result.Count());
        mockRepository.Verify(repo => repo.GetAllSubmissionsAsync(), Times.Once); //Verifies the method gets called exactly one time. Test will fail otherwise.
    }

    [TestMethod] // Tests SubmitAsync method in SubmissionService.cs
    public async Task SubmitAsync_SubmitsCorrectly()
    {
        //Arrange
        var mockRepository = new Mock<ISubmissionRepository>();
        var testDto = new SubmissionDto
        {
        FirstName = "Kristian",
        LastName = "Helse",
        Email = "kh@mail.com",
        DateOfBirth = new DateTime(2000, 3, 12),  //Atleast 18 years of age.
        SerialNumber = "THIS-IS-TEST-SERIAL"
         };

        // Makes sure serial number exists.
        mockRepository.Setup(repo => repo.SerialExistsAsync("THIS-IS-TEST-SERIAL"))
            .ReturnsAsync(true);

        // Make sure serial number gets returned once.
        mockRepository.Setup(repo => repo.GetSubmissionsCountAsync("THIS-IS-TEST-SERIAL"))
            .ReturnsAsync(1);  

        // Makes method accept any submission.
        mockRepository.Setup(repo => repo.AddSubmissionAsync(It.IsAny<Submission>()))
            .Returns(Task.CompletedTask);

        var service = new SubmissionService(mockRepository.Object);
       
        //Act
        var result = await service.SubmitAsync(testDto);

        //Assert
        Assert.IsTrue(result.Success);
        Assert.IsNull(result.Error);
        
        //Verify
        mockRepository.Verify(repo => repo.AddSubmissionAsync(It.IsAny<Submission>()), Times.Once);
        mockRepository.Verify(repo => repo.SerialExistsAsync("THIS-IS-TEST-SERIAL"), Times.Once);
        mockRepository.Verify(repo => repo.GetSubmissionsCountAsync("THIS-IS-TEST-SERIAL"), Times.Once);
    }
}
