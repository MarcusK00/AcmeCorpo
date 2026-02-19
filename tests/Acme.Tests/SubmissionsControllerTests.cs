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
    
    


    
    
}
    
