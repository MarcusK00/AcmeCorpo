using Acme.Core.Interfaces;
using Acme.Core.Models;
using Acme.Core.Services;
using Moq;
using Acme.Shared.DTOs;
using Acme.Api.Controllers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Tests;

[TestClass]
public class SubmissionControllerTests
{
 
    
    [TestMethod]
    public async Task Submit_ReturnsOk()
    {
        // Arrange
        var mockService = new Mock<ISubmissionService>();
        var testController = new SubmissionsController(mockService.Object);
        
        var dto = new SubmissionDto
        {
            FirstName = "Kristian",
            LastName = "Helse",
            Email = "kh@mail.com",
            DateOfBirth = new DateTime(2000, 3, 12),
            SerialNumber = "TEST-SERIAL"
        };
        mockService.Setup(s => s.SubmitAsync(It.IsAny<SubmissionDto>())).ReturnsAsync((true, null));
        
        // Act
        var result = await testController.Submit(dto);

        // Asset
        Assert.IsInstanceOfType(result, typeof(OkResult));
        mockService.Verify(s => s.SubmitAsync(It.IsAny<SubmissionDto>()), Times.Once);
    }

    [TestMethod]
    public async Task GetAll_ReturnsCorrectList_ReturnsOk()
    {
        //Arrange
        var mockService = new Mock<ISubmissionService>();
        var testController = new SubmissionsController(mockService.Object);
        var testSubmissions = new List<Submission>()
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
        
        mockService.Setup(s => s.GetAllSubmissionsAsync()).ReturnsAsync(testSubmissions);
        
        //Act
        var result = await testController.GetAllAsync();
        var okResult = result.Result as OkObjectResult; 
        var dtos = okResult!.Value as List<SubmissionDto>;
        
        //Assert
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.HasCount(2,dtos!);
        mockService.Verify(s=>s.GetAllSubmissionsAsync(), Times.Once);
    }
}
    
